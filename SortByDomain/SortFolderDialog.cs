using SortByDomain.Helpers;
using System;
using System.Threading;
using System.Windows.Forms;

namespace SortByDomain
{
    public partial class SortFolderDialog : Form
    {
        private readonly EmailSorter sorter;
        private readonly SynchronizationContext uiContext;

        public SortFolderDialog()
        {
            InitializeComponent();

            uiContext = SynchronizationContext.Current;

            sorter = new EmailSorter();
            sorter.ReportProgress += Sorter_ReportProgress;
            sorter.ReportStatus += Sorter_ReportStatus;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                FunctionHelper.ConsumeException(() => sorter?.Dispose());

                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (sorter?.Sorting ?? false)
            {
                sorter?.Cancel();
            }
            else
            {
                this.Close();
            }
        }

        private void BtnSelectCopyFolder_Click(object sender, EventArgs e)
        {
            sorter?.SelectCopyFolder();

            TxtCopyFolder.Text = sorter?.CopyToFolderName ?? string.Empty;

            UpdateSortable();
        }

        private void BtnSelectMoveFolder_Click(object sender, EventArgs e)
        {
            sorter?.SelectMoveFolder();

            TxtMoveFolder.Text = sorter?.MoveToFolderName ?? string.Empty;

            UpdateSortable();
        }

        private void BtnSelectSortFolder_Click(object sender, EventArgs e)
        {
            sorter?.SelectSortFolder();

            TxtSortFolder.Text = sorter?.SortFolderName ?? string.Empty;

            UpdateSortable();
        }

        private async void BtnSort_Click(object sender, EventArgs e)
        {
            try
            {
                if (sorter != null)
                {
                    GpbSortConfiguration.Enabled = false;
                    GpbAction.Enabled = false;
                    BtnSort.Enabled = false;

                    await sorter.Sort(ChkCreateRules.Checked,
                                      ChkMoveRule.Checked,
                                      ChkCopyRule.Checked,
                                      ChkPerformActions.Checked,
                                      ChkMoveAction.Checked,
                                      ChkCopyAction.Checked,
                                      ChkMoveSubdirectories.Checked,
                                      ChkCopySubdirectories.Checked).ConfigureAwait(true);
                }
            }
            finally
            {
                GpbSortConfiguration.Enabled = true;
                GpbAction.Enabled = true;
                BtnSort.Enabled = true;
            }
        }

        private void ChkCopyAction_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSortable();
        }

        private void ChkCopyRule_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSortable();
        }

        private void ChkCreateRules_CheckedChanged(object sender, EventArgs e)
        {
            ChkCopyRule.Enabled = ChkCreateRules.Checked;
            ChkMoveRule.Enabled = ChkCreateRules.Checked;

            UpdateSortable();
        }

        private void ChkMoveAction_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSortable();
        }

        private void ChkMoveRule_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSortable();
        }

        private void ChkPerformActions_CheckedChanged(object sender, EventArgs e)
        {
            ChkCopyAction.Enabled = ChkPerformActions.Checked;
            ChkMoveAction.Enabled = ChkPerformActions.Checked;

            UpdateSortable();
        }

        private void Sorter_ReportProgress(object sender, decimal e)
        {
            UpdateUI(() =>
            {
                PgbSortProgress.Value = (int)Math.Floor(e);
                lblProgessPercentage.Text = e.ToString("0.00\\%");
            });
        }

        private void Sorter_ReportStatus(object sender, string e)
        {
            UpdateUI(() => TxtProgressEvents.AppendText(e + Environment.NewLine));
        }

        private void SortFolderDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sorter?.Sorting ?? false)
            {
                if (MessageBox.Show("Cancel current sort?", "Sort running", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    sorter?.Cancel();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void UpdateSortable()
        {
            BtnSort.Enabled = sorter?.CanBeginSort(ChkCreateRules.Checked,
                                                   ChkMoveRule.Checked,
                                                   ChkCopyRule.Checked,
                                                   ChkPerformActions.Checked,
                                                   ChkMoveAction.Checked,
                                                   ChkCopyAction.Checked) ?? false;
        }

        private void UpdateUI(Action action)
        {
            uiContext.Post(new SendOrPostCallback(o => action()), null);
        }
    }
}