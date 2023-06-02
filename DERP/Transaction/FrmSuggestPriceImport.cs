﻿using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DevExpress.XtraEditors;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction
{
    public partial class FrmSuggestPriceImport : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        SuggestPriceImport ObjSuggestPriceImport;

        DataTable DtControlSettings;
        DataTable DtSieve;
        DataTable DtAssort;
        DataTable DTabFile;
        DataTable DTab_Data;
        DataTable DAssort;
        DataTable DSieve;

        int m_numForm_id;
        int i;
        int IntRes;


        #endregion

        #region Constructor
        public FrmSuggestPriceImport()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            ObjSuggestPriceImport = new SuggestPriceImport();

            DtControlSettings = new DataTable();
            DtSieve = new SieveMaster().GetData();
            DtAssort = new AssortMaster().GetData();
            DTabFile = new DataTable();
            DTab_Data = new DataTable();
            DAssort = new DataTable();
            DSieve = new DataTable();

            m_numForm_id = 0;
            i = 0;
            IntRes = 0;
        }
        public void ShowForm()
        {
            ObjPer.FormName = this.Name.ToUpper();
            m_numForm_id = ObjPer.form_id;
            if (ObjPer.CheckPermission() == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionViwMsg);
                return;
            }
            if (Global.CheckDefault() == 0)
            {
                Global.Message("Please Check User Default Setting");
                this.Close();
                return;
            }
            Val.frmGenSet(this);
            AttachFormEvents();

            // for Dynamic Setting By Praful On 01022020

            if (Global.HideFormControls(Val.ToInt(ObjPer.form_id), this) != "")
            {
                Global.Message("Select First User Setting...Please Contact to Administrator...");
                return;
            }

            ControlSettingDT(Val.ToInt(ObjPer.form_id), this);
            AddGotFocusListener(this);
            this.KeyPreview = true;

            TabControlsToList(this.Controls);
            _tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();

            // End for Dynamic Setting By Praful On 01022020

            this.Show();
        }

        private void AddGotFocusListener(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                c.GotFocus += new EventHandler(Control_GotFocus);
                if (c.Controls.Count > 0)
                {
                    AddGotFocusListener(c);
                }
            }
        }
        private void Control_GotFocus(object sender, EventArgs e)
        {
            if (!((Control)sender).Name.ToString().Trim().Equals(string.Empty))
            {
                _NextEnteredControl = (Control)sender;
                if ((Control)sender is LookUpEdit)
                {
                    ((LookUpEdit)(Control)sender).ShowPopup();
                }
            }
        }

        private void TabControlsToList(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control.TabStop)
                    _tabControls.Add(control);
                if (control.HasChildren)
                    TabControlsToList(control.Controls);
            }
        }
        private void ControlSettingDT(int FormCode, Form pForm)
        {
            BLL.Validation Val = new BLL.Validation();
            Single_Setting ObjSingleSettings = new Single_Setting();
            Single_SettingProperty Property = new Single_SettingProperty();

            Property.role_id = Val.ToInt(BLL.GlobalDec.gEmployeeProperty.role_id);
            Property.form_id = Val.ToInt(FormCode);
            DataTable DtColSetting = ObjSingleSettings.GetData(Property);

            DataTable DtFilterColSetting = (from DataRow dr in DtColSetting.Rows
                                            where Val.ToBooleanToInt(dr["is_control"]) == 1
                                            && dr["column_type"].ToString() != "LABEL"
                                            select dr).CopyToDataTable();
            DevExpress.XtraLayout.LayoutControl l = new DevExpress.XtraLayout.LayoutControl();
            l.OptionsFocus.EnableAutoTabOrder = false;

            if (DtFilterColSetting.Rows.Count > 0)
            {
                DtControlSettings = DtFilterColSetting;
                foreach (Control item1 in pForm.Controls)
                {
                    ControllSettings(item1, DtFilterColSetting);
                }
            }
        }

        private static void ControllSettings(Control item2, DataTable DTab)
        {
            BLL.Validation Val = new BLL.Validation();

            //else
            {
                var VarControlSetting = (from DataRow dr in DTab.Rows
                                         where dr["column_name"].ToString() == item2.Name.ToString()
                                         select dr);

                if (VarControlSetting.Count() > 0)
                {
                    DataRow DRow = VarControlSetting.CopyToDataTable().Rows[0];
                    if (item2.Name.ToString() == Val.ToString(DRow["column_name"]))
                    {
                        if (!(item2 is TextEdit))
                        {
                            if (!(item2 is DateTimePicker))
                            {
                                if (!(item2 is DevExpress.XtraEditors.TextEdit))
                                {
                                    item2.Text = (Val.ToBooleanToInt(DRow["is_compulsory"]).Equals(0) ? Val.ToString(DRow["caption"]) : "* " + Val.ToString(DRow["caption"]));
                                }
                            }
                        }
                        if (Val.ToInt(DRow["tab_index"]) >= 0)
                        {
                            if (item2.CanSelect)
                                item2.TabStop = true;
                        }
                        else
                            item2.TabStop = false;
                        if (Val.ToBooleanToInt(DRow["is_visible"]).Equals(1))
                        {
                            item2.Visible = true;
                            item2.TabStop = true;
                        }
                        else
                        {
                            item2.Visible = false;
                            item2.TabStop = false;
                        }

                        item2.TabIndex = Val.ToInt(DRow["tab_index"]);
                        if (item2.TabIndex == 1)
                        {
                            item2.Select();
                            item2.Focus();
                        }
                        if (Val.ToBooleanToInt(DRow["is_editable"]).Equals(1))
                        {
                            item2.Enabled = true;
                        }
                        else
                        {
                            item2.Enabled = false;
                        }
                    }
                }
                else
                {
                    item2.TabStop = false;
                }
            }
            if (item2.Controls.Count > 0)
            {
                foreach (Control item1 in item2.Controls)
                {
                    ControllSettings(item1, DTab);
                }
            }
        }

        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            //objBOFormEvents.ObjToDispose.Add(objCountry);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region "Events" 
        private void FrmPriceImport_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPRate(lueRateType);
                Global.LOOKUPCurrency(lueCurrency);
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void FrmPriceImport_Shown(object sender, System.EventArgs e)
        {
            dtpDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            dtpDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            dtpDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpDate.Properties.CharacterCasing = CharacterCasing.Upper;

            dtpDate.EditValue = DateTime.Now;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var result = DateTime.Compare(Convert.ToDateTime(dtpDate.Text), DateTime.Today);
                if (result > 0)
                {
                    Global.Message("Date Not Be Greater Than Today Date");
                    dtpDate.Focus();
                    return;

                }
                ObjPer.SetFormPer();
                if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }

                List<ListError> lstError = new List<ListError>();
                Dictionary<Control, string> rtnCtrls = new Dictionary<Control, string>();
                rtnCtrls = Global.CheckCompulsoryControls(Val.ToInt(ObjPer.form_id), this);
                if (rtnCtrls.Count > 0)
                {
                    foreach (KeyValuePair<Control, string> entry in rtnCtrls)
                    {
                        if (entry.Key is DevExpress.XtraEditors.LookUpEdit || entry.Key is DevExpress.XtraEditors.DateEdit || entry.Key is DevExpress.XtraEditors.TextEdit)
                        {
                            lstError.Add(new ListError(13, entry.Value));
                        }
                    }
                    rtnCtrls.First().Key.Focus();
                    BLL.General.ShowErrors(lstError);
                    Cursor.Current = Cursors.Arrow;
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                DataTable dt = new DataTable();
                dt = ObjSuggestPriceImport.GetData(Val.ToString(dtpDate.Text), Val.ToInt(lueRateType.EditValue), Val.ToInt(lueCurrency.EditValue));
                if (dt.Rows.Count == 0)
                {
                    int rowNo = 1;

                    DTab_Data = (DataTable)grdSuggestPriceImport.DataSource;
                    if (DTab_Data != null)
                    {
                        foreach (DataRow DRow in DTab_Data.Rows)
                        {
                            if (DRow["assort"] != null)
                            {
                                if (Val.ToString(DRow["assort"]) != "")
                                {
                                    if (DtAssort.Select("assort_name ='" + Val.ToString(DRow["assort"]) + "'").Length > 0)
                                    {
                                        DAssort = DtAssort.Select("assort_name ='" + Val.ToString(DRow["assort"]) + "'").CopyToDataTable();
                                    }
                                    else
                                    {
                                        Global.Message("Assort Not found in Master" + Val.ToString(DRow["assort"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                                else
                                {
                                    Global.Message("Assort Name Blank at Row No :" + rowNo);
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                            }
                            else
                            {
                                Global.Message("Assort Name are not found :" + Val.ToString(DRow["assort"]));
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            if (DRow["sieve"] != null)
                            {
                                if (Val.ToString(DRow["sieve"]) != "")
                                {
                                    if (DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").Length > 0)
                                    {
                                        DSieve = DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").CopyToDataTable();
                                    }
                                    else
                                    {
                                        Global.Message("Sieve Not found in Master" + Val.ToString(DRow["sieve"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                                else
                                {
                                    Global.Message("Sieve Name Blank at Row No :" + rowNo);
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                            }
                            else
                            {
                                Global.Message("Sieve Name are not found :" + Val.ToString(DRow["sieve"]));
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            rowNo++;
                        }
                    }
                    else
                    {
                        Global.Message("Data not found in Grid Please check data");
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                    panelProgress.Visible = true;
                    backgroundWorker_SuggestPriceImport.RunWorkerAsync();

                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    Global.Message("Price already exists.");
                    return;
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                this.Cursor = Cursors.Default;
                return;
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFileName.Text = "";
            dtpDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            dtpDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            dtpDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpDate.Properties.CharacterCasing = CharacterCasing.Upper;

            dtpDate.EditValue = DateTime.Now;
            lueCurrency.EditValue = null;
            lueRateType.EditValue = null;
            DTabFile.Rows.Clear();
            grdSuggestPriceImport.DataSource = null;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                List<ListError> lstError = new List<ListError>();
                Dictionary<Control, string> rtnCtrls = new Dictionary<Control, string>();
                rtnCtrls = Global.CheckCompulsoryControls(Val.ToInt(ObjPer.form_id), this);
                if (rtnCtrls.Count > 0)
                {
                    foreach (KeyValuePair<Control, string> entry in rtnCtrls)
                    {
                        if (entry.Key is DevExpress.XtraEditors.LookUpEdit || entry.Key is DevExpress.XtraEditors.DateEdit || entry.Key is DevExpress.XtraEditors.TextEdit)
                        {
                            lstError.Add(new ListError(13, entry.Value));
                        }
                    }
                    rtnCtrls.First().Key.Focus();
                    BLL.General.ShowErrors(lstError);
                    Cursor.Current = Cursors.Arrow;
                    return;
                }

                // m_blnsave = true;
                //if (!ValidateDetails())
                //{
                //    m_blnsave = false;
                //    btnSave.Enabled = true;
                //    return;
                //}

                OpenFileDialog OpenDialog = new OpenFileDialog();
                if (OpenDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                txtFileName.Text = OpenDialog.FileName;
                OpenDialog.Dispose();
                OpenDialog = null;

                if (File.Exists(txtFileName.Text) == false)
                {
                    Global.Message("File Is Not Exists To The Path");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                grdSuggestPriceImport.DataSource = null;

                if (txtFileName.Text.Length != 0)
                {
                    using (var pck = new ExcelPackage(new FileInfo(txtFileName.Text)))
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                        DTabFile = WorksheetToDataTable(ws, true);
                    }
                }
                grdSuggestPriceImport.DataSource = DTabFile;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                this.Cursor = Cursors.Default;
                return;
            }
        }
        private void lblFormatSample_Click(object sender, EventArgs e)
        {
            Global.CopyFormat(System.Windows.Forms.Application.StartupPath + @"\FORMAT\PriceImport.xlsx", "PriceImport.xlsx", "xlsx");
        }
        public void SetControlPropertyValue(Control oControl, string propName, object propValue)
        {
            if (oControl.InvokeRequired)
            {
                SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                oControl.Invoke(d, new object[]
                        {
                            oControl,
                            propName,
                            propValue
                        });
            }
            else
            {
                Type t = oControl.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (PropertyInfo p in props)
                {
                    if ((p.Name.ToUpper() == propName.ToUpper()))
                    {
                        p.SetValue(oControl, propValue, null);
                    }
                }
            }
        }
        private void backgroundWorker_PriceImport_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn = new BeginTranConnection(true, false);
                }
                else
                {
                    Conn = new BeginTranConnection(false, true);
                }
                SuggestPrice_ImportProperty ObjSuggestPriceImportProperty = new SuggestPrice_ImportProperty();
                try
                {
                    IntRes = 0;

                    int IntCounter = 0;
                    int Count = 0;
                    int TotalCount = DTab_Data.Rows.Count;


                    ObjSuggestPriceImportProperty.suggest_rate_date = Val.DBDate(dtpDate.Text);
                    ObjSuggestPriceImportProperty.rate_type_id = Val.ToInt32(lueRateType.EditValue);
                    ObjSuggestPriceImportProperty.currency_id = Val.ToInt32(lueCurrency.EditValue);

                    int IntResMst = ObjSuggestPriceImport.MasterSave(ObjSuggestPriceImportProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                    int Rate_id = ObjSuggestPriceImportProperty.suggest_rate_id;

                    if (IntResMst > 0)
                    {
                        foreach (DataRow DRow in DTab_Data.Rows)
                        {

                            if (DRow["assort"] != null)
                            {
                                if (Val.ToString(DRow["assort"]) != "")
                                {
                                    if (DtAssort.Select("assort_name ='" + Val.ToString(DRow["assort"]) + "'").Length > 0)
                                    {
                                        DAssort = DtAssort.Select("assort_name ='" + Val.ToString(DRow["assort"]) + "'").CopyToDataTable();
                                    }
                                }
                            }

                            if (DRow["sieve"] != null)
                            {
                                if (Val.ToString(DRow["sieve"]) != "")
                                {
                                    if (DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").Length > 0)
                                    {
                                        DSieve = DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").CopyToDataTable();
                                    }
                                }
                            }
                            ObjSuggestPriceImportProperty = new SuggestPrice_ImportProperty();
                            ObjSuggestPriceImportProperty.suggest_rate_id = Val.ToInt(Rate_id);
                            ObjSuggestPriceImportProperty.assort_id = Val.ToInt(DAssort.Rows[0]["assort_id"]);
                            ObjSuggestPriceImportProperty.sieve_id = Val.ToInt(DSieve.Rows[0]["sieve_id"]);
                            ObjSuggestPriceImportProperty.rate = Val.ToDecimal(DTab_Data.Rows[i]["rate"]);

                            i++;
                            ObjSuggestPriceImportProperty.sequence_no = i;
                            IntRes = ObjSuggestPriceImport.DetailSave(ObjSuggestPriceImportProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                            Count++;
                            IntCounter++;
                            IntRes++;
                            SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                        }
                        if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                        {
                            Conn.Inter1.Commit();
                        }
                        else
                        {
                            Conn.Inter2.Commit();
                        }
                    }
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                    {
                        Conn.Inter1.Rollback();
                    }
                    else
                    {
                        Conn.Inter2.Rollback();
                    }
                    Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
                finally
                {
                    ObjSuggestPriceImportProperty = null;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn.Inter1.Rollback();
                }
                else
                {
                    Conn.Inter2.Rollback();
                }
                Conn = null;
                Global.Message(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorker_PriceImport_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    Global.Confirm("Suggest Price Import Data Save Successfully");
                    i = 0;
                    btnClear_Click(null, null);
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    Global.Confirm("Error In Suggest Price Import");
                    this.Cursor = Cursors.Default;
                    dtpDate.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        #endregion

        #region "Function"
        public System.Data.DataTable WorksheetToDataTable(ExcelWorksheet ws, bool hasHeader = true)
        {
            System.Data.DataTable dt = new System.Data.DataTable(ws.Name);
            int totalCols = ws.Dimension.End.Column;
            int totalRows = ws.Dimension.End.Row;
            int startRow = hasHeader ? 2 : 1;
            ExcelRange wsRow;
            DataRow dr;
            foreach (var firstRowCell in ws.Cells[1, 1, 1, totalCols])
            {
                dt.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
            }

            for (int rowNum = startRow; rowNum <= totalRows; rowNum++)
            {
                wsRow = ws.Cells[rowNum, 1, rowNum, totalCols];
                dr = dt.NewRow();
                foreach (var cell in wsRow)
                {
                    dr[cell.Start.Column - 1] = cell.Text;
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }
        private void Export(string format, string dlgHeader, string dlgFilter)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = format;
                svDialog.Title = dlgHeader;
                svDialog.FileName = "Report";
                svDialog.Filter = dlgFilter;
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    string Filepath = svDialog.FileName;

                    switch (format)
                    {
                        case "pdf":
                            dgvSuggestPriceImport.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvSuggestPriceImport.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvSuggestPriceImport.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvSuggestPriceImport.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvSuggestPriceImport.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvSuggestPriceImport.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvSuggestPriceImport.ExportToCsv(Filepath);
                            break;
                    }

                    if (format.Equals(Exports.xlsx.ToString()))
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open Excel File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                    else if (format.Equals(Exports.pdf.ToString()))
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open PDF File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                    else
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString(), "Error in Export");
            }
        }

        #endregion

        #region Export Grid
        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            //Global.Export("xlsx", dgvRoughClarityMaster);
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void MNExportPDF_Click(object sender, EventArgs e)
        {
            // Global.Export("pdf", dgvRoughClarityMaster);
            Export("pdf", "Export Report to PDF", "PDF (*.PDF)|*.PDF");
        }
        private void MNExportTEXT_Click(object sender, EventArgs e)
        {
            Export("txt", "Export to Text", "Text files (*.txt)|*.txt|All files (*.*)|*.*");
        }

        private void MNExportHTML_Click(object sender, EventArgs e)
        {
            Export("html", "Export to HTML", "Html files (*.html)|*.html|Htm files (*.htm)|*.htm");
        }

        private void MNExportRTF_Click(object sender, EventArgs e)
        {
            Export("rtf", "Export to RTF", "Word (*.doc) |*.doc;*.rtf|(*.txt) |*.txt|(*.*) |*.*");
        }

        private void MNExportCSV_Click(object sender, EventArgs e)
        {
            Export("csv", "Export Report to CSVB", "csv (*.csv)|*.csv");
        }
        #endregion
    }
}
