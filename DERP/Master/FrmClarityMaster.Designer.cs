﻿namespace DERP.Master
{
    partial class FrmClarityMaster
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
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.grdPurityMaster = new DevExpress.XtraGrid.GridControl();
            this.ContextMNExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MNExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportTEXT = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.MNExportRTF = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvPurityMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmPurityId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmPurity_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmPurityGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmSequenceno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmPurityGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmShade = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRoughShadeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.lueColor = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.luePurityGroup = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.chkActive = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtSeqNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtPurityName = new DevExpress.XtraEditors.TextEdit();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.lblMode = new DevExpress.XtraEditors.LabelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPurityMaster)).BeginInit();
            this.ContextMNExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurityMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePurityGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeqNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPurityName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.panelControl6.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.DockingOptions.ShowCloseButton = false;
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("66f1be09-bf3f-426d-8d8f-9d3932132dcd");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(348, 200);
            this.dockPanel1.Size = new System.Drawing.Size(348, 430);
            this.dockPanel1.Text = "Purity Master";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.grdPurityMaster);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(339, 403);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // grdPurityMaster
            // 
            this.grdPurityMaster.ContextMenuStrip = this.ContextMNExport;
            this.grdPurityMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPurityMaster.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPurityMaster.Location = new System.Drawing.Point(0, 0);
            this.grdPurityMaster.MainView = this.dgvPurityMaster;
            this.grdPurityMaster.Name = "grdPurityMaster";
            this.grdPurityMaster.Size = new System.Drawing.Size(339, 403);
            this.grdPurityMaster.TabIndex = 16;
            this.grdPurityMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvPurityMaster});
            // 
            // ContextMNExport
            // 
            this.ContextMNExport.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.ContextMNExport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MNExportExcel,
            this.MNExportPDF,
            this.MNExportTEXT,
            this.MNExportHTML,
            this.MNExportCSV,
            this.MNExportRTF});
            this.ContextMNExport.Name = "ContextExport";
            this.ContextMNExport.Size = new System.Drawing.Size(130, 136);
            // 
            // MNExportExcel
            // 
            this.MNExportExcel.Name = "MNExportExcel";
            this.MNExportExcel.Size = new System.Drawing.Size(129, 22);
            this.MNExportExcel.Text = "To Excel";
            this.MNExportExcel.Click += new System.EventHandler(this.MNExportExcel_Click);
            // 
            // MNExportPDF
            // 
            this.MNExportPDF.Name = "MNExportPDF";
            this.MNExportPDF.Size = new System.Drawing.Size(129, 22);
            this.MNExportPDF.Text = "To PDF";
            this.MNExportPDF.Click += new System.EventHandler(this.MNExportPDF_Click);
            // 
            // MNExportTEXT
            // 
            this.MNExportTEXT.Name = "MNExportTEXT";
            this.MNExportTEXT.Size = new System.Drawing.Size(129, 22);
            this.MNExportTEXT.Text = "To TEXT";
            this.MNExportTEXT.Click += new System.EventHandler(this.MNExportTEXT_Click);
            // 
            // MNExportHTML
            // 
            this.MNExportHTML.Name = "MNExportHTML";
            this.MNExportHTML.Size = new System.Drawing.Size(129, 22);
            this.MNExportHTML.Text = "To HTML";
            this.MNExportHTML.Click += new System.EventHandler(this.MNExportHTML_Click);
            // 
            // MNExportCSV
            // 
            this.MNExportCSV.Name = "MNExportCSV";
            this.MNExportCSV.Size = new System.Drawing.Size(129, 22);
            this.MNExportCSV.Text = "To CSV";
            this.MNExportCSV.Click += new System.EventHandler(this.MNExportCSV_Click);
            // 
            // MNExportRTF
            // 
            this.MNExportRTF.Name = "MNExportRTF";
            this.MNExportRTF.Size = new System.Drawing.Size(129, 22);
            this.MNExportRTF.Text = "To RTF";
            this.MNExportRTF.Click += new System.EventHandler(this.MNExportRTF_Click);
            // 
            // dgvPurityMaster
            // 
            this.dgvPurityMaster.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvPurityMaster.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.dgvPurityMaster.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.dgvPurityMaster.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.dgvPurityMaster.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.dgvPurityMaster.Appearance.FooterPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvPurityMaster.Appearance.FooterPanel.Options.UseFont = true;
            this.dgvPurityMaster.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvPurityMaster.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvPurityMaster.Appearance.Row.Font = new System.Drawing.Font("Cambria", 9F);
            this.dgvPurityMaster.Appearance.Row.Options.UseFont = true;
            this.dgvPurityMaster.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmPurityId,
            this.clmPurity_name,
            this.clmPurityGroup,
            this.clmActive,
            this.clmSequenceno,
            this.clmRemark,
            this.clmPurityGroupId,
            this.clmShade,
            this.clmRoughShadeId});
            this.dgvPurityMaster.GridControl = this.grdPurityMaster;
            this.dgvPurityMaster.Name = "dgvPurityMaster";
            this.dgvPurityMaster.OptionsBehavior.Editable = false;
            this.dgvPurityMaster.OptionsBehavior.ReadOnly = true;
            this.dgvPurityMaster.OptionsCustomization.AllowQuickHideColumns = false;
            this.dgvPurityMaster.OptionsView.ShowAutoFilterRow = true;
            this.dgvPurityMaster.OptionsView.ShowFooter = true;
            this.dgvPurityMaster.OptionsView.ShowGroupPanel = false;
            this.dgvPurityMaster.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.dgvPurityMaster_RowCellClick);
            // 
            // clmPurityId
            // 
            this.clmPurityId.Caption = "Purity Id";
            this.clmPurityId.FieldName = "purity_id";
            this.clmPurityId.Name = "clmPurityId";
            this.clmPurityId.OptionsColumn.AllowEdit = false;
            // 
            // clmPurity_name
            // 
            this.clmPurity_name.Caption = "Purity Name";
            this.clmPurity_name.FieldName = "purity_name";
            this.clmPurity_name.Name = "clmPurity_name";
            this.clmPurity_name.OptionsColumn.AllowEdit = false;
            this.clmPurity_name.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.clmPurity_name.Visible = true;
            this.clmPurity_name.VisibleIndex = 0;
            // 
            // clmPurityGroup
            // 
            this.clmPurityGroup.Caption = "Purity Group";
            this.clmPurityGroup.FieldName = "purity_group";
            this.clmPurityGroup.Name = "clmPurityGroup";
            this.clmPurityGroup.Visible = true;
            this.clmPurityGroup.VisibleIndex = 1;
            // 
            // clmActive
            // 
            this.clmActive.Caption = "Active";
            this.clmActive.FieldName = "active";
            this.clmActive.Name = "clmActive";
            this.clmActive.OptionsColumn.AllowEdit = false;
            this.clmActive.Visible = true;
            this.clmActive.VisibleIndex = 3;
            // 
            // clmSequenceno
            // 
            this.clmSequenceno.Caption = "Sequence No";
            this.clmSequenceno.FieldName = "sequence_no";
            this.clmSequenceno.Name = "clmSequenceno";
            this.clmSequenceno.OptionsColumn.AllowEdit = false;
            this.clmSequenceno.Visible = true;
            this.clmSequenceno.VisibleIndex = 4;
            // 
            // clmRemark
            // 
            this.clmRemark.Caption = "Remark";
            this.clmRemark.FieldName = "remarks";
            this.clmRemark.Name = "clmRemark";
            this.clmRemark.OptionsColumn.AllowEdit = false;
            this.clmRemark.Visible = true;
            this.clmRemark.VisibleIndex = 5;
            // 
            // clmPurityGroupId
            // 
            this.clmPurityGroupId.Caption = "Purity Group Id";
            this.clmPurityGroupId.FieldName = "purity_group_id";
            this.clmPurityGroupId.Name = "clmPurityGroupId";
            // 
            // clmShade
            // 
            this.clmShade.Caption = "Rough Shade";
            this.clmShade.FieldName = "color_name";
            this.clmShade.Name = "clmShade";
            this.clmShade.Visible = true;
            this.clmShade.VisibleIndex = 2;
            // 
            // clmRoughShadeId
            // 
            this.clmRoughShadeId.Caption = "RoughShadeId";
            this.clmRoughShadeId.FieldName = "color_id";
            this.clmRoughShadeId.Name = "clmRoughShadeId";
            // 
            // panelControl2
            // 
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl2.Location = new System.Drawing.Point(348, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(11, 430);
            this.panelControl2.TabIndex = 11;
            // 
            // panelControl1
            // 
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(359, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(567, 11);
            this.panelControl1.TabIndex = 14;
            // 
            // panelControl3
            // 
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl3.Location = new System.Drawing.Point(926, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(11, 430);
            this.panelControl3.TabIndex = 15;
            // 
            // panelControl4
            // 
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl4.Location = new System.Drawing.Point(359, 419);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(567, 11);
            this.panelControl4.TabIndex = 16;
            // 
            // panelControl5
            // 
            this.panelControl5.Controls.Add(this.lueColor);
            this.panelControl5.Controls.Add(this.labelControl9);
            this.panelControl5.Controls.Add(this.luePurityGroup);
            this.panelControl5.Controls.Add(this.labelControl1);
            this.panelControl5.Controls.Add(this.labelControl7);
            this.panelControl5.Controls.Add(this.chkActive);
            this.panelControl5.Controls.Add(this.labelControl6);
            this.panelControl5.Controls.Add(this.labelControl3);
            this.panelControl5.Controls.Add(this.txtSeqNo);
            this.panelControl5.Controls.Add(this.labelControl5);
            this.panelControl5.Controls.Add(this.txtPurityName);
            this.panelControl5.Controls.Add(this.txtRemark);
            this.panelControl5.Controls.Add(this.labelControl4);
            this.panelControl5.Controls.Add(this.panelControl6);
            this.panelControl5.Controls.Add(this.labelControl2);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl5.Location = new System.Drawing.Point(359, 11);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(567, 408);
            this.panelControl5.TabIndex = 17;
            // 
            // lueColor
            // 
            this.lueColor.EnterMoveNextControl = true;
            this.lueColor.Location = new System.Drawing.Point(102, 58);
            this.lueColor.Name = "lueColor";
            this.lueColor.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueColor.Properties.Appearance.Options.UseFont = true;
            this.lueColor.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.lueColor.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueColor.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.lueColor.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.lueColor.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueColor.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("color_name", "Color"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("color_id", "Color Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueColor.Properties.NullText = "";
            this.lueColor.Properties.ShowHeader = false;
            this.lueColor.Size = new System.Drawing.Size(187, 22);
            this.lueColor.TabIndex = 2;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Location = new System.Drawing.Point(6, 62);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(79, 15);
            this.labelControl9.TabIndex = 22;
            this.labelControl9.Text = "Rough Shade";
            // 
            // luePurityGroup
            // 
            this.luePurityGroup.EnterMoveNextControl = true;
            this.luePurityGroup.Location = new System.Drawing.Point(102, 32);
            this.luePurityGroup.Name = "luePurityGroup";
            this.luePurityGroup.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.luePurityGroup.Properties.Appearance.Options.UseFont = true;
            this.luePurityGroup.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold);
            this.luePurityGroup.Properties.AppearanceDropDown.Options.UseFont = true;
            this.luePurityGroup.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold);
            this.luePurityGroup.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.luePurityGroup.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.luePurityGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luePurityGroup.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("purity_group", "Purity Group"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("purity_group_id", "Purity Group Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.luePurityGroup.Properties.NullText = "";
            this.luePurityGroup.Properties.ShowHeader = false;
            this.luePurityGroup.Size = new System.Drawing.Size(187, 22);
            this.luePurityGroup.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(94, 32);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(8, 16);
            this.labelControl1.TabIndex = 20;
            this.labelControl1.Text = "*";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(6, 36);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(78, 15);
            this.labelControl7.TabIndex = 19;
            this.labelControl7.Text = "Purity Group";
            // 
            // chkActive
            // 
            this.chkActive.EditValue = true;
            this.chkActive.EnterMoveNextControl = true;
            this.chkActive.Location = new System.Drawing.Point(309, 7);
            this.chkActive.Name = "chkActive";
            this.chkActive.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkActive.Properties.Appearance.Options.UseFont = true;
            this.chkActive.Properties.Caption = "Active";
            this.chkActive.Size = new System.Drawing.Size(67, 19);
            this.chkActive.TabIndex = 5;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Appearance.Options.UseForeColor = true;
            this.labelControl6.Location = new System.Drawing.Point(94, 84);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(8, 16);
            this.labelControl6.TabIndex = 17;
            this.labelControl6.Text = "*";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(94, 6);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(8, 16);
            this.labelControl3.TabIndex = 16;
            this.labelControl3.Text = "*";
            // 
            // txtSeqNo
            // 
            this.txtSeqNo.EnterMoveNextControl = true;
            this.txtSeqNo.Location = new System.Drawing.Point(102, 84);
            this.txtSeqNo.Name = "txtSeqNo";
            this.txtSeqNo.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSeqNo.Properties.Appearance.Options.UseFont = true;
            this.txtSeqNo.Size = new System.Drawing.Size(187, 22);
            this.txtSeqNo.TabIndex = 3;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(6, 87);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(77, 15);
            this.labelControl5.TabIndex = 13;
            this.labelControl5.Text = "Sequence No";
            // 
            // txtPurityName
            // 
            this.txtPurityName.EnterMoveNextControl = true;
            this.txtPurityName.Location = new System.Drawing.Point(102, 6);
            this.txtPurityName.Name = "txtPurityName";
            this.txtPurityName.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPurityName.Properties.Appearance.Options.UseFont = true;
            this.txtPurityName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPurityName.Size = new System.Drawing.Size(187, 22);
            this.txtPurityName.TabIndex = 0;
            // 
            // txtRemark
            // 
            this.txtRemark.EditValue = "";
            this.txtRemark.EnterMoveNextControl = true;
            this.txtRemark.Location = new System.Drawing.Point(102, 110);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Properties.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemark.Properties.Appearance.Options.UseFont = true;
            this.txtRemark.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemark.Size = new System.Drawing.Size(187, 49);
            this.txtRemark.TabIndex = 4;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(6, 112);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(50, 15);
            this.labelControl4.TabIndex = 11;
            this.labelControl4.Text = "Remark";
            // 
            // panelControl6
            // 
            this.panelControl6.Controls.Add(this.lblMode);
            this.panelControl6.Controls.Add(this.btnExit);
            this.panelControl6.Controls.Add(this.btnClear);
            this.panelControl6.Controls.Add(this.btnSave);
            this.panelControl6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl6.Location = new System.Drawing.Point(2, 358);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Size = new System.Drawing.Size(563, 48);
            this.panelControl6.TabIndex = 6;
            // 
            // lblMode
            // 
            this.lblMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMode.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblMode.Appearance.Options.UseFont = true;
            this.lblMode.Appearance.Options.UseForeColor = true;
            this.lblMode.Location = new System.Drawing.Point(148, 14);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(71, 16);
            this.lblMode.TabIndex = 13;
            this.lblMode.Text = "Add Mode";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.ImageOptions.Image = global::DERP.Properties.Resources.Exit;
            this.btnExit.Location = new System.Drawing.Point(455, 7);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(102, 32);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "E&xit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Appearance.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageOptions.Image = global::DERP.Properties.Resources.Clear;
            this.btnClear.Location = new System.Drawing.Point(347, 7);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(102, 32);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "&Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageOptions.Image = global::DERP.Properties.Resources.Save;
            this.btnSave.Location = new System.Drawing.Point(239, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 32);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(6, 10);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(76, 15);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Purity Name";
            // 
            // FrmClarityMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 430);
            this.Controls.Add(this.panelControl5);
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.dockPanel1);
            this.Name = "FrmClarityMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purity Master";
            this.Load += new System.EventHandler(this.FrmClarityMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPurityMaster)).EndInit();
            this.ContextMNExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurityMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            this.panelControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePurityGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeqNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPurityName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.panelControl6.ResumeLayout(false);
            this.panelControl6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.TextEdit txtSeqNo;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtPurityName;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl6;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraGrid.GridControl grdPurityMaster;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvPurityMaster;
        private DevExpress.XtraGrid.Columns.GridColumn clmPurityId;
        private DevExpress.XtraGrid.Columns.GridColumn clmPurity_name;
        private DevExpress.XtraGrid.Columns.GridColumn clmActive;
        private DevExpress.XtraGrid.Columns.GridColumn clmRemark;
        private DevExpress.XtraGrid.Columns.GridColumn clmSequenceno;
        private DevExpress.XtraEditors.LabelControl lblMode;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit chkActive;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private System.Windows.Forms.ContextMenuStrip ContextMNExport;
        private System.Windows.Forms.ToolStripMenuItem MNExportExcel;
        private System.Windows.Forms.ToolStripMenuItem MNExportPDF;
        private System.Windows.Forms.ToolStripMenuItem MNExportTEXT;
        private System.Windows.Forms.ToolStripMenuItem MNExportHTML;
        private System.Windows.Forms.ToolStripMenuItem MNExportRTF;
        private System.Windows.Forms.ToolStripMenuItem MNExportCSV;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraGrid.Columns.GridColumn clmPurityGroup;
        private DevExpress.XtraGrid.Columns.GridColumn clmPurityGroupId;
        private DevExpress.XtraEditors.LookUpEdit luePurityGroup;
        private DevExpress.XtraEditors.LookUpEdit lueColor;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraGrid.Columns.GridColumn clmShade;
        private DevExpress.XtraGrid.Columns.GridColumn clmRoughShadeId;
    }
}