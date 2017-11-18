namespace JenkinsTray.UI
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.serversTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.serversSettingsControl = new JenkinsTray.UI.Controls.ServersSettingsControl();
            this.notificationsTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.notificationsSettingsControl = new JenkinsTray.UI.Controls.NotificationsSettingsControl();
            this.generalTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.refreshLabel = new DevExpress.XtraEditors.LabelControl();
            this.refreshSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.showFoldersCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.checkForUpdatesCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.updateMainWindowIconCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.integrateWithClaimPluginCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.showProjectDisplayNameCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.serversTabPage.SuspendLayout();
            this.notificationsTabPage.SuspendLayout();
            this.generalTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.refreshSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.showFoldersCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkForUpdatesCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updateMainWindowIconCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.integrateWithClaimPluginCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.showProjectDisplayNameCheckEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.serversTabPage;
            this.tabControl.Size = new System.Drawing.Size(746, 411);
            this.tabControl.TabIndex = 0;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.serversTabPage,
            this.notificationsTabPage,
            this.generalTabPage});
            // 
            // serversTabPage
            // 
            this.serversTabPage.Controls.Add(this.serversSettingsControl);
            this.serversTabPage.Name = "serversTabPage";
            this.serversTabPage.Size = new System.Drawing.Size(717, 404);
            this.serversTabPage.Text = "Servers and projects";
            // 
            // serversSettingsControl
            // 
            this.serversSettingsControl.AutoSize = true;
            this.serversSettingsControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.serversSettingsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serversSettingsControl.Location = new System.Drawing.Point(0, 0);
            this.serversSettingsControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.serversSettingsControl.LookAndFeel.UseWindowsXPTheme = true;
            this.serversSettingsControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.serversSettingsControl.Name = "serversSettingsControl";
            this.serversSettingsControl.Size = new System.Drawing.Size(717, 404);
            this.serversSettingsControl.TabIndex = 0;
            // 
            // notificationsTabPage
            // 
            this.notificationsTabPage.Controls.Add(this.notificationsSettingsControl);
            this.notificationsTabPage.Name = "notificationsTabPage";
            this.notificationsTabPage.Size = new System.Drawing.Size(717, 404);
            this.notificationsTabPage.Text = "Notifications";
            // 
            // notificationsSettingsControl
            // 
            this.notificationsSettingsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notificationsSettingsControl.Location = new System.Drawing.Point(0, 0);
            this.notificationsSettingsControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.notificationsSettingsControl.Name = "notificationsSettingsControl";
            this.notificationsSettingsControl.Size = new System.Drawing.Size(717, 404);
            this.notificationsSettingsControl.TabIndex = 0;
            // 
            // generalTabPage
            // 
            this.generalTabPage.Controls.Add(this.groupControl1);
            this.generalTabPage.Name = "generalTabPage";
            this.generalTabPage.Size = new System.Drawing.Size(717, 404);
            this.generalTabPage.Text = "General";
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.tableLayoutPanel2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(717, 404);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "General options";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.showFoldersCheckEdit, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.checkForUpdatesCheckEdit, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.updateMainWindowIconCheckEdit, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.integrateWithClaimPluginCheckEdit, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.showProjectDisplayNameCheckEdit, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 22);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(713, 380);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.refreshLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.refreshSpinEdit, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(707, 26);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // refreshLabel
            // 
            this.refreshLabel.Location = new System.Drawing.Point(3, 6);
            this.refreshLabel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.refreshLabel.Name = "refreshLabel";
            this.refreshLabel.Size = new System.Drawing.Size(97, 13);
            this.refreshLabel.TabIndex = 4;
            this.refreshLabel.Text = "&Refresh interval (s):";
            // 
            // refreshSpinEdit
            // 
            this.refreshSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.refreshSpinEdit.Location = new System.Drawing.Point(106, 3);
            this.refreshSpinEdit.Name = "refreshSpinEdit";
            this.refreshSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.refreshSpinEdit.Properties.Mask.EditMask = "N0";
            this.refreshSpinEdit.Properties.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.refreshSpinEdit.Size = new System.Drawing.Size(100, 20);
            this.refreshSpinEdit.TabIndex = 1;
            // 
            // showFoldersCheckEdit
            // 
            this.showFoldersCheckEdit.Location = new System.Drawing.Point(3, 110);
            this.showFoldersCheckEdit.Name = "showFoldersCheckEdit";
            this.showFoldersCheckEdit.Properties.AutoWidth = true;
            this.showFoldersCheckEdit.Properties.Caption = "Show &folders";
            this.showFoldersCheckEdit.Size = new System.Drawing.Size(85, 19);
            this.showFoldersCheckEdit.TabIndex = 0;
            this.showFoldersCheckEdit.CheckedChanged += new System.EventHandler(this.showFoldersCheckEdit_CheckedChanged);
            // 
            // checkForUpdatesCheckEdit
            // 
            this.checkForUpdatesCheckEdit.Location = new System.Drawing.Point(3, 135);
            this.checkForUpdatesCheckEdit.Name = "checkForUpdatesCheckEdit";
            this.checkForUpdatesCheckEdit.Properties.AutoWidth = true;
            this.checkForUpdatesCheckEdit.Properties.Caption = "&Check for updates periodically";
            this.checkForUpdatesCheckEdit.Size = new System.Drawing.Size(167, 19);
            this.checkForUpdatesCheckEdit.TabIndex = 0;
            this.checkForUpdatesCheckEdit.CheckedChanged += new System.EventHandler(this.checkForUpdatesCheckEdit_CheckedChanged);
            // 
            // updateMainWindowIconCheckEdit
            // 
            this.updateMainWindowIconCheckEdit.Location = new System.Drawing.Point(3, 35);
            this.updateMainWindowIconCheckEdit.Name = "updateMainWindowIconCheckEdit";
            this.updateMainWindowIconCheckEdit.Properties.AutoWidth = true;
            this.updateMainWindowIconCheckEdit.Properties.Caption = "&Update the main window\'s icon";
            this.updateMainWindowIconCheckEdit.Size = new System.Drawing.Size(170, 19);
            this.updateMainWindowIconCheckEdit.TabIndex = 5;
            // 
            // integrateWithClaimPluginCheckEdit
            // 
            this.integrateWithClaimPluginCheckEdit.Location = new System.Drawing.Point(3, 60);
            this.integrateWithClaimPluginCheckEdit.Name = "integrateWithClaimPluginCheckEdit";
            this.integrateWithClaimPluginCheckEdit.Properties.AutoWidth = true;
            this.integrateWithClaimPluginCheckEdit.Properties.Caption = "Integrate with Clai&m plugin (experimental)";
            this.integrateWithClaimPluginCheckEdit.Size = new System.Drawing.Size(224, 19);
            this.integrateWithClaimPluginCheckEdit.TabIndex = 0;
            // 
            // showProjectDisplayNameCheckEdit
            // 
            this.showProjectDisplayNameCheckEdit.Location = new System.Drawing.Point(3, 85);
            this.showProjectDisplayNameCheckEdit.Name = "showProjectDisplayNameCheckEdit";
            this.showProjectDisplayNameCheckEdit.Properties.AutoWidth = true;
            this.showProjectDisplayNameCheckEdit.Properties.Caption = "&Show display name over project name in Main UI";
            this.showProjectDisplayNameCheckEdit.Size = new System.Drawing.Size(255, 19);
            this.showProjectDisplayNameCheckEdit.TabIndex = 0;
            this.showProjectDisplayNameCheckEdit.CheckedChanged += new System.EventHandler(this.checkForUpdatesCheckEdit_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 411);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "Jenkins Tray - Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.serversTabPage.ResumeLayout(false);
            this.serversTabPage.PerformLayout();
            this.notificationsTabPage.ResumeLayout(false);
            this.generalTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.refreshSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.showFoldersCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkForUpdatesCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updateMainWindowIconCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.integrateWithClaimPluginCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.showProjectDisplayNameCheckEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraTab.XtraTabPage serversTabPage;
        private DevExpress.XtraTab.XtraTabPage notificationsTabPage;
        private JenkinsTray.UI.Controls.ServersSettingsControl serversSettingsControl;
        private JenkinsTray.UI.Controls.NotificationsSettingsControl notificationsSettingsControl;
        private DevExpress.XtraTab.XtraTabPage generalTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.SpinEdit refreshSpinEdit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraEditors.LabelControl refreshLabel;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckEdit updateMainWindowIconCheckEdit;
        private DevExpress.XtraEditors.CheckEdit integrateWithClaimPluginCheckEdit;
        private DevExpress.XtraEditors.CheckEdit checkForUpdatesCheckEdit;
        private DevExpress.XtraEditors.CheckEdit showProjectDisplayNameCheckEdit;
        private DevExpress.XtraEditors.CheckEdit showFoldersCheckEdit;
    }
}