namespace Jenkins.Tray.UI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.settingsButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.refreshButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.exitButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.checkUpdatesButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.aboutButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.lastCheckBarStaticItem = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.projectsGridControl = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openProjectPageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openConsolePageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runBuildMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.acknowledgeStatusMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acknowledgeProjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.claimBuildMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.removeProjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectsGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.setAuthenticationTokenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.statusGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPictureEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.nameGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.urlGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.buildDetailsGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lastBuildGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lastSuccessGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lastSuccessUserGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lastFailureGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lastFailureUserGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.claimedByGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.claimReasonGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectsGridControl)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.projectsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit3)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar3});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.settingsButtonItem,
            this.aboutButtonItem,
            this.refreshButtonItem,
            this.exitButtonItem,
            this.lastCheckBarStaticItem,
            this.checkUpdatesButtonItem});
            this.barManager.MainMenu = this.bar2;
            this.barManager.MaxItemId = 6;
            this.barManager.StatusBar = this.bar3;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.settingsButtonItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.refreshButtonItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.exitButtonItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.checkUpdatesButtonItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.aboutButtonItem)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // settingsButtonItem
            // 
            this.settingsButtonItem.Caption = "Settings";
            this.settingsButtonItem.Glyph = ((System.Drawing.Image)(resources.GetObject("settingsButtonItem.Glyph")));
            this.settingsButtonItem.Id = 0;
            this.settingsButtonItem.Name = "settingsButtonItem";
            this.settingsButtonItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.settingsButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.settingsButtonItem_ItemClick);
            // 
            // refreshButtonItem
            // 
            this.refreshButtonItem.Caption = "Refresh";
            this.refreshButtonItem.Glyph = ((System.Drawing.Image)(resources.GetObject("refreshButtonItem.Glyph")));
            this.refreshButtonItem.Id = 2;
            this.refreshButtonItem.Name = "refreshButtonItem";
            this.refreshButtonItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.refreshButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.refreshButtonItem_ItemClick);
            // 
            // exitButtonItem
            // 
            this.exitButtonItem.Caption = "Exit";
            this.exitButtonItem.Glyph = ((System.Drawing.Image)(resources.GetObject("exitButtonItem.Glyph")));
            this.exitButtonItem.Id = 3;
            this.exitButtonItem.Name = "exitButtonItem";
            this.exitButtonItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.exitButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.exitButtonItem_ItemClick);
            // 
            // checkUpdatesButtonItem
            // 
            this.checkUpdatesButtonItem.Caption = "Check for updates";
            this.checkUpdatesButtonItem.Glyph = ((System.Drawing.Image)(resources.GetObject("checkUpdatesButtonItem.Glyph")));
            this.checkUpdatesButtonItem.Id = 5;
            this.checkUpdatesButtonItem.Name = "checkUpdatesButtonItem";
            this.checkUpdatesButtonItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.checkUpdatesButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.checkUpdatesButtonItem_ItemClick);
            // 
            // aboutButtonItem
            // 
            this.aboutButtonItem.Caption = "About";
            this.aboutButtonItem.Glyph = ((System.Drawing.Image)(resources.GetObject("aboutButtonItem.Glyph")));
            this.aboutButtonItem.Id = 1;
            this.aboutButtonItem.Name = "aboutButtonItem";
            this.aboutButtonItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.aboutButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.aboutButtonItem_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.lastCheckBarStaticItem)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // lastCheckBarStaticItem
            // 
            this.lastCheckBarStaticItem.Caption = "Last check: -";
            this.lastCheckBarStaticItem.Id = 4;
            this.lastCheckBarStaticItem.Name = "lastCheckBarStaticItem";
            this.lastCheckBarStaticItem.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(879, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 384);
            this.barDockControlBottom.Size = new System.Drawing.Size(879, 28);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 358);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(879, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 358);
            // 
            // projectsGridControl
            // 
            this.projectsGridControl.ContextMenuStrip = this.contextMenuStrip;
            this.projectsGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectsGridControl.Location = new System.Drawing.Point(0, 26);
            this.projectsGridControl.MainView = this.projectsGridView;
            this.projectsGridControl.Name = "projectsGridControl";
            this.projectsGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit3});
            this.projectsGridControl.Size = new System.Drawing.Size(879, 358);
            this.projectsGridControl.TabIndex = 4;
            this.projectsGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.projectsGridView});
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openProjectPageMenuItem,
            this.openConsolePageMenuItem,
            this.runBuildMenuItem,
            this.toolStripSeparator1,
            this.acknowledgeStatusMenuItem,
            this.acknowledgeProjectMenuItem,
            this.toolStripSeparator4,
            this.setAuthenticationTokenMenuItem,
            this.toolStripSeparator2,
            this.claimBuildMenuItem,
            this.toolStripSeparator3,
            this.removeProjectMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(230, 176);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // openProjectPageMenuItem
            // 
            this.openProjectPageMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openProjectPageMenuItem.Image")));
            this.openProjectPageMenuItem.Name = "openProjectPageMenuItem";
            this.openProjectPageMenuItem.Size = new System.Drawing.Size(229, 22);
            this.openProjectPageMenuItem.Text = "Open project page";
            this.openProjectPageMenuItem.Click += new System.EventHandler(this.openProjectPageMenuItem_Click);
            // 
            // openConsolePageMenuItem
            // 
            this.openConsolePageMenuItem.Name = "openConsolePageMenuItem";
            this.openConsolePageMenuItem.Size = new System.Drawing.Size(229, 22);
            this.openConsolePageMenuItem.Text = "View console output";
            this.openConsolePageMenuItem.Click += new System.EventHandler(this.openConsolePageMenuItem_Click);
            // 
            // runBuildMenuItem
            // 
            this.runBuildMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("runBuildMenuItem.Image")));
            this.runBuildMenuItem.Name = "runBuildMenuItem";
            this.runBuildMenuItem.Size = new System.Drawing.Size(229, 22);
            this.runBuildMenuItem.Text = "Run build";
            this.runBuildMenuItem.Click += new System.EventHandler(this.runBuildMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(226, 6);
            // 
            // acknowledgeStatusMenuItem
            // 
            this.acknowledgeStatusMenuItem.Name = "acknowledgeStatusMenuItem";
            this.acknowledgeStatusMenuItem.Size = new System.Drawing.Size(229, 22);
            this.acknowledgeStatusMenuItem.Text = "Acknowledge status";
            this.acknowledgeStatusMenuItem.Click += new System.EventHandler(this.acknowledgeMenuItem_Click);
            // 
            // acknowledgeProjectMenuItem
            // 
            this.acknowledgeProjectMenuItem.Name = "acknowledgeProjectMenuItem";
            this.acknowledgeProjectMenuItem.Size = new System.Drawing.Size(229, 22);
            this.acknowledgeProjectMenuItem.Text = "Acknowledge project";
            this.acknowledgeProjectMenuItem.Click += new System.EventHandler(this.acknowledgeProjectMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(226, 6);
            // 
            // claimBuildMenuItem
            // 
            this.claimBuildMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("claimBuildMenuItem.Image")));
            this.claimBuildMenuItem.Name = "claimBuildMenuItem";
            this.claimBuildMenuItem.Size = new System.Drawing.Size(229, 22);
            this.claimBuildMenuItem.Text = "Claim this build";
            this.claimBuildMenuItem.Click += new System.EventHandler(this.claimBuildMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(226, 6);
            // 
            // removeProjectMenuItem
            // 
            this.removeProjectMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("removeProjectMenuItem.Image")));
            this.removeProjectMenuItem.Name = "removeProjectMenuItem";
            this.removeProjectMenuItem.Size = new System.Drawing.Size(229, 22);
            this.removeProjectMenuItem.Text = "Remove project";
            this.removeProjectMenuItem.Click += new System.EventHandler(this.removeProjectMenuItem_Click);
            // 
            // projectsGridView
            // 
            this.projectsGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.serverGridColumn,
            this.statusGridColumn,
            this.nameGridColumn,
            this.urlGridColumn,
            this.buildDetailsGridColumn,
            this.lastBuildGridColumn,
            this.lastSuccessGridColumn,
            this.lastSuccessUserGridColumn,
            this.lastFailureGridColumn,
            this.lastFailureUserGridColumn,
            this.claimedByGridColumn,
            this.claimReasonGridColumn});
            this.projectsGridView.GridControl = this.projectsGridControl;
            this.projectsGridView.GroupCount = 1;
            this.projectsGridView.Name = "projectsGridView";
            this.projectsGridView.OptionsBehavior.AutoExpandAllGroups = true;
            this.projectsGridView.OptionsCustomization.AllowGroup = false;
            this.projectsGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.projectsGridView.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateAllContent;
            this.projectsGridView.OptionsView.ShowGroupPanel = false;
            this.projectsGridView.OptionsView.ShowIndicator = false;
            this.projectsGridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.serverGridColumn, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.nameGridColumn, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.projectsGridView.CustomColumnSort += new DevExpress.XtraGrid.Views.Base.CustomColumnSortEventHandler(this.projectsGridView_CustomColumnSort);
            this.projectsGridView.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.projectsGridView_CustomUnboundColumnData);
            this.projectsGridView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.projectsGridView_MouseMove);
            this.projectsGridView.DoubleClick += new System.EventHandler(this.projectsGridView_DoubleClick);
            // 
            // setAuthenticationTokenMenuItem
            // 
            this.setAuthenticationTokenMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("setAuthenticationTokenMenuItem.Image")));
            this.setAuthenticationTokenMenuItem.Name = "setAuthenticationTokenMenuItem";
            this.setAuthenticationTokenMenuItem.Size = new System.Drawing.Size(203, 22);
            this.setAuthenticationTokenMenuItem.Text = "Set authentication token";
            this.setAuthenticationTokenMenuItem.Click += new System.EventHandler(this.setAuthenticationTokenMenuItem_Click);
            // 
            // serverGridColumn
            // 
            this.serverGridColumn.Caption = "Server";
            this.serverGridColumn.FieldName = "Server";
            this.serverGridColumn.Name = "serverGridColumn";
            this.serverGridColumn.OptionsColumn.AllowEdit = false;
            // 
            // statusGridColumn
            // 
            this.statusGridColumn.Caption = " ";
            this.statusGridColumn.ColumnEdit = this.repositoryItemPictureEdit3;
            this.statusGridColumn.FieldName = "statusGridColumn";
            this.statusGridColumn.ImageIndex = 0;
            this.statusGridColumn.Name = "statusGridColumn";
            this.statusGridColumn.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.statusGridColumn.Visible = true;
            this.statusGridColumn.VisibleIndex = 0;
            this.statusGridColumn.Width = 50;
            // 
            // repositoryItemPictureEdit3
            // 
            this.repositoryItemPictureEdit3.Name = "repositoryItemPictureEdit3";
            // 
            // nameGridColumn
            // 
            this.nameGridColumn.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.nameGridColumn.AppearanceCell.Options.UseFont = true;
            this.nameGridColumn.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.nameGridColumn.AppearanceHeader.Options.UseFont = true;
            this.nameGridColumn.Caption = "Project";
            this.nameGridColumn.FieldName = "Name";
            this.nameGridColumn.Name = "nameGridColumn";
            this.nameGridColumn.OptionsColumn.AllowEdit = false;
            this.nameGridColumn.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
            this.nameGridColumn.Visible = true;
            this.nameGridColumn.VisibleIndex = 1;
            this.nameGridColumn.Width = 137;
            // 
            // urlGridColumn
            // 
            this.urlGridColumn.Caption = "URL";
            this.urlGridColumn.FieldName = "Url";
            this.urlGridColumn.Name = "urlGridColumn";
            this.urlGridColumn.OptionsColumn.AllowEdit = false;
            this.urlGridColumn.Width = 157;
            // 
            // buildDetailsGridColumn
            // 
            this.buildDetailsGridColumn.AppearanceCell.Options.UseTextOptions = true;
            this.buildDetailsGridColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.buildDetailsGridColumn.Caption = "Details";
            this.buildDetailsGridColumn.FieldName = "buildDetailsStr";
            this.buildDetailsGridColumn.Name = "buildDetailsGridColumn";
            this.buildDetailsGridColumn.OptionsColumn.AllowEdit = false;
            this.buildDetailsGridColumn.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
            this.buildDetailsGridColumn.Visible = true;
            this.buildDetailsGridColumn.VisibleIndex = 2;
            this.buildDetailsGridColumn.Width = 200;
            // 
            // lastBuildGridColumn
            // 
            this.lastBuildGridColumn.AppearanceCell.Options.UseTextOptions = true;
            this.lastBuildGridColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lastBuildGridColumn.Caption = "Last build";
            this.lastBuildGridColumn.FieldName = "lastBuildStr";
            this.lastBuildGridColumn.Name = "lastBuildGridColumn";
            this.lastBuildGridColumn.OptionsColumn.AllowEdit = false;
            this.lastBuildGridColumn.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            this.lastBuildGridColumn.Visible = true;
            this.lastBuildGridColumn.VisibleIndex = 3;
            this.lastBuildGridColumn.Width = 121;
            // 
            // lastSuccessGridColumn
            // 
            this.lastSuccessGridColumn.AppearanceCell.Options.UseTextOptions = true;
            this.lastSuccessGridColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lastSuccessGridColumn.Caption = "Last success";
            this.lastSuccessGridColumn.FieldName = "LastSuccessBuildStr";
            this.lastSuccessGridColumn.Name = "lastSuccessGridColumn";
            this.lastSuccessGridColumn.OptionsColumn.AllowEdit = false;
            this.lastSuccessGridColumn.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            this.lastSuccessGridColumn.Visible = true;
            this.lastSuccessGridColumn.VisibleIndex = 4;
            this.lastSuccessGridColumn.Width = 121;
            // 
            // lastSuccessUserGridColumn
            // 
            this.lastSuccessUserGridColumn.Caption = "Last success user";
            this.lastSuccessUserGridColumn.FieldName = "LastSuccessUsers";
            this.lastSuccessUserGridColumn.Name = "lastSuccessUserGridColumn";
            this.lastSuccessUserGridColumn.OptionsColumn.AllowEdit = false;
            this.lastSuccessUserGridColumn.Width = 57;
            // 
            // lastFailureGridColumn
            // 
            this.lastFailureGridColumn.AppearanceCell.Options.UseTextOptions = true;
            this.lastFailureGridColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lastFailureGridColumn.Caption = "Last failure";
            this.lastFailureGridColumn.FieldName = "LastFailureBuildStr";
            this.lastFailureGridColumn.Name = "lastFailureGridColumn";
            this.lastFailureGridColumn.OptionsColumn.AllowEdit = false;
            this.lastFailureGridColumn.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            this.lastFailureGridColumn.Visible = true;
            this.lastFailureGridColumn.VisibleIndex = 5;
            this.lastFailureGridColumn.Width = 124;
            // 
            // lastFailureUserGridColumn
            // 
            this.lastFailureUserGridColumn.Caption = "Last failure user";
            this.lastFailureUserGridColumn.FieldName = "LastFailureUsers";
            this.lastFailureUserGridColumn.Name = "lastFailureUserGridColumn";
            this.lastFailureUserGridColumn.OptionsColumn.AllowEdit = false;
            this.lastFailureUserGridColumn.Visible = true;
            this.lastFailureUserGridColumn.VisibleIndex = 6;
            this.lastFailureUserGridColumn.Width = 65;
            // 
            // claimedByGridColumn
            // 
            this.claimedByGridColumn.Caption = "Claimed by";
            this.claimedByGridColumn.FieldName = "ClaimedBy";
            this.claimedByGridColumn.Name = "claimedByGridColumn";
            this.claimedByGridColumn.OptionsColumn.AllowEdit = false;
            // 
            // claimReasonGridColumn
            // 
            this.claimReasonGridColumn.Caption = "Claim reason";
            this.claimReasonGridColumn.FieldName = "ClaimReason";
            this.claimReasonGridColumn.Name = "claimReasonGridColumn";
            this.claimReasonGridColumn.OptionsColumn.AllowEdit = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 412);
            this.Controls.Add(this.projectsGridControl);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "Jenkins Tray";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.JenkinsTrayForm_FormClosing);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectsGridControl)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.projectsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem settingsButtonItem;
        private DevExpress.XtraBars.BarButtonItem aboutButtonItem;
        private DevExpress.XtraGrid.GridControl projectsGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView projectsGridView;
        private DevExpress.XtraGrid.Columns.GridColumn nameGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn urlGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn buildDetailsGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn lastBuildGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn lastSuccessGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn lastFailureGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn serverGridColumn;
        private DevExpress.XtraBars.BarButtonItem refreshButtonItem;
        private DevExpress.XtraBars.BarButtonItem exitButtonItem;
        private DevExpress.XtraBars.BarStaticItem lastCheckBarStaticItem;
        private DevExpress.XtraGrid.Columns.GridColumn statusGridColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit3;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openProjectPageMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runBuildMenuItem;
        private DevExpress.XtraBars.BarButtonItem checkUpdatesButtonItem;
        private System.Windows.Forms.ToolStripMenuItem acknowledgeStatusMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem removeProjectMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn lastSuccessUserGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn lastFailureUserGridColumn;
        private System.Windows.Forms.ToolStripMenuItem openConsolePageMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn claimedByGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn claimReasonGridColumn;
        private System.Windows.Forms.ToolStripMenuItem claimBuildMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem acknowledgeProjectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAuthenticationTokenMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}