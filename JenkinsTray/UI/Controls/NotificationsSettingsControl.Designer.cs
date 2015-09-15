namespace Jenkins.Tray.UI.Controls
{
    partial class NotificationsSettingsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.notificationSettingsControl1 = new Jenkins.Tray.UI.Controls.NotificationSettingsControl();
            this.notificationSettingsControl2 = new Jenkins.Tray.UI.Controls.NotificationSettingsControl();
            this.notificationSettingsControl3 = new Jenkins.Tray.UI.Controls.NotificationSettingsControl();
            this.notificationSettingsControl4 = new Jenkins.Tray.UI.Controls.NotificationSettingsControl();
            this.enableSoundCheckBox = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.treatUnstableAsFailedCheckBox = new DevExpress.XtraEditors.CheckEdit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.enableSoundCheckBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treatUnstableAsFailedCheckBox.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.notificationSettingsControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.notificationSettingsControl2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.notificationSettingsControl3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.notificationSettingsControl4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.enableSoundCheckBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.treatUnstableAsFailedCheckBox, 0, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 25);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(552, 391);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // notificationSettingsControl1
            // 
            this.notificationSettingsControl1.AutoSize = true;
            this.notificationSettingsControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.notificationSettingsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notificationSettingsControl1.Location = new System.Drawing.Point(3, 34);
            this.notificationSettingsControl1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.notificationSettingsControl1.Name = "notificationSettingsControl1";
            this.notificationSettingsControl1.Size = new System.Drawing.Size(546, 60);
            this.notificationSettingsControl1.SoundPath = "";
            this.notificationSettingsControl1.Status = "Failed";
            this.notificationSettingsControl1.TabIndex = 1;
            // 
            // notificationSettingsControl2
            // 
            this.notificationSettingsControl2.AutoSize = true;
            this.notificationSettingsControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.notificationSettingsControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notificationSettingsControl2.Location = new System.Drawing.Point(3, 104);
            this.notificationSettingsControl2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.notificationSettingsControl2.Name = "notificationSettingsControl2";
            this.notificationSettingsControl2.Size = new System.Drawing.Size(546, 60);
            this.notificationSettingsControl2.SoundPath = "";
            this.notificationSettingsControl2.Status = "Fixed";
            this.notificationSettingsControl2.TabIndex = 2;
            // 
            // notificationSettingsControl3
            // 
            this.notificationSettingsControl3.AutoSize = true;
            this.notificationSettingsControl3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.notificationSettingsControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notificationSettingsControl3.Location = new System.Drawing.Point(3, 174);
            this.notificationSettingsControl3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.notificationSettingsControl3.Name = "notificationSettingsControl3";
            this.notificationSettingsControl3.Size = new System.Drawing.Size(546, 60);
            this.notificationSettingsControl3.SoundPath = "";
            this.notificationSettingsControl3.Status = "StillFailing";
            this.notificationSettingsControl3.TabIndex = 3;
            // 
            // notificationSettingsControl4
            // 
            this.notificationSettingsControl4.AutoSize = true;
            this.notificationSettingsControl4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.notificationSettingsControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notificationSettingsControl4.Location = new System.Drawing.Point(3, 244);
            this.notificationSettingsControl4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.notificationSettingsControl4.Name = "notificationSettingsControl4";
            this.notificationSettingsControl4.Size = new System.Drawing.Size(546, 60);
            this.notificationSettingsControl4.SoundPath = "";
            this.notificationSettingsControl4.Status = "Succeeded";
            this.notificationSettingsControl4.TabIndex = 3;
            // 
            // enableSoundCheckBox
            // 
            this.enableSoundCheckBox.Location = new System.Drawing.Point(3, 4);
            this.enableSoundCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.enableSoundCheckBox.Name = "enableSoundCheckBox";
            this.enableSoundCheckBox.Properties.Caption = "&Enable sound notifications";
            this.enableSoundCheckBox.Size = new System.Drawing.Size(175, 21);
            this.enableSoundCheckBox.TabIndex = 0;
            this.enableSoundCheckBox.CheckedChanged += new System.EventHandler(this.enableSoundCheckBox_CheckedChanged);
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.tableLayoutPanel1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(556, 418);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Configure sound notifications";
            // 
            // treatUnstableAsFailedCheckBox
            // 
            this.treatUnstableAsFailedCheckBox.Location = new System.Drawing.Point(3, 313);
            this.treatUnstableAsFailedCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.treatUnstableAsFailedCheckBox.Name = "treatUnstableAsFailedCheckBox";
            this.treatUnstableAsFailedCheckBox.Properties.Caption = "&Treat unstable as failed";
            this.treatUnstableAsFailedCheckBox.Size = new System.Drawing.Size(162, 21);
            this.treatUnstableAsFailedCheckBox.TabIndex = 9;
            this.treatUnstableAsFailedCheckBox.CheckedChanged += new System.EventHandler(this.treatUnstableAsFailedCheckBox_CheckedChanged);
            // 
            // NotificationsSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "NotificationsSettingsControl";
            this.Size = new System.Drawing.Size(556, 418);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.enableSoundCheckBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treatUnstableAsFailedCheckBox.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private NotificationSettingsControl notificationSettingsControl1;
        private NotificationSettingsControl notificationSettingsControl2;
        private NotificationSettingsControl notificationSettingsControl3;
        private NotificationSettingsControl notificationSettingsControl4;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckEdit enableSoundCheckBox;
        private DevExpress.XtraEditors.CheckEdit treatUnstableAsFailedCheckBox;
    }
}
