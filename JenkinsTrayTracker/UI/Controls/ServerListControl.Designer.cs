namespace Jenkins.TrayTracker.UI.Controls
{
    partial class ServerListControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerListControl));
            this.serversGridControl = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editServerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeServerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serversGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.serverGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.addServerButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.editServerButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.removeServerButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.statusTextItem = new DevExpress.XtraBars.BarStaticItem();
            this.statusProgressItem = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemMarqueeProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.serversGridControl)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serversGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // serversGridControl
            // 
            this.serversGridControl.ContextMenuStrip = this.contextMenuStrip;
            this.serversGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serversGridControl.Location = new System.Drawing.Point(0, 26);
            this.serversGridControl.MainView = this.serversGridView;
            this.serversGridControl.Name = "serversGridControl";
            this.serversGridControl.Size = new System.Drawing.Size(680, 314);
            this.serversGridControl.TabIndex = 0;
            this.serversGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.serversGridView});
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editServerMenuItem,
            this.removeServerMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(152, 48);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // editServerMenuItem
            // 
            this.editServerMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editServerMenuItem.Image")));
            this.editServerMenuItem.Name = "editServerMenuItem";
            this.editServerMenuItem.Size = new System.Drawing.Size(151, 22);
            this.editServerMenuItem.Text = "Edit server";
            this.editServerMenuItem.Click += new System.EventHandler(this.editServerMenuItem_Click);
            // 
            // removeServerMenuItem
            // 
            this.removeServerMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("removeServerMenuItem.Image")));
            this.removeServerMenuItem.Name = "removeServerMenuItem";
            this.removeServerMenuItem.Size = new System.Drawing.Size(151, 22);
            this.removeServerMenuItem.Text = "Remove server";
            this.removeServerMenuItem.Click += new System.EventHandler(this.removeServerMenuItem_Click);
            // 
            // serversGridView
            // 
            this.serversGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.serverGridColumn});
            this.serversGridView.GridControl = this.serversGridControl;
            this.serversGridView.Name = "serversGridView";
            this.serversGridView.OptionsCustomization.AllowGroup = false;
            this.serversGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.serversGridView.OptionsView.ShowGroupPanel = false;
            this.serversGridView.OptionsView.ShowIndicator = false;
            this.serversGridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.serverGridColumn, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.serversGridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.serversGridView_FocusedRowChanged);
            // 
            // serverGridColumn
            // 
            this.serverGridColumn.Caption = "Servers";
            this.serverGridColumn.FieldName = "DisplayText";
            this.serverGridColumn.Name = "serverGridColumn";
            this.serverGridColumn.OptionsColumn.AllowEdit = false;
            this.serverGridColumn.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
            this.serverGridColumn.Visible = true;
            this.serverGridColumn.VisibleIndex = 0;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.addServerButtonItem,
            this.removeServerButtonItem,
            this.statusTextItem,
            this.statusProgressItem,
            this.editServerButtonItem});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 5;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMarqueeProgressBar1});
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.addServerButtonItem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.editServerButtonItem),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.removeServerButtonItem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // addServerButtonItem
            // 
            this.addServerButtonItem.Caption = "Add server";
            this.addServerButtonItem.Glyph = ((System.Drawing.Image)(resources.GetObject("addServerButtonItem.Glyph")));
            this.addServerButtonItem.Id = 0;
            this.addServerButtonItem.Name = "addServerButtonItem";
            this.addServerButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.addServerButtonItem_ItemClick);
            // 
            // editServerButtonItem
            // 
            this.editServerButtonItem.Caption = "Edit server";
            this.editServerButtonItem.Enabled = false;
            this.editServerButtonItem.Glyph = ((System.Drawing.Image)(resources.GetObject("editServerButtonItem.Glyph")));
            this.editServerButtonItem.Id = 4;
            this.editServerButtonItem.Name = "editServerButtonItem";
            this.editServerButtonItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.editServerButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.editServerButtonItem_ItemClick);
            // 
            // removeServerButtonItem
            // 
            this.removeServerButtonItem.Caption = "Remove server";
            this.removeServerButtonItem.Enabled = false;
            this.removeServerButtonItem.Glyph = ((System.Drawing.Image)(resources.GetObject("removeServerButtonItem.Glyph")));
            this.removeServerButtonItem.Id = 1;
            this.removeServerButtonItem.Name = "removeServerButtonItem";
            this.removeServerButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.removeServerButtonItem_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(680, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 340);
            this.barDockControlBottom.Size = new System.Drawing.Size(680, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 314);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(680, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 314);
            // 
            // statusTextItem
            // 
            this.statusTextItem.Id = 2;
            this.statusTextItem.Name = "statusTextItem";
            this.statusTextItem.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // statusProgressItem
            // 
            this.statusProgressItem.Edit = this.repositoryItemMarqueeProgressBar1;
            this.statusProgressItem.Id = 3;
            this.statusProgressItem.Name = "statusProgressItem";
            // 
            // repositoryItemMarqueeProgressBar1
            // 
            this.repositoryItemMarqueeProgressBar1.Name = "repositoryItemMarqueeProgressBar1";
            // 
            // ServerListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.serversGridControl);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ServerListControl";
            this.Size = new System.Drawing.Size(680, 340);
            ((System.ComponentModel.ISupportInitialize)(this.serversGridControl)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.serversGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl serversGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView serversGridView;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem addServerButtonItem;
        private DevExpress.XtraBars.BarButtonItem removeServerButtonItem;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.Columns.GridColumn serverGridColumn;
        private DevExpress.XtraBars.BarStaticItem statusTextItem;
        private DevExpress.XtraBars.BarEditItem statusProgressItem;
        private DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar repositoryItemMarqueeProgressBar1;
        private DevExpress.XtraBars.BarButtonItem editServerButtonItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editServerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeServerMenuItem;
    }
}