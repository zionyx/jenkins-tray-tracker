namespace Jenkins.Tray.UI
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
            this.serversSettingsControl = new Jenkins.Tray.UI.Controls.ServersSettingsControl();
            this.notificationsTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.notificationsSettingsControl = new Jenkins.Tray.UI.Controls.NotificationsSettingsControl();
            this.generalTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.checkForUpdatesCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.refreshLabel = new DevExpress.XtraEditors.LabelControl();
            this.refreshSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.updateMainWindowIconCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.integrateWithClaimPluginCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.serversTabPage.SuspendLayout();
            this.notificationsTabPage.SuspendLayout();
            this.generalTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkForUpdatesCheckEdit.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.refreshSpinEdit.Properties)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updateMainWindowIconCheckEdit.Properties)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.integrateWithClaimPluginCheckEdit.Properties)).BeginInit();
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
            this.notificationsTabPage.Text = "Sound notifications";
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
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 22);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(713, 380);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.checkForUpdatesCheckEdit, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 97);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(707, 25);
            this.tableLayoutPanel5.TabIndex = 6;
            // 
            // checkForUpdatesCheckEdit
            // 
            this.checkForUpdatesCheckEdit.Location = new System.Drawing.Point(3, 3);
            this.checkForUpdatesCheckEdit.Name = "checkForUpdatesCheckEdit";
            this.checkForUpdatesCheckEdit.Properties.AutoWidth = true;
            this.checkForUpdatesCheckEdit.Properties.Caption = "&Check for updates periodically";
            this.checkForUpdatesCheckEdit.Size = new System.Drawing.Size(167, 19);
            this.checkForUpdatesCheckEdit.TabIndex = 0;
            this.checkForUpdatesCheckEdit.CheckedChanged += new System.EventHandler(this.checkForUpdatesCheckEdit_CheckedChanged);
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
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.updateMainWindowIconCheckEdit, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 35);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(707, 25);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // updateMainWindowIconCheckEdit
            // 
            this.updateMainWindowIconCheckEdit.Location = new System.Drawing.Point(3, 3);
            this.updateMainWindowIconCheckEdit.Name = "updateMainWindowIconCheckEdit";
            this.updateMainWindowIconCheckEdit.Properties.AutoWidth = true;
            this.updateMainWindowIconCheckEdit.Properties.Caption = "&Update the main window\'s icon";
            this.updateMainWindowIconCheckEdit.Size = new System.Drawing.Size(170, 19);
            this.updateMainWindowIconCheckEdit.TabIndex = 5;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.integrateWithClaimPluginCheckEdit, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 66);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(707, 25);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // integrateWithClaimPluginCheckEdit
            // 
            this.integrateWithClaimPluginCheckEdit.Location = new System.Drawing.Point(3, 3);
            this.integrateWithClaimPluginCheckEdit.Name = "integrateWithClaimPluginCheckEdit";
            this.integrateWithClaimPluginCheckEdit.Properties.AutoWidth = true;
            this.integrateWithClaimPluginCheckEdit.Properties.Caption = "Integrate with Clai&m plugin (experimental)";
            this.integrateWithClaimPluginCheckEdit.Size = new System.Drawing.Size(224, 19);
            this.integrateWithClaimPluginCheckEdit.TabIndex = 0;
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
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkForUpdatesCheckEdit.Properties)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.refreshSpinEdit.Properties)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.updateMainWindowIconCheckEdit.Properties)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.integrateWithClaimPluginCheckEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraTab.XtraTabPage serversTabPage;
        private DevExpress.XtraTab.XtraTabPage notificationsTabPage;
        private Jenkins.Tray.UI.Controls.ServersSettingsControl serversSettingsControl;
        private Jenkins.Tray.UI.Controls.NotificationsSettingsControl notificationsSettingsControl;
        private DevExpress.XtraTab.XtraTabPage generalTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.SpinEdit refreshSpinEdit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraEditors.LabelControl refreshLabel;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private DevExpress.XtraEditors.CheckEdit updateMainWindowIconCheckEdit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private DevExpress.XtraEditors.CheckEdit integrateWithClaimPluginCheckEdit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private DevExpress.XtraEditors.CheckEdit checkForUpdatesCheckEdit;

    }
}