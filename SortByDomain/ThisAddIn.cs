using SortByDomain.Helpers;
using System;
using System.Diagnostics;
using Office = Microsoft.Office.Core;

namespace SortByDomain
{
    public partial class ThisAddIn
    {
        private const string EVENT_LOG_SOURCE = "Outlook";

        private Ribbon ribbon;

        protected override Office.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            ribbon = new Ribbon();

            return ribbon;
        }

        private void DisableAddIn()
        {
            if (ribbon != null)
            {
                ribbon.OnSortByFolderRequested -= Ribbon_OnSortByFolderRequested;
            }

            FunctionHelper.ConsumeFinalReleaseNullComObject(AddInHelper.AddInApplication);

            AddInHelper.AddInApplication = null;
        }

        private void EnableAddIn()
        {
            AddInHelper.AddInApplication = Application;

            if (ribbon != null)
            {
                ribbon.OnSortByFolderRequested += Ribbon_OnSortByFolderRequested;
            }
        }

        private void Ribbon_OnSortByFolderRequested(object sender, EventArgs e)
        {
            using (var sorter = new SortFolderDialog())
            {
                sorter.ShowDialog();
            }
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            DisableAddIn();
        }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            try
            {
                //Not actually using enabler yet
                if (Properties.Settings.Default.AddInEnabled)
                {
                    EnableAddIn();
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(EVENT_LOG_SOURCE, ex.Message, EventLogEntryType.Error, 1);
                throw;
            }
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion VSTO generated code
    }
}