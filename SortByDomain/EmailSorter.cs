using SortByDomain.Extentions;
using SortByDomain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace SortByDomain
{
    public class EmailSorter : IDisposable
    {
        private const string RULE_DOMAIN_NAME_MATCH = "^@([a-z0-9]+(-[a-z0-9]+)*\\.)+[a-z]{2,}$";
        private const string TAGS_SMTP_ADDRESS = "http://schemas.microsoft.com/mapi/proptag/0x39FE001E";

        private MAPIFolderEx copyToFolder;
        private Outlook.NameSpace defaultNamespace;
        private MAPIFolderEx moveToFolder;
        private CancellationTokenSource sortCancellationTokenSource;

        //@domain.com naming
        private MAPIFolderEx sortFolder;

        public EmailSorter()
        {
            defaultNamespace = AddInHelper.AddInApplication.GetNamespace("MAPI");
            Sorting = false;
        }

        public event EventHandler<decimal> ReportProgress;

        public event EventHandler<string> ReportStatus;

        public string CopyToFolderName { get => copyToFolder?.GetName(ref defaultNamespace) ?? string.Empty; }
        public string MoveToFolderName { get => moveToFolder?.GetName(ref defaultNamespace) ?? string.Empty; }
        public string SortFolderName { get => sortFolder?.GetName(ref defaultNamespace) ?? string.Empty; }
        public bool Sorting { get; internal set; }

        public bool CanBeginSort(bool createRules,
                                 bool createMoveRule,
                                 bool createCopyRule,
                                 bool performAction,
                                 bool performMoveAction,
                                 bool performCopyAction)
        {
            if (Sorting) return false;
            if (sortFolder == null) return false;
            if (!createRules && !performAction) return false;
            if (createRules && !(createMoveRule | createCopyRule)) return false;
            if (performAction && !(performMoveAction | performCopyAction)) return false;
            if ((createMoveRule | performMoveAction) && moveToFolder == null) return false;
            if ((createCopyRule | performCopyAction) && copyToFolder == null) return false;

            return true;
        }

        public void Cancel()
        {
            sortCancellationTokenSource?.Cancel();
        }

        public void Dispose()
        {
            CleanupSortFolder();
            CleanupMoveFolder();
            CleanupCopyFolder();

            FunctionHelper.ConsumeFinalReleaseNullComObject(defaultNamespace);

            defaultNamespace = null;
        }

        public async Task<bool> Sort(bool createRules,
                                     bool createMoveRule,
                                     bool createCopyRule,
                                     bool performAction,
                                     bool performMoveAction,
                                     bool performCopyAction,
                                     bool createMoveSubdirectories,
                                     bool createCopySubdirectories)
        {
            return await Sort(createRules,
                              createMoveRule,
                              createCopyRule,
                              performAction,
                              performMoveAction,
                              performCopyAction,
                              createMoveSubdirectories,
                              createCopySubdirectories,
                              new CancellationTokenSource().Token).ConfigureAwait(false);
        }

        public async Task<bool> Sort(bool createRules,
                                     bool createMoveRule,
                                     bool createCopyRule,
                                     bool performAction,
                                     bool performMoveAction,
                                     bool performCopyAction,
                                     bool createMoveSubdirectories,
                                     bool createCopySubdirectories,
                                     CancellationToken cancellationToken)
        {
            Sorting = true;
            ReportProgress?.Invoke(this, 0);

            try
            {
                sortCancellationTokenSource?.Cancel();
                sortCancellationTokenSource = new CancellationTokenSource();

                using (var linkedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(sortCancellationTokenSource.Token, cancellationToken))
                {
                    var linkedCancellationToken = linkedCancellationTokenSource.Token;

                    if (sortFolder.GetItemCount(ref defaultNamespace) > 0)
                    {
                        Dictionary<string, Outlook.Rule> domainRules = null;

                        try
                        {
                            domainRules = await GetDomainRules(linkedCancellationToken);

                            await SortItems(createRules,
                                            createMoveRule,
                                            createCopyRule,
                                            performAction,
                                            performMoveAction,
                                            performCopyAction,
                                            createMoveSubdirectories,
                                            createCopySubdirectories,
                                            domainRules,
                                            linkedCancellationToken);
                        }
                        finally
                        {
                            var keys = new string[domainRules?.Keys?.Count ?? 0];

                            domainRules?.Keys.CopyTo(keys, 0);

                            //Cleanup COM dictionary
                            foreach (var key in keys)
                            {
                                FunctionHelper.ConsumeFinalReleaseNullComObject(domainRules[key]);

                                domainRules[key] = null;
                            }

                            domainRules = null;
                        }

                        linkedCancellationToken.ThrowIfCancellationRequested();
                    }
                }

                sortCancellationTokenSource.Cancel();
            }
            catch (OperationCanceledException)
            {
                ReportStatus?.Invoke(this, "Sorting cancelled");
                ReportProgress?.Invoke(this, 100);

                return false;
            }
            catch (Exception ex)
            {
                ReportStatus?.Invoke(this, $"Exception occurred - {ex.Message}");
                ReportStatus?.Invoke(this, ex.StackTrace);

                return false;
            }
            finally
            {
                Sorting = false;
            }

            ReportStatus?.Invoke(this, "Sorting complete");
            ReportProgress?.Invoke(this, 100);

            return true;
        }

        #region Rules logic

        private void CreateDomainRule(Dictionary<string, Outlook.Rule> rules,
                                      string domain,
                                      bool createMoveRule,
                                      bool createCopyRule,
                                      Outlook.MAPIFolder moveFolder,
                                      Outlook.MAPIFolder copyFolder)
        {
            Outlook.Rules ruleSet = null;
            Outlook.Rule rule = null;

            try
            {
                ruleSet = sortFolder?.Folder.Store?.GetRules();
                rule = ruleSet?.Create(domain, Outlook.OlRuleType.olRuleReceive);

                if (rule != null)
                {
                    // From condition
                    rule.Conditions.SenderAddress.Address = new string[] { domain };
                    rule.Conditions.SenderAddress.Enabled = true;

                    // Rule Actions
                    rule.Actions.Stop.Enabled = true;

                    if (createMoveRule)
                    {
                        rule.Actions.MoveToFolder.Folder = moveFolder;
                        rule.Actions.MoveToFolder.Enabled = true;
                    }

                    if (createCopyRule)
                    {
                        rule.Actions.CopyToFolder.Folder = copyFolder;
                        rule.Actions.CopyToFolder.Enabled = true;
                    }

                    rule.Enabled = true;

                    ruleSet?.Save(false);
                    rules.Add(domain, rule);
                }
            }
            catch
            {
                FunctionHelper.ConsumeFinalReleaseNullComObject(rule);

                throw;
            }
            finally
            {
                FunctionHelper.ConsumeFinalReleaseNullComObject(ruleSet);
            }
        }

        private Task<Dictionary<string, Outlook.Rule>> GetDomainRules(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                ReportStatus?.Invoke(this, "Getting domain rules...");

                Dictionary<string, Outlook.Rule> matchingRules = new Dictionary<string, Outlook.Rule>();
                Outlook.Rules rules = null;
                try
                {
                    rules = sortFolder?.Folder.Store?.GetRules();

                    for (int i = 1; i <= (rules?.Count ?? 0); i++)
                    {
                        cancellationToken.ThrowIfCancellationRequested(); //Cancellation handling

                        Outlook.Rule rule = rules[i];

                        bool isDomainRule = default;
                        try
                        {
                            if (IsDomainRule(rule))
                            {
                                isDomainRule = true;
                                matchingRules.Add(rule.Name, rule);
                            }
                        }
                        finally
                        {
                            //Only cleanup rule if we didn't add it to our set
                            if (!isDomainRule) FunctionHelper.ConsumeFinalReleaseNullComObject(rule);
                        }
                    }
                }
                finally
                {
                    FunctionHelper.ConsumeFinalReleaseNullComObject(rules);

                    ReportStatus?.Invoke(this, "Getting domain rules complete");
                }

                return matchingRules;
            }, cancellationToken);
        }

        private (Outlook.MAPIFolder moveDestination, Outlook.MAPIFolder copyDestination) GetFoldersFromDomainRule(Dictionary<string, Outlook.Rule> rules, string domain)
        {
            if (rules?.ContainsKey(domain) ?? false)
            {
                return (rules[domain]?.Actions?.MoveToFolder?.Folder, rules[domain]?.Actions?.CopyToFolder?.Folder);
            }

            return (null, null);
        }

        #endregion Rules logic

        private Task SortItems(bool createRules,
                               bool createMoveRule,
                               bool createCopyRule,
                               bool performAction,
                               bool performMoveAction,
                               bool performCopyAction,
                               bool createMoveSubdirectories,
                               bool createCopySubdirectories,
                               Dictionary<string, Outlook.Rule> rules,
                               CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                ReportStatus?.Invoke(this, "Sorting items...");

                try
                {
                    int? itemsToSort = sortFolder?.GetItemCount(ref defaultNamespace);

                    for (int i = 1; i <= (itemsToSort ?? 0); i++)
                    {
                        ReportProgress?.Invoke(this, ((100M / (itemsToSort ?? 1)) * i));

                        cancellationToken.ThrowIfCancellationRequested(); //Cancellation handling

                        if (sortFolder?.GetItem(i, ref defaultNamespace) is Outlook.MailItem item)
                        {
                            try
                            {
                                if (item != null)
                                {
                                    string senderAddress = GetSenderAddress(item);
                                    string senderDomain = GetDomain(senderAddress);

                                    if (senderDomain != null)
                                    {
                                        Outlook.MAPIFolder moveDestination = null;
                                        Outlook.MAPIFolder copyDestination = null;

                                        try
                                        {
                                            //Always read folders from rules if available
                                            // this will handle users moving folders around after rules were
                                            // initially created, as rules update with folder moves
                                            if (rules?.ContainsKey(senderDomain) ?? false) //createRules &&
                                            {
                                                //Rule exists, try and get domain folders
                                                (moveDestination, copyDestination) = GetFoldersFromDomainRule(rules, senderDomain);
                                            }

                                            if ((createMoveRule | performMoveAction) && moveDestination == null && createMoveSubdirectories)
                                            {
                                                moveDestination = GetOrCreateDomainSubfolder(moveToFolder, senderDomain);
                                            }

                                            if ((createCopyRule | performCopyAction) && copyDestination == null && createCopySubdirectories)
                                            {
                                                copyDestination = GetOrCreateDomainSubfolder(copyToFolder, senderDomain);
                                            }

                                            //Create new rule
                                            if (createRules && (!rules?.ContainsKey(senderDomain) ?? false))
                                            {
                                                ReportStatus?.Invoke(this, $"Creating new rule - {senderDomain}");

                                                CreateDomainRule(rules,
                                                                 senderDomain,
                                                                 createMoveRule,
                                                                 createCopyRule,
                                                                 createMoveSubdirectories ? moveDestination : moveToFolder?.Folder,
                                                                 createCopySubdirectories ? copyDestination : copyToFolder?.Folder);
                                            }

                                            //Process items
                                            if (performAction)
                                            {
                                                if (performCopyAction)
                                                {
                                                    Outlook.MailItem copy = null;
                                                    try
                                                    {
                                                        //Create a copy and move to new folder
                                                        copy = item.Copy();
                                                        copy.Move(createCopySubdirectories ? copyDestination : copyToFolder?.Folder);
                                                    }
                                                    catch { }
                                                    finally
                                                    {
                                                        FunctionHelper.ConsumeFinalReleaseNullComObject(copy);
                                                    }
                                                }

                                                if (performMoveAction)
                                                {
                                                    try
                                                    {
                                                        item.Move(createMoveSubdirectories ? moveDestination : moveToFolder?.Folder);
                                                    }
                                                    catch { }
                                                }
                                            }
                                        }
                                        finally
                                        {
                                            FunctionHelper.ConsumeFinalReleaseNullComObject(moveDestination);
                                            FunctionHelper.ConsumeFinalReleaseNullComObject(copyDestination);
                                        }
                                    }
                                }
                            }
                            finally
                            {
                                FunctionHelper.ConsumeFinalReleaseNullComObject(item);
                            }
                        }
                    }
                }
                finally
                {
                    FunctionHelper.ConsumeFinalReleaseNullComObject(rules);

                    ReportStatus?.Invoke(this, "Sorting items complete");
                }
            }, cancellationToken);
        }

        #region Folder logic

        public bool SelectCopyFolder()
        {
            CleanupCopyFolder();

            copyToFolder = new MAPIFolderEx(defaultNamespace?.PickFolder());

            return copyToFolder != null;
        }

        public bool SelectMoveFolder()
        {
            CleanupMoveFolder();

            moveToFolder = new MAPIFolderEx(defaultNamespace?.PickFolder());

            return moveToFolder != null;
        }

        public bool SelectSortFolder()
        {
            CleanupSortFolder();

            sortFolder = new MAPIFolderEx(defaultNamespace?.PickFolder());

            return sortFolder != null;
        }

        private void CleanupCopyFolder()
        {
            FunctionHelper.ConsumeFinalReleaseNullComObject(copyToFolder);

            copyToFolder = null;
        }

        private void CleanupMoveFolder()
        {
            FunctionHelper.ConsumeFinalReleaseNullComObject(moveToFolder);

            moveToFolder = null;
        }

        private void CleanupSortFolder()
        {
            FunctionHelper.ConsumeFinalReleaseNullComObject(sortFolder);

            sortFolder = null;
        }

        private bool DomainFolderExists(MAPIFolderEx rootFolder, string name)
        {
            return rootFolder?.GetSubFolderExists(name, ref defaultNamespace) ?? false;
        }

        private Outlook.MAPIFolder GetOrCreateDomainSubfolder(MAPIFolderEx rootFolder, string domain)
        {
            if (!DomainFolderExists(rootFolder, domain))
            {
                //ReportStatus?.Invoke(this, $"Creating new domain folder - {rootFolder?.FolderPath ?? string.Empty + '\\' + domain}");

                return rootFolder?.AddSubFolder(domain, ref defaultNamespace);
            }
            else
            {
                //No rule but folder exists, odd...
                return rootFolder?.GetSubFolder(domain, ref defaultNamespace);
            }
        }

        #endregion Folder logic

        #region Email address logic

        private string GetDomain(string email)
        {
            if (email?.Contains('@') ?? false)
            {
                //Domain including the @ symbol
                return email.Substring(email.IndexOf('@')).ToLower();
            }

            return null;
        }

        private string GetSenderAddress(Outlook.MailItem mail)
        {
            if (mail.SenderEmailType == "EX")
            {
                Outlook.AddressEntry sender = null;
                try
                {
                    sender = mail.Sender;
                    if (sender != null)
                    {
                        //Now we have an AddressEntry representing the Sender
                        if (sender.AddressEntryUserType == Outlook.OlAddressEntryUserType.olExchangeUserAddressEntry ||
                            sender.AddressEntryUserType == Outlook.OlAddressEntryUserType.olExchangeRemoteUserAddressEntry)
                        {
                            //Use the ExchangeUser object PrimarySMTPAddress
                            Outlook.ExchangeUser exchangeUser = null;
                            try
                            {
                                exchangeUser = sender.GetExchangeUser();
                                if (exchangeUser != null)
                                {
                                    return exchangeUser.PrimarySmtpAddress.ToLower();
                                }
                            }
                            finally
                            {
                                FunctionHelper.ConsumeFinalReleaseNullComObject(exchangeUser);
                            }
                        }
                        else
                        {
                            return (sender.PropertyAccessor.GetProperty(TAGS_SMTP_ADDRESS) as string).ToLower();
                        }
                    }
                }
                finally
                {
                    FunctionHelper.ConsumeFinalReleaseNullComObject(sender);
                }
            }
            else
            {
                return mail.SenderEmailAddress.ToLower();
            }

            return null;
        }

        private bool IsDomainRule(Outlook.Rule rule)
        {
            return rule.RuleType == Outlook.OlRuleType.olRuleReceive &&
                   Regex.IsMatch(rule.Name, RULE_DOMAIN_NAME_MATCH);
        }

        #endregion Email address logic
    }
}