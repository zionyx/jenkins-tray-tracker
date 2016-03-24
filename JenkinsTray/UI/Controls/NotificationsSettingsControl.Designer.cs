namespace JenkinsTray.UI.Controls
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
            this.notificationSettingsControl1 = new JenkinsTray.UI.Controls.NotificationSettingsControl();
            this.notificationSettingsControl2 = new JenkinsTray.UI.Controls.NotificationSettingsControl();
            this.notificationSettingsControl3 = new JenkinsTray.UI.Controls.NotificationSettingsControl();
            this.notificationSettingsControl4 = new JenkinsTray.UI.Controls.NotificationSettingsControl();
            this.enableSoundCheckBox = new DevExpress.XtraEditors.CheckEdit();
            this.treatUnstableAsFailedCheckBox = new DevExpress.XtraEditors.CheckEdit();
            this.enableBalloonCheckBox = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.enableSoundCheckBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treatUnstableAsFailedCheckBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enableBalloonCheckBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.enableBalloonCheckBox, 0, 7);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 22);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(473, 316);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // notificationSettingsControl1
            // 
            this.notificationSettingsControl1.AutoSize = true;
            this.notificationSettingsControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.notificationSettingsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notificationSettingsControl1.Location = new System.Drawing.Point(3, 29);
            this.notificationSettingsControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.notificationSettingsControl1.Name = "notificationSettingsControl1";
            this.notificationSettingsControl1.Size = new System.Drawing.Size(467, 48);
            this.notificationSettingsControl1.SoundPath = "";
            this.notificationSettingsControl1.Status = "Failed";
            this.notificationSettingsControl1.TabIndex = 1;
            // 
            // notificationSettingsControl2
            // 
            this.notificationSettingsControl2.AutoSize = true;
            this.notificationSettingsControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.notificationSettingsControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notificationSettingsControl2.Location = new System.Drawing.Point(3, 85);
            this.notificationSettingsControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.notificationSettingsControl2.Name = "notificationSettingsControl2";
            this.notificationSettingsControl2.Size = new System.Drawing.Size(467, 48);
            this.notificationSettingsControl2.SoundPath = "";
            this.notificationSettingsControl2.Status = "Fixed";
            this.notificationSettingsControl2.TabIndex = 2;
            // 
            // notificationSettingsControl3
            // 
            this.notificationSettingsControl3.AutoSize = true;
            this.notificationSettingsControl3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.notificationSettingsControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notificationSettingsControl3.Location = new System.Drawing.Point(3, 141);
            this.notificationSettingsControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.notificationSettingsControl3.Name = "notificationSettingsControl3";
            this.notificationSettingsControl3.Size = new System.Drawing.Size(467, 48);
            this.notificationSettingsControl3.SoundPath = "";
            this.notificationSettingsControl3.Status = "StillFailing";
            this.notificationSettingsControl3.TabIndex = 3;
            // 
            // notificationSettingsControl4
            // 
            this.notificationSettingsControl4.AutoSize = true;
            this.notificationSettingsControl4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.notificationSettingsControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notificationSettingsControl4.Location = new System.Drawing.Point(3, 197);
            this.notificationSettingsControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.notificationSettingsControl4.Name = "notificationSettingsControl4";
            this.notificationSettingsControl4.Size = new System.Drawing.Size(467, 48);
            this.notificationSettingsControl4.SoundPath = "";
            this.notificationSettingsControl4.Status = "Succeeded";
            this.notificationSettingsControl4.TabIndex = 3;
            // 
            // enableSoundCheckBox
            // 
            this.enableSoundCheckBox.Location = new System.Drawing.Point(3, 3);
            this.enableSoundCheckBox.Name = "enableSoundCheckBox";
            this.enableSoundCheckBox.Properties.Caption = "&Enable sound notifications";
            this.enableSoundCheckBox.Size = new System.Drawing.Size(150, 19);
            this.enableSoundCheckBox.TabIndex = 0;
            this.enableSoundCheckBox.CheckedChanged += new System.EventHandler(this.enableSoundCheckBox_CheckedChanged);
            // 
            // treatUnstableAsFailedCheckBox
            // 
            this.treatUnstableAsFailedCheckBox.Location = new System.Drawing.Point(3, 252);
            this.treatUnstableAsFailedCheckBox.Name = "treatUnstableAsFailedCheckBox";
            this.treatUnstableAsFailedCheckBox.Properties.Caption = "&Treat unstable as failed";
            this.treatUnstableAsFailedCheckBox.Size = new System.Drawing.Size(139, 19);
            this.treatUnstableAsFailedCheckBox.TabIndex = 9;
            this.treatUnstableAsFailedCheckBox.CheckedChanged += new System.EventHandler(this.treatUnstableAsFailedCheckBox_CheckedChanged);
            // 
            // enableBalloonCheckBox
            // 
            this.enableBalloonCheckBox.Location = new System.Drawing.Point(3, 277);
            this.enableBalloonCheckBox.Name = "enableBalloonCheckBox";
            this.enableBalloonCheckBox.Properties.Caption = "Enable system tray &balloon notifications";
            this.enableBalloonCheckBox.Size = new System.Drawing.Size(256, 19);
            this.enableBalloonCheckBox.TabIndex = 10;
            this.enableBalloonCheckBox.CheckedChanged += new System.EventHandler(this.enableBalloonCheckBox_CheckedChanged);
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.tableLayoutPanel1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(477, 340);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Configure sound notifications";
            // 
            // NotificationsSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "NotificationsSettingsControl";
            this.Size = new System.Drawing.Size(477, 340);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.enableSoundCheckBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treatUnstableAsFailedCheckBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enableBalloonCheckBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
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
        private DevExpress.XtraEditors.CheckEdit enableBalloonCheckBox;
    }
}
