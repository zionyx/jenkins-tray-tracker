namespace Jenkins.Tray.UI.Controls
{
    partial class NotificationSettingsControl
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
            this.pathEdit = new DevExpress.XtraEditors.ButtonEdit();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.statusLabel = new DevExpress.XtraEditors.LabelControl();
            this.testSoundButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pathEdit.Properties)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pathEdit
            // 
            this.pathEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pathEdit.Location = new System.Drawing.Point(3, 28);
            this.pathEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pathEdit.Name = "pathEdit";
            this.pathEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Close)});
            this.pathEdit.Size = new System.Drawing.Size(117, 22);
            this.pathEdit.TabIndex = 1;
            this.pathEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.pathEdit_ButtonClick);
            this.pathEdit.EditValueChanged += new System.EventHandler(this.pathEdit_EditValueChanged);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.pathEdit, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.statusLabel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.testSoundButton, 1, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(216, 60);
            this.tableLayoutPanel.TabIndex = 2;
            // 
            // statusLabel
            // 
            this.statusLabel.Location = new System.Drawing.Point(3, 4);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(36, 16);
            this.statusLabel.TabIndex = 2;
            this.statusLabel.Text = "Sound";
            // 
            // testSoundButton
            // 
            this.testSoundButton.Location = new System.Drawing.Point(126, 28);
            this.testSoundButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.testSoundButton.Name = "testSoundButton";
            this.testSoundButton.Size = new System.Drawing.Size(87, 28);
            this.testSoundButton.TabIndex = 3;
            this.testSoundButton.Text = "Test";
            this.testSoundButton.Click += new System.EventHandler(this.testSoundButton_Click);
            // 
            // NotificationSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "NotificationSettingsControl";
            this.Size = new System.Drawing.Size(216, 60);
            ((System.ComponentModel.ISupportInitialize)(this.pathEdit.Properties)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ButtonEdit pathEdit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private DevExpress.XtraEditors.LabelControl statusLabel;
        private DevExpress.XtraEditors.SimpleButton testSoundButton;
    }
}
