
namespace SortByDomain
{
    partial class SortFolderDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SortFolderDialog));
            this.GpbSortConfiguration = new System.Windows.Forms.GroupBox();
            this.ChkCopySubdirectories = new System.Windows.Forms.CheckBox();
            this.ChkMoveSubdirectories = new System.Windows.Forms.CheckBox();
            this.TxtCopyFolder = new System.Windows.Forms.TextBox();
            this.BtnSelectCopyFolder = new System.Windows.Forms.Button();
            this.TxtMoveFolder = new System.Windows.Forms.TextBox();
            this.TxtSortFolder = new System.Windows.Forms.TextBox();
            this.BtnSelectMoveFolder = new System.Windows.Forms.Button();
            this.BtnSelectSortFolder = new System.Windows.Forms.Button();
            this.GpbProgress = new System.Windows.Forms.GroupBox();
            this.lblProgessPercentage = new System.Windows.Forms.Label();
            this.TxtProgressEvents = new System.Windows.Forms.TextBox();
            this.PgbSortProgress = new System.Windows.Forms.ProgressBar();
            this.BtnSort = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.ChkMoveAction = new System.Windows.Forms.CheckBox();
            this.GpbAction = new System.Windows.Forms.GroupBox();
            this.ChkPerformActions = new System.Windows.Forms.CheckBox();
            this.ChkCopyAction = new System.Windows.Forms.CheckBox();
            this.GpbRules = new System.Windows.Forms.GroupBox();
            this.ChkCopyRule = new System.Windows.Forms.CheckBox();
            this.ChkCreateRules = new System.Windows.Forms.CheckBox();
            this.ChkMoveRule = new System.Windows.Forms.CheckBox();
            this.GpbSortConfiguration.SuspendLayout();
            this.GpbProgress.SuspendLayout();
            this.GpbAction.SuspendLayout();
            this.GpbRules.SuspendLayout();
            this.SuspendLayout();
            // 
            // GpbSortConfiguration
            // 
            this.GpbSortConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GpbSortConfiguration.Controls.Add(this.ChkCopySubdirectories);
            this.GpbSortConfiguration.Controls.Add(this.ChkMoveSubdirectories);
            this.GpbSortConfiguration.Controls.Add(this.TxtCopyFolder);
            this.GpbSortConfiguration.Controls.Add(this.BtnSelectCopyFolder);
            this.GpbSortConfiguration.Controls.Add(this.TxtMoveFolder);
            this.GpbSortConfiguration.Controls.Add(this.TxtSortFolder);
            this.GpbSortConfiguration.Controls.Add(this.BtnSelectMoveFolder);
            this.GpbSortConfiguration.Controls.Add(this.BtnSelectSortFolder);
            this.GpbSortConfiguration.Location = new System.Drawing.Point(12, 12);
            this.GpbSortConfiguration.Name = "GpbSortConfiguration";
            this.GpbSortConfiguration.Size = new System.Drawing.Size(559, 100);
            this.GpbSortConfiguration.TabIndex = 0;
            this.GpbSortConfiguration.TabStop = false;
            this.GpbSortConfiguration.Text = "Sort configuration";
            // 
            // ChkCopySubdirectories
            // 
            this.ChkCopySubdirectories.AutoSize = true;
            this.ChkCopySubdirectories.Location = new System.Drawing.Point(420, 75);
            this.ChkCopySubdirectories.Name = "ChkCopySubdirectories";
            this.ChkCopySubdirectories.Size = new System.Drawing.Size(133, 17);
            this.ChkCopySubdirectories.TabIndex = 7;
            this.ChkCopySubdirectories.Text = "Domain sub-directories";
            this.ChkCopySubdirectories.UseVisualStyleBackColor = true;
            // 
            // ChkMoveSubdirectories
            // 
            this.ChkMoveSubdirectories.AutoSize = true;
            this.ChkMoveSubdirectories.Checked = true;
            this.ChkMoveSubdirectories.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkMoveSubdirectories.Location = new System.Drawing.Point(420, 49);
            this.ChkMoveSubdirectories.Name = "ChkMoveSubdirectories";
            this.ChkMoveSubdirectories.Size = new System.Drawing.Size(133, 17);
            this.ChkMoveSubdirectories.TabIndex = 6;
            this.ChkMoveSubdirectories.Text = "Domain sub-directories";
            this.ChkMoveSubdirectories.UseVisualStyleBackColor = true;
            // 
            // TxtCopyFolder
            // 
            this.TxtCopyFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtCopyFolder.Location = new System.Drawing.Point(137, 73);
            this.TxtCopyFolder.Name = "TxtCopyFolder";
            this.TxtCopyFolder.ReadOnly = true;
            this.TxtCopyFolder.Size = new System.Drawing.Size(277, 20);
            this.TxtCopyFolder.TabIndex = 5;
            // 
            // BtnSelectCopyFolder
            // 
            this.BtnSelectCopyFolder.Location = new System.Drawing.Point(6, 71);
            this.BtnSelectCopyFolder.Name = "BtnSelectCopyFolder";
            this.BtnSelectCopyFolder.Size = new System.Drawing.Size(125, 23);
            this.BtnSelectCopyFolder.TabIndex = 4;
            this.BtnSelectCopyFolder.Text = "Select copy folder";
            this.BtnSelectCopyFolder.UseVisualStyleBackColor = true;
            this.BtnSelectCopyFolder.Click += new System.EventHandler(this.BtnSelectCopyFolder_Click);
            // 
            // TxtMoveFolder
            // 
            this.TxtMoveFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtMoveFolder.Location = new System.Drawing.Point(137, 47);
            this.TxtMoveFolder.Name = "TxtMoveFolder";
            this.TxtMoveFolder.ReadOnly = true;
            this.TxtMoveFolder.Size = new System.Drawing.Size(277, 20);
            this.TxtMoveFolder.TabIndex = 3;
            // 
            // TxtSortFolder
            // 
            this.TxtSortFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtSortFolder.Location = new System.Drawing.Point(137, 21);
            this.TxtSortFolder.Name = "TxtSortFolder";
            this.TxtSortFolder.ReadOnly = true;
            this.TxtSortFolder.Size = new System.Drawing.Size(416, 20);
            this.TxtSortFolder.TabIndex = 2;
            // 
            // BtnSelectMoveFolder
            // 
            this.BtnSelectMoveFolder.Location = new System.Drawing.Point(6, 45);
            this.BtnSelectMoveFolder.Name = "BtnSelectMoveFolder";
            this.BtnSelectMoveFolder.Size = new System.Drawing.Size(125, 23);
            this.BtnSelectMoveFolder.TabIndex = 1;
            this.BtnSelectMoveFolder.Text = "Select move folder";
            this.BtnSelectMoveFolder.UseVisualStyleBackColor = true;
            this.BtnSelectMoveFolder.Click += new System.EventHandler(this.BtnSelectMoveFolder_Click);
            // 
            // BtnSelectSortFolder
            // 
            this.BtnSelectSortFolder.Location = new System.Drawing.Point(6, 19);
            this.BtnSelectSortFolder.Name = "BtnSelectSortFolder";
            this.BtnSelectSortFolder.Size = new System.Drawing.Size(125, 23);
            this.BtnSelectSortFolder.TabIndex = 0;
            this.BtnSelectSortFolder.Text = "Select folder to sort";
            this.BtnSelectSortFolder.UseVisualStyleBackColor = true;
            this.BtnSelectSortFolder.Click += new System.EventHandler(this.BtnSelectSortFolder_Click);
            // 
            // GpbProgress
            // 
            this.GpbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GpbProgress.Controls.Add(this.lblProgessPercentage);
            this.GpbProgress.Controls.Add(this.TxtProgressEvents);
            this.GpbProgress.Controls.Add(this.PgbSortProgress);
            this.GpbProgress.Location = new System.Drawing.Point(12, 118);
            this.GpbProgress.Name = "GpbProgress";
            this.GpbProgress.Size = new System.Drawing.Size(777, 325);
            this.GpbProgress.TabIndex = 1;
            this.GpbProgress.TabStop = false;
            this.GpbProgress.Text = "Progress";
            // 
            // lblProgessPercentage
            // 
            this.lblProgessPercentage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgessPercentage.BackColor = System.Drawing.Color.Transparent;
            this.lblProgessPercentage.CausesValidation = false;
            this.lblProgessPercentage.Location = new System.Drawing.Point(719, 296);
            this.lblProgessPercentage.Name = "lblProgessPercentage";
            this.lblProgessPercentage.Size = new System.Drawing.Size(52, 23);
            this.lblProgessPercentage.TabIndex = 8;
            this.lblProgessPercentage.Text = "100.00%";
            this.lblProgessPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtProgressEvents
            // 
            this.TxtProgressEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtProgressEvents.Location = new System.Drawing.Point(6, 19);
            this.TxtProgressEvents.Multiline = true;
            this.TxtProgressEvents.Name = "TxtProgressEvents";
            this.TxtProgressEvents.ReadOnly = true;
            this.TxtProgressEvents.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtProgressEvents.Size = new System.Drawing.Size(765, 271);
            this.TxtProgressEvents.TabIndex = 1;
            this.TxtProgressEvents.WordWrap = false;
            // 
            // PgbSortProgress
            // 
            this.PgbSortProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PgbSortProgress.Location = new System.Drawing.Point(6, 296);
            this.PgbSortProgress.Name = "PgbSortProgress";
            this.PgbSortProgress.Size = new System.Drawing.Size(707, 23);
            this.PgbSortProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.PgbSortProgress.TabIndex = 0;
            // 
            // BtnSort
            // 
            this.BtnSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSort.Enabled = false;
            this.BtnSort.Location = new System.Drawing.Point(624, 449);
            this.BtnSort.Name = "BtnSort";
            this.BtnSort.Size = new System.Drawing.Size(75, 23);
            this.BtnSort.TabIndex = 2;
            this.BtnSort.Text = "Sort";
            this.BtnSort.UseVisualStyleBackColor = true;
            this.BtnSort.Click += new System.EventHandler(this.BtnSort_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.Location = new System.Drawing.Point(705, 449);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 3;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ChkMoveAction
            // 
            this.ChkMoveAction.AutoSize = true;
            this.ChkMoveAction.Enabled = false;
            this.ChkMoveAction.Location = new System.Drawing.Point(6, 49);
            this.ChkMoveAction.Name = "ChkMoveAction";
            this.ChkMoveAction.Size = new System.Drawing.Size(80, 17);
            this.ChkMoveAction.TabIndex = 5;
            this.ChkMoveAction.Text = "Move items";
            this.ChkMoveAction.UseVisualStyleBackColor = true;
            this.ChkMoveAction.CheckedChanged += new System.EventHandler(this.ChkMoveAction_CheckedChanged);
            // 
            // GpbAction
            // 
            this.GpbAction.Controls.Add(this.ChkPerformActions);
            this.GpbAction.Controls.Add(this.ChkCopyAction);
            this.GpbAction.Controls.Add(this.ChkMoveAction);
            this.GpbAction.Location = new System.Drawing.Point(682, 12);
            this.GpbAction.Name = "GpbAction";
            this.GpbAction.Size = new System.Drawing.Size(107, 100);
            this.GpbAction.TabIndex = 6;
            this.GpbAction.TabStop = false;
            this.GpbAction.Text = "Action";
            // 
            // ChkPerformActions
            // 
            this.ChkPerformActions.AutoSize = true;
            this.ChkPerformActions.Location = new System.Drawing.Point(6, 23);
            this.ChkPerformActions.Name = "ChkPerformActions";
            this.ChkPerformActions.Size = new System.Drawing.Size(99, 17);
            this.ChkPerformActions.TabIndex = 7;
            this.ChkPerformActions.Text = "Perform actions";
            this.ChkPerformActions.UseVisualStyleBackColor = true;
            this.ChkPerformActions.CheckedChanged += new System.EventHandler(this.ChkPerformActions_CheckedChanged);
            // 
            // ChkCopyAction
            // 
            this.ChkCopyAction.AutoSize = true;
            this.ChkCopyAction.Enabled = false;
            this.ChkCopyAction.Location = new System.Drawing.Point(6, 75);
            this.ChkCopyAction.Name = "ChkCopyAction";
            this.ChkCopyAction.Size = new System.Drawing.Size(77, 17);
            this.ChkCopyAction.TabIndex = 6;
            this.ChkCopyAction.Text = "Copy items";
            this.ChkCopyAction.UseVisualStyleBackColor = true;
            this.ChkCopyAction.CheckedChanged += new System.EventHandler(this.ChkCopyAction_CheckedChanged);
            // 
            // GpbRules
            // 
            this.GpbRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GpbRules.Controls.Add(this.ChkCopyRule);
            this.GpbRules.Controls.Add(this.ChkCreateRules);
            this.GpbRules.Controls.Add(this.ChkMoveRule);
            this.GpbRules.Location = new System.Drawing.Point(577, 12);
            this.GpbRules.Name = "GpbRules";
            this.GpbRules.Size = new System.Drawing.Size(99, 100);
            this.GpbRules.TabIndex = 7;
            this.GpbRules.TabStop = false;
            this.GpbRules.Text = "Rules";
            // 
            // ChkCopyRule
            // 
            this.ChkCopyRule.AutoSize = true;
            this.ChkCopyRule.Checked = true;
            this.ChkCopyRule.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkCopyRule.Location = new System.Drawing.Point(6, 75);
            this.ChkCopyRule.Name = "ChkCopyRule";
            this.ChkCopyRule.Size = new System.Drawing.Size(77, 17);
            this.ChkCopyRule.TabIndex = 6;
            this.ChkCopyRule.Text = "Copy items";
            this.ChkCopyRule.UseVisualStyleBackColor = true;
            this.ChkCopyRule.CheckedChanged += new System.EventHandler(this.ChkCopyRule_CheckedChanged);
            // 
            // ChkCreateRules
            // 
            this.ChkCreateRules.AutoSize = true;
            this.ChkCreateRules.Checked = true;
            this.ChkCreateRules.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkCreateRules.Location = new System.Drawing.Point(6, 23);
            this.ChkCreateRules.Name = "ChkCreateRules";
            this.ChkCreateRules.Size = new System.Drawing.Size(82, 17);
            this.ChkCreateRules.TabIndex = 4;
            this.ChkCreateRules.Text = "Create rules";
            this.ChkCreateRules.UseVisualStyleBackColor = true;
            this.ChkCreateRules.CheckedChanged += new System.EventHandler(this.ChkCreateRules_CheckedChanged);
            // 
            // ChkMoveRule
            // 
            this.ChkMoveRule.AutoSize = true;
            this.ChkMoveRule.Checked = true;
            this.ChkMoveRule.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkMoveRule.Location = new System.Drawing.Point(6, 49);
            this.ChkMoveRule.Name = "ChkMoveRule";
            this.ChkMoveRule.Size = new System.Drawing.Size(80, 17);
            this.ChkMoveRule.TabIndex = 5;
            this.ChkMoveRule.Text = "Move items";
            this.ChkMoveRule.UseVisualStyleBackColor = true;
            this.ChkMoveRule.CheckedChanged += new System.EventHandler(this.ChkMoveRule_CheckedChanged);
            // 
            // SortFolderDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 484);
            this.Controls.Add(this.GpbRules);
            this.Controls.Add(this.GpbAction);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnSort);
            this.Controls.Add(this.GpbProgress);
            this.Controls.Add(this.GpbSortConfiguration);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SortFolderDialog";
            this.Text = "Sort emails";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SortFolderDialog_FormClosing);
            this.GpbSortConfiguration.ResumeLayout(false);
            this.GpbSortConfiguration.PerformLayout();
            this.GpbProgress.ResumeLayout(false);
            this.GpbProgress.PerformLayout();
            this.GpbAction.ResumeLayout(false);
            this.GpbAction.PerformLayout();
            this.GpbRules.ResumeLayout(false);
            this.GpbRules.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GpbSortConfiguration;
        private System.Windows.Forms.TextBox TxtMoveFolder;
        private System.Windows.Forms.TextBox TxtSortFolder;
        private System.Windows.Forms.Button BtnSelectMoveFolder;
        private System.Windows.Forms.Button BtnSelectSortFolder;
        private System.Windows.Forms.GroupBox GpbProgress;
        private System.Windows.Forms.TextBox TxtProgressEvents;
        private System.Windows.Forms.ProgressBar PgbSortProgress;
        private System.Windows.Forms.Button BtnSort;
        private System.Windows.Forms.Button BtnCancel;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.CheckBox ChkMoveAction;
        private System.Windows.Forms.GroupBox GpbAction;
        private System.Windows.Forms.CheckBox ChkCopyAction;
        private System.Windows.Forms.GroupBox GpbRules;
        private System.Windows.Forms.CheckBox ChkCopyRule;
        private System.Windows.Forms.CheckBox ChkCreateRules;
        private System.Windows.Forms.CheckBox ChkMoveRule;
        private System.Windows.Forms.TextBox TxtCopyFolder;
        private System.Windows.Forms.Button BtnSelectCopyFolder;
        private System.Windows.Forms.CheckBox ChkPerformActions;
        private System.Windows.Forms.Label lblProgessPercentage;
        private System.Windows.Forms.CheckBox ChkCopySubdirectories;
        private System.Windows.Forms.CheckBox ChkMoveSubdirectories;
    }
}