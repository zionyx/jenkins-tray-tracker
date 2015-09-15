namespace Jenkins.Tray.UI.Controls
{
    partial class ProjectListControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectListControl));
            this.projectsGridControl = new DevExpress.XtraGrid.GridControl();
            this.projectContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllProjectsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unselectAllProjectsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectsGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.projectSelectedGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.projectSelectedCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.projectNameGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.toggleSelectionButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.repositoryItemMarqueeProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.projectsGridControl)).BeginInit();
            this.projectContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.projectsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectSelectedCheckEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // projectsGridControl
            // 
            this.projectsGridControl.ContextMenuStrip = this.projectContextMenuStrip;
            this.projectsGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectsGridControl.Location = new System.Drawing.Point(0, 26);
            this.projectsGridControl.MainView = this.projectsGridView;
            this.projectsGridControl.Name = "projectsGridControl";
            this.projectsGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.projectSelectedCheckEdit});
            this.projectsGridControl.Size = new System.Drawing.Size(680, 314);
            this.projectsGridControl.TabIndex = 1;
            this.projectsGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.projectsGridView});
            this.projectsGridControl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProjectListControl_KeyPress);
            this.projectsGridControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.projectsGridControl_MouseDoubleClick);
            // 
            // projectContextMenuStrip
            // 
            this.projectContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllProjectsMenuItem,
            this.unselectAllProjectsMenuItem});
            this.projectContextMenuStrip.Name = "projectContextMenuStrip";
            this.projectContextMenuStrip.Size = new System.Drawing.Size(182, 48);
            // 
            // selectAllProjectsMenuItem
            // 
            this.selectAllProjectsMenuItem.Name = "selectAllProjectsMenuItem";
            this.selectAllProjectsMenuItem.Size = new System.Drawing.Size(181, 22);
            this.selectAllProjectsMenuItem.Text = "Select all projects";
            this.selectAllProjectsMenuItem.Click += new System.EventHandler(this.selectAllProjectsMenuItem_Click);
            // 
            // unselectAllProjectsMenuItem
            // 
            this.unselectAllProjectsMenuItem.Name = "unselectAllProjectsMenuItem";
            this.unselectAllProjectsMenuItem.Size = new System.Drawing.Size(181, 22);
            this.unselectAllProjectsMenuItem.Text = "Unselect all projects";
            this.unselectAllProjectsMenuItem.Click += new System.EventHandler(this.deselectAllProjectsMenuItem_Click);
            // 
            // projectsGridView
            // 
            this.projectsGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.projectSelectedGridColumn,
            this.projectNameGridColumn});
            this.projectsGridView.GridControl = this.projectsGridControl;
            this.projectsGridView.Name = "projectsGridView";
            this.projectsGridView.OptionsCustomization.AllowGroup = false;
            this.projectsGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.projectsGridView.OptionsView.ShowGroupPanel = false;
            this.projectsGridView.OptionsView.ShowIndicator = false;
            this.projectsGridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.projectNameGridColumn, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.projectsGridView.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.projectsGridView_CustomUnboundColumnData);
            // 
            // projectSelectedGridColumn
            // 
            this.projectSelectedGridColumn.ColumnEdit = this.projectSelectedCheckEdit;
            this.projectSelectedGridColumn.FieldName = "Selected";
            this.projectSelectedGridColumn.Name = "projectSelectedGridColumn";
            this.projectSelectedGridColumn.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            this.projectSelectedGridColumn.Visible = true;
            this.projectSelectedGridColumn.VisibleIndex = 0;
            this.projectSelectedGridColumn.Width = 28;
            // 
            // projectSelectedCheckEdit
            // 
            this.projectSelectedCheckEdit.AutoHeight = false;
            this.projectSelectedCheckEdit.Name = "projectSelectedCheckEdit";
            this.projectSelectedCheckEdit.EditValueChanged += new System.EventHandler(this.projectSelectedCheckEdit_EditValueChanged);
            // 
            // projectNameGridColumn
            // 
            this.projectNameGridColumn.Caption = "Projects";
            this.projectNameGridColumn.FieldName = "Name";
            this.projectNameGridColumn.Name = "projectNameGridColumn";
            this.projectNameGridColumn.OptionsColumn.AllowEdit = false;
            this.projectNameGridColumn.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
            this.projectNameGridColumn.Visible = true;
            this.projectNameGridColumn.VisibleIndex = 1;
            this.projectNameGridColumn.Width = 302;
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
            this.toggleSelectionButtonItem});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 7;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleSelectionButtonItem)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // toggleSelectionButtonItem
            // 
            this.toggleSelectionButtonItem.Caption = "Select/unselect all projects";
            this.toggleSelectionButtonItem.Glyph = ((System.Drawing.Image)(resources.GetObject("toggleSelectionButtonItem.Glyph")));
            this.toggleSelectionButtonItem.Id = 5;
            this.toggleSelectionButtonItem.Name = "toggleSelectionButtonItem";
            this.toggleSelectionButtonItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toggleSelectionButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toggleSelectionButtonItem_ItemClick);
            // 
            // repositoryItemMarqueeProgressBar1
            // 
            this.repositoryItemMarqueeProgressBar1.Name = "repositoryItemMarqueeProgressBar1";
            // 
            // ProjectListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.projectsGridControl);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ProjectListControl";
            this.Size = new System.Drawing.Size(680, 340);
            ((System.ComponentModel.ISupportInitialize)(this.projectsGridControl)).EndInit();
            this.projectContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.projectsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectSelectedCheckEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl projectsGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView projectsGridView;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.Columns.GridColumn projectNameGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn projectSelectedGridColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit projectSelectedCheckEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar repositoryItemMarqueeProgressBar1;
        private System.Windows.Forms.ContextMenuStrip projectContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem selectAllProjectsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unselectAllProjectsMenuItem;
        private DevExpress.XtraBars.BarButtonItem toggleSelectionButtonItem;
    }
}