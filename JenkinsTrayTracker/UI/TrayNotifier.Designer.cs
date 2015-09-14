namespace Jenkins.TrayTracker.UI
{
    partial class TrayNotifier
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrayNotifier));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyContextMenuStrip.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notifyContextMenuStrip;
            this.notifyIcon.Text = "Jenkins Tray";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // notifyContextMenuStrip
            // 
            this.notifyContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMenuItem,
            this.settingsMenuItem,
            this.refreshMenuItem,
            this.aboutMenuItem,
            this.exitMenuItem});
            this.notifyContextMenuStrip.Name = "notifyContextMenuStrip";
            this.notifyContextMenuStrip.Size = new System.Drawing.Size(117, 114);
            // 
            // openMenuItem
            // 
            this.openMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.openMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openMenuItem.Image")));
            this.openMenuItem.Name = "openMenuItem";
            this.openMenuItem.Size = new System.Drawing.Size(116, 22);
            this.openMenuItem.Text = "Open";
            this.openMenuItem.Click += new System.EventHandler(this.openMenuItem_Click);
            // 
            // settingsMenuItem
            // 
            this.settingsMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("settingsMenuItem.Image")));
            this.settingsMenuItem.Name = "settingsMenuItem";
            this.settingsMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsMenuItem.Text = "Settings";
            this.settingsMenuItem.Click += new System.EventHandler(this.settingsMenuItem_Click);
            // 
            // refreshMenuItem
            // 
            this.refreshMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("refreshMenuItem.Image")));
            this.refreshMenuItem.Name = "refreshMenuItem";
            this.refreshMenuItem.Size = new System.Drawing.Size(116, 22);
            this.refreshMenuItem.Text = "Refresh";
            this.refreshMenuItem.Click += new System.EventHandler(this.refreshMenuItem_Click);
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutMenuItem.Image")));
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutMenuItem.Text = "About";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitMenuItem.Image")));
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exitMenuItem.Text = "Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            this.notifyContextMenuStrip.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifyContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
    }
}
