﻿using BLL;
using BLL.FunctionClasses.Rejection;
using BLL.PropertyClasses.Rejection;
using DERP.Class;
using DREP.Rejection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Rejection
{
    public partial class FrmMfgRejectionToMakableTransfer : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;
        DataTable m_dtbParam;
        DataTable m_dtCut;
        DataTable m_DtProcess;
        DataTable m_dtbType;
        DataTable m_dtbRejectionLot;
        DataTable m_dtbRejectionLotList;
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        MFGRejectionToMakableTransfer objRejectionTrf;
        MFGRejectionInternalTransfer objRejInternalTransfer;
        MFGRoughStockEntry objRoughStockEntry = new MFGRoughStockEntry();
        int m_numForm_id;
        int IntRes;
        int m_Srno;
        int m_update_srno;
        Int64 Lot_SrNo;
        decimal m_numSummRate = 0;
        Int64 m_transfer_id;
        bool m_blnadd;
        bool m_blnsave;

        #endregion

        #region Constructor
        public FrmMfgRejectionToMakableTransfer()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objRejectionTrf = new MFGRejectionToMakableTransfer();
            objRejInternalTransfer = new MFGRejectionInternalTransfer();
            m_dtbParam = new DataTable();
            m_dtCut = new DataTable();
            m_DtProcess = new DataTable();
            m_dtbRejectionLot = new DataTable();
            m_dtbRejectionLotList = new DataTable();
            m_numForm_id = 0;
            IntRes = 0;
            m_Srno = 0;
            m_update_srno = 1;
            m_blnadd = new bool();
            m_blnsave = new bool();
            m_transfer_id = 0;
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
            this.Show();
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objRejectionTrf);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion   

        #region Events     
        private void FrmRejectionToMakableTransferConfirm_Load(object sender, EventArgs e)
        {
            try
            {
                dtpConfirmDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpConfirmDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpConfirmDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpConfirmDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpConfirmDate.EditValue = DateTime.Now;

                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = DateTime.Now;

                dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpToDate.EditValue = DateTime.Now;

                m_dtbType = new DataTable();
                m_dtbType.Columns.Add("type");
                m_dtbType.Rows.Add("ROUGH");
                m_dtbType.Rows.Add("REJECTION");

                Global.LOOKUPRejSection(lueSection);
                Global.LOOKUPRejPurity(luePurity);
                Global.LOOKUPManager(lueManager);
                Global.LOOKUPRejKapan(lueKapan);

                lueType.Properties.DataSource = m_dtbType;
                lueType.Properties.ValueMember = "type";
                lueType.Properties.DisplayMember = "type";
                ClearDetails();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPer.SetFormPer();
                if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }
                btnSave.Enabled = false;
                m_blnsave = true;
                m_blnadd = false;
                if (!ValidateDetails())
                {
                    m_blnsave = false;
                    btnSave.Enabled = true;
                    return;
                }

                m_dtbRejectionLot.AcceptChanges();
                if (Val.ToString(lblMode.Text) == "Add Mode")
                {
                    if (m_dtbRejectionLot.Rows.Count > 0)
                    {
                        DialogResult result = MessageBox.Show("Do you want to Save data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                        if (result != DialogResult.Yes)
                        {
                            btnSave.Enabled = true;
                            return;
                        }
                        DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                        panelProgress.Visible = true;
                        backgroundWorker_RejectionTransfer.RunWorkerAsync();
                    }
                    else
                    {
                        General.ShowErrors("Atleast 1 Lot must be in a grid.");
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Do you want to Update data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes)
                    {
                        btnSave.Enabled = true;
                        return;
                    }
                    DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                    panelProgress.Visible = true;
                    backgroundWorker_RejectionTransfer.RunWorkerAsync();
                }
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                this.Cursor = Cursors.Default;
                btnSave.Enabled = true;
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }
        private void lueSection_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMFGRejectionSectionMaster frmRejectionSection = new FrmMFGRejectionSectionMaster();
                frmRejectionSection.ShowDialog();
                Global.LOOKUPRejSection(lueSection);
            }
        }
        private void lueKapan_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMFGRoughStockEntry objRoughStockEntry = new FrmMFGRoughStockEntry();
                objRoughStockEntry.ShowDialog();
                Global.LOOKUPRejKapan(lueKapan);
            }
        }
        private void luePurity_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgRejectionPurityMaster objRejectionPurity = new FrmMfgRejectionPurityMaster();
                objRejectionPurity.ShowDialog();
                Global.LOOKUPRejPurity(luePurity);
            }
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
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (GlobalDec.gEmployeeProperty.user_name != "JAYESH")
            {
                Global.Message("Don't have Permission...So Please Contact to Administrator");
                return;
            }
            if (Val.ToString(lblMode.Text) != "Edit Mode")
            {
                return;
            }
            btnDelete.Enabled = false;
            DialogResult result = MessageBox.Show("Do you want to Delete data?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                btnDelete.Enabled = true;
                return;
            }
            MFGRejectionToMakable_TransferProperty RejTransferProperty = new MFGRejectionToMakable_TransferProperty();
            try
            {
                Conn = new BeginTranConnection(true, false);
                foreach (DataRow DRow in m_dtbRejectionLot.Rows)
                {
                    RejTransferProperty.transfer_id = Val.ToInt64(DRow["transfer_id"]);
                    RejTransferProperty.transfer_date = Val.DBDate(dtpConfirmDate.Text);
                    RejTransferProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                    RejTransferProperty.type = Val.ToString(lueType.EditValue);
                    RejTransferProperty.section_id = Val.ToInt64(DRow["section_id"]);
                    RejTransferProperty.purity_id = Val.ToInt(DRow["purity_id"]);
                    RejTransferProperty.manager_id = Val.ToInt64(lueManager.EditValue);
                    RejTransferProperty.carat = Val.ToDecimal(DRow["carat"]);
                    RejTransferProperty.rate = Val.ToDecimal(DRow["rate"]);
                    RejTransferProperty.amount = Val.ToDecimal(DRow["amount"]);
                    RejTransferProperty.lot_srno = Val.ToInt64(Lot_SrNo);
                    IntRes = objRejectionTrf.Delete(RejTransferProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                    IntRes++;
                }
                Conn.Inter1.Commit();

                if (IntRes > 0)
                {
                    Global.Confirm("Rejection To Makable Transfer Data Deleted Succesfully");
                    btnDelete.Enabled = true;
                    btnClear_Click(null, null);
                    PopulateDetails();
                }
                else
                {
                    Global.Confirm("Error In Rejection To Makable Transfer Data");
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                General.ShowErrors(ex.ToString());
                btnDelete.Enabled = true;
                return;
            }
            finally
            {
                RejTransferProperty = null;
                btnDelete.Enabled = true;
            }
        }
        private void backgroundWorker_RejectionTransfer_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;
                MFGRejectionToMakable_TransferProperty RejTransferProperty = new MFGRejectionToMakable_TransferProperty();
                objRejectionTrf = new MFGRejectionToMakableTransfer();
                try
                {
                    IntRes = 0;
                    Lot_SrNo = 0;
                    Conn = new BeginTranConnection(true, false);
                    int IntCounter = 0;
                    int Count = 0;

                    //if (lblMode.Text == "Add Mode")
                    //{
                    m_dtbRejectionLot.AcceptChanges();
                    //m_dtbRejectionLot.Rows.Clear();
                    m_dtbRejectionLot = (DataTable)grdRejToMakTransfer.DataSource;

                    int TotalCount = m_dtbRejectionLot.Rows.Count;

                    foreach (DataRow DRow in m_dtbRejectionLot.Rows)
                    {
                        RejTransferProperty.transfer_id = Val.ToInt64(DRow["transfer_id"]);
                        RejTransferProperty.transfer_date = Val.DBDate(dtpConfirmDate.Text);
                        RejTransferProperty.kapan_id = Val.ToInt64(DRow["kapan_id"]);
                        RejTransferProperty.type = Val.ToString(DRow["type"]);
                        RejTransferProperty.section_id = Val.ToInt64(DRow["section_id"]);
                        RejTransferProperty.purity_id = Val.ToInt64(DRow["purity_id"]);
                        RejTransferProperty.manager_id = Val.ToInt64(DRow["manager_id"]);
                        RejTransferProperty.pcs = Val.ToDecimal(DRow["pcs"]);
                        RejTransferProperty.carat = Val.ToDecimal(DRow["carat"]);
                        RejTransferProperty.rate = Val.ToDecimal(DRow["rate"]);
                        RejTransferProperty.amount = Val.ToDecimal(DRow["amount"]);
                        if (Val.ToInt64(DRow["lot_srno"]) == 0)
                        {
                            RejTransferProperty.lot_srno = Lot_SrNo;
                        }
                        else
                        {
                            RejTransferProperty.lot_srno = Val.ToInt64(DRow["lot_srno"]);
                        }

                        RejTransferProperty = objRejectionTrf.Save(RejTransferProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        Lot_SrNo = Val.ToInt64(RejTransferProperty.lot_srno);
                        Count++;
                        IntCounter++;
                        IntRes++;
                        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                    }
                    //}
                    //else
                    //{
                    //    RejTransferProperty.lot_srno = Val.ToInt(lblMode.Tag);
                    //    RejTransferProperty.transfer_date = Val.DBDate(dtpConfirmDate.Text);
                    //    RejTransferProperty.type = Val.ToString(lueType.EditValue);
                    //    RejTransferProperty.section_id = Val.ToInt64(lueSection.EditValue);
                    //    RejTransferProperty.manager_id = Val.ToInt(lueManager.EditValue);
                    //    IntRes = objRejectionTrf.Update(RejTransferProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                    //}
                    //if ((Val.ToString(lblMode.Text) == "Add Mode" && Lot_SrNo == 0) || (Val.ToString(lblMode.Text) == "Edit Mode" && IntRes <= 0))
                    //{
                    //    Global.Confirm("Error In Rejection Transfer");
                    //}

                    Conn.Inter1.Commit();
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    Conn.Inter1.Rollback();
                    Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
                finally
                {
                    RejTransferProperty = null;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                General.ShowErrors(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorker_RejectionTransfer_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (Lot_SrNo > 0 && Val.ToString(lblMode.Text) == "Add Mode")
                {
                    Global.Confirm("Rejection Transfer Successfully");
                    //m_dtbRejectionLot = null;
                    btnClear_Click(null, null);
                    PopulateDetails();
                    //GetData();
                    //CalculateTotal();

                    lueKapan.Focus();
                    this.Cursor = Cursors.Default;
                }
                else if (Val.ToString(lblMode.Text) == "Edit Mode" && IntRes > 0)
                {
                    Global.Confirm("Update Rejection Transfer Successfully");
                    //m_dtbRejectionLot = null;
                    btnClear_Click(null, null);
                    PopulateDetails();
                    lblMode.Tag = 0;
                    //GetData();
                    //CalculateTotal();

                    lueKapan.Focus();
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    Global.Confirm("Error In Rejection Transfer");
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void btnLSearch_Click(object sender, EventArgs e)
        {
            if (!PopulateDetails())
                return;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (AddInGrid())
                {
                    txtPcs.Text = "0";
                    txtCarat.Text = "0";
                    txtRate.Text = "0";

                    txtAmount.Text = "0";
                    luePurity.EditValue = null;
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }
        private void txtCarat_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text)), 0));
        }
        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {
            txtAmount.Text = Val.ToString(Math.Round(Val.ToDecimal(Val.ToDecimal(txtCarat.Text) * Val.ToDecimal(txtRate.Text)), 0));
        }
        private void luePurity_EditValueChanged(object sender, EventArgs e)
        {
            if (Val.ToString(luePurity.Text) != "")
            {
                DataTable dtStock = new DataTable();
                dtStock = objRejInternalTransfer.Rej_Purity_Stock_Data(Val.ToInt64(luePurity.EditValue));
                if (dtStock.Rows.Count > 0)
                {
                    lblOsCarat.Text = Val.ToString(Val.ToDecimal(dtStock.Rows[0]["carat"]));
                    txtPurityGroup.Text = Val.ToString(dtStock.Rows[0]["group_name"]);
                    lblDiffCarat.Text = Val.ToString(Val.ToDecimal(dtStock.Rows[0]["carat"]));
                }
                else
                {
                    lblOsCarat.Text = Val.ToString("0");
                    txtPurityGroup.Text = Val.ToString("");
                    lblDiffCarat.Text = "0";
                }
            }
        }
        private void txtCarat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        #region GridEvents
        private void dgvRejToMakTransfer_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SEL")
            {
                if (Val.ToBoolean(dgvRejToMakTransfer.GetRowCellValue(e.RowHandle, "SEL")) == true)
                {
                    dgvRejToMakTransfer.SetRowCellValue(e.RowHandle, "SEL", false);
                }
                else
                {
                    dgvRejToMakTransfer.SetRowCellValue(e.RowHandle, "SEL", true);
                }
            }
        }
        private void dgvRejToMakTransfer_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvRejToMakTransfer.GetDataRow(e.RowHandle);
                        btnAdd.Text = "&Update";

                        luePurity.EditValue = Val.ToInt64(Drow["purity_id"]);
                        lueType.EditValue = Val.ToString(Drow["type"]);
                        lueKapan.EditValue = Val.ToInt64(Drow["kapan_id"]);
                        lueSection.EditValue = Val.ToInt64(Drow["section_id"]);
                        txtPurityGroup.Text = Val.ToString(Drow["purity_group"]);
                        lueManager.EditValue = Val.ToInt64(Drow["manager_id"]);
                        txtPcs.Text = Val.ToString(Drow["pcs"]);
                        txtCarat.Text = Val.ToString(Drow["carat"]);
                        txtRate.Text = Val.ToString(Drow["rate"]);
                        m_transfer_id = Val.ToInt(Drow["transfer_id"]);
                        m_update_srno = Val.ToInt(Drow["sr_no"]);
                        decimal Diff_Carat = Math.Round(Val.ToDecimal(clmCarat.SummaryItem.SummaryValue), 3) + Val.ToDecimal(lblOsCarat.Text);
                        lblDiffCarat.Text = Diff_Carat.ToString();
                        //m_dtbRejectionLot = objRejectionTrf.GetData(Val.ToInt(Drow["lot_srno"]));
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void dgvRejToMakTransferList_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvRejToMakTransferList.GetDataRow(e.RowHandle);
                        objRejectionTrf = new MFGRejectionToMakableTransfer();
                        m_dtbRejectionLot = new DataTable();
                        dtpConfirmDate.EditValue = Val.ToString(Drow["transfer_date"]);
                        m_dtbRejectionLot = objRejectionTrf.GetData(Val.ToInt64(Drow["lot_srno"]));
                        Lot_SrNo = Val.ToInt64(Drow["lot_srno"]);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Lot_SrNo);
                        grdRejToMakTransfer.DataSource = m_dtbRejectionLot;
                        ttlbRejectionTransfer.SelectedTabPage = xtbpgEntry;
                        decimal Diff_Carat = Math.Round(Val.ToDecimal(clmCarat.SummaryItem.SummaryValue), 3) + Val.ToDecimal(lblOsCarat.Text);
                        lblDiffCarat.Text = Diff_Carat.ToString();
                        btnDelete.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }

        private void dgvRejToMakTransferList_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (Val.ToDecimal(clmLAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmLCarat.SummaryItem.SummaryValue) > 0)
            {
                m_numSummRate = Math.Round((Val.ToDecimal(clmLAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmLCarat.SummaryItem.SummaryValue)), 3, MidpointRounding.AwayFromZero);
            }
            else
            {
                m_numSummRate = 0;
            }
            if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    e.TotalValue = m_numSummRate;
            }
        }
        private void dgvRejToMakTransfer_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmCarat.SummaryItem.SummaryValue) > 0)
            {
                m_numSummRate = Math.Round((Val.ToDecimal(clmAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmCarat.SummaryItem.SummaryValue)), 3, MidpointRounding.AwayFromZero);
            }
            else
            {
                m_numSummRate = 0;
            }
            if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    e.TotalValue = m_numSummRate;
            }
        }

        #endregion

        #endregion

        #region Functions  
        private bool ValidateDetails()
        {

            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (m_blnsave)
                {
                    if (m_dtbRejectionLot.Rows.Count == 0)
                    {
                        lstError.Add(new ListError(22, "Record"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                        }
                    }

                    if (Val.ToString(dtpConfirmDate.Text) == string.Empty)
                    {
                        lstError.Add(new ListError(22, "Date"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            dtpConfirmDate.Focus();
                        }
                    }
                    if (lblMode.Text == "Edit Mode")
                    {

                    }
                    else
                    {
                        if (lueKapan.Text.Length == 0)
                        {
                            lstError.Add(new ListError(12, "Kapan"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                lueKapan.Focus();
                            }
                        }
                        if (lueManager.Text.Length == 0)
                        {
                            lstError.Add(new ListError(12, "Manager"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                lueManager.Focus();
                            }
                        }
                        if (lueType.Text.Length == 0)
                        {
                            lstError.Add(new ListError(12, "Type"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                lueType.Focus();
                            }
                        }
                        if (lueSection.Text.Length == 0)
                        {
                            lstError.Add(new ListError(12, "Section"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                lueSection.Focus();
                            }
                        }
                    }
                }
                if (m_blnadd)
                {
                    if (lueSection.Text.Length == 0)
                    {
                        lstError.Add(new ListError(12, "Section"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueSection.Focus();
                        }
                    }
                    if (luePurity.Text.Length == 0)
                    {
                        lstError.Add(new ListError(12, "Purity"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            luePurity.Focus();
                        }
                    }
                    //if (Val.ToDecimal(txtPcs.Text) == 0)
                    //{
                    //    lstError.Add(new ListError(5, "Pcs Not Be Blank!!"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        txtPcs.Focus();
                    //    }
                    //}
                    if (Val.ToDecimal(txtCarat.Text) == 0)
                    {
                        lstError.Add(new ListError(5, "Carat Not Be Blank!!"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
                        }
                    }
                    if (Val.ToDecimal(txtRate.Text) == 0)
                    {
                        lstError.Add(new ListError(5, "Rate Not Be Blank!!"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtRate.Focus();
                        }
                    }
                    if (Val.ToDecimal(txtAmount.Text) == 0)
                    {
                        lstError.Add(new ListError(5, "Amount Not Be Blank!!"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            txtCarat.Focus();
                        }
                    }
                    if (lblMode.Text == "Edit Mode")
                    {

                    }
                    else
                    {
                        if (Val.ToDecimal(txtCarat.Text) > Val.ToDecimal(lblOsCarat.Text) + Val.ToDecimal(lblDiffCarat.Text))
                        {
                            lstError.Add(new ListError(5, "Carat Not Be More Than Balance Carat!!"));
                            if (!blnFocus)
                            {
                                blnFocus = true;
                                txtCarat.Focus();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        private bool PopulateDetails()
        {
            objRejectionTrf = new MFGRejectionToMakableTransfer();
            bool blnReturn = true;
            DateTime datFromDate = DateTime.MinValue;
            DateTime datToDate = DateTime.MinValue;
            try
            {
                m_dtbRejectionLotList = objRejectionTrf.GetSearchList(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text));

                if (m_dtbRejectionLotList.Rows.Count == 0)
                {
                    Global.Message("Data Not Found");
                    blnReturn = false;
                }
                grdRejToMakTransferList.DataSource = m_dtbRejectionLotList;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
                objRejectionTrf = null;
            }
            return blnReturn;
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
                            dgvRejToMakTransfer.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvRejToMakTransfer.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvRejToMakTransfer.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvRejToMakTransfer.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvRejToMakTransfer.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvRejToMakTransfer.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvRejToMakTransfer.ExportToCsv(Filepath);
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
        private void Export_List(string format, string dlgHeader, string dlgFilter)
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
                            dgvRejToMakTransferList.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvRejToMakTransferList.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvRejToMakTransferList.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvRejToMakTransferList.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvRejToMakTransferList.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvRejToMakTransferList.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvRejToMakTransferList.ExportToCsv(Filepath);
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

        private bool AddInGrid()
        {
            bool blnReturn = true;
            try
            {
                m_blnadd = true;
                m_blnsave = false;

                if (!ValidateDetails())
                {
                    m_blnadd = false;
                    blnReturn = false;
                    return blnReturn;
                }
                if (btnAdd.Text == "&Add")
                {
                    DataRow[] dr = m_dtbRejectionLot.Select("purity_id = '" + Val.ToInt32(luePurity.EditValue) + "'");

                    if (dr.Count() == 1)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        luePurity.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }

                    DataRow drwNew = m_dtbRejectionLot.NewRow();
                    decimal numReturnPcs = Val.ToDecimal(txtPcs.Text);
                    decimal numReturnCarat = Val.ToDecimal(txtCarat.Text);
                    decimal numRate = Val.ToDecimal(txtRate.Text);
                    decimal numAmount = Val.ToDecimal(txtAmount.Text);

                    drwNew["transfer_id"] = Val.ToInt64(0);
                    drwNew["type"] = Val.ToString(lueType.Text);
                    drwNew["purity_id"] = Val.ToInt64(luePurity.EditValue);
                    drwNew["purity_name"] = Val.ToString(luePurity.Text);
                    drwNew["kapan_id"] = Val.ToInt64(lueKapan.EditValue);
                    drwNew["kapan_no"] = Val.ToString(lueKapan.Text);
                    drwNew["manager_id"] = Val.ToInt64(lueManager.EditValue);
                    drwNew["manager_name"] = Val.ToString(lueManager.Text);
                    drwNew["purity_group"] = Val.ToString(txtPurityGroup.Text);
                    drwNew["section_id"] = Val.ToInt64(lueSection.EditValue);
                    drwNew["section_name"] = Val.ToString(lueSection.Text);
                    drwNew["pcs"] = numReturnPcs;
                    drwNew["carat"] = numReturnCarat;
                    drwNew["rate"] = numRate;
                    drwNew["amount"] = numAmount;

                    m_Srno = m_Srno + 1;
                    drwNew["sr_no"] = Val.ToInt(m_Srno);
                    drwNew["lot_srno"] = Val.ToInt(0);

                    m_dtbRejectionLot.Rows.Add(drwNew);
                    m_Srno++;
                }
                else if (btnAdd.Text == "&Update")
                {
                    DataRow[] dr = m_dtbRejectionLot.Select("purity_id = '" + Val.ToInt32(luePurity.EditValue) + "' and sr_no <>'" + m_update_srno + "'");
                    if (dr.Count() == 1)
                    {
                        Global.Message("Record already exists in grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        luePurity.Focus();
                        blnReturn = false;
                        return blnReturn;
                    }
                    for (int i = 0; i < m_dtbRejectionLot.Rows.Count; i++)
                    {
                        if (m_dtbRejectionLot.Select("transfer_id ='" + m_transfer_id + "' AND sr_no = '" + m_update_srno + "'").Length > 0)
                        {
                            if (m_dtbRejectionLot.Rows[m_update_srno - 1]["transfer_id"].ToString() == m_transfer_id.ToString())
                            {
                                m_dtbRejectionLot.Rows[m_update_srno - 1]["type"] = Val.ToString(lueType.EditValue);
                                m_dtbRejectionLot.Rows[m_update_srno - 1]["purity_id"] = Val.ToInt64(luePurity.EditValue).ToString();
                                m_dtbRejectionLot.Rows[m_update_srno - 1]["purity_name"] = Val.ToString(luePurity.Text).ToString();
                                m_dtbRejectionLot.Rows[m_update_srno - 1]["kapan_id"] = Val.ToInt64(lueKapan.EditValue).ToString();
                                m_dtbRejectionLot.Rows[m_update_srno - 1]["kapan_no"] = Val.ToString(lueKapan.Text).ToString();
                                m_dtbRejectionLot.Rows[m_update_srno - 1]["manager_id"] = Val.ToInt64(lueManager.EditValue).ToString();
                                m_dtbRejectionLot.Rows[m_update_srno - 1]["manager_name"] = Val.ToString(lueManager.Text).ToString();
                                m_dtbRejectionLot.Rows[m_update_srno - 1]["purity_group"] = Val.ToString(txtPurityGroup.Text).ToString();
                                m_dtbRejectionLot.Rows[m_update_srno - 1]["section_id"] = Val.ToInt64(lueSection.EditValue).ToString();
                                m_dtbRejectionLot.Rows[m_update_srno - 1]["section_name"] = Val.ToString(lueSection.Text).ToString();
                                m_dtbRejectionLot.Rows[m_update_srno - 1]["pcs"] = Val.ToDecimal(txtPcs.Text).ToString();
                                m_dtbRejectionLot.Rows[m_update_srno - 1]["carat"] = Val.ToDecimal(txtCarat.Text).ToString();
                                m_dtbRejectionLot.Rows[m_update_srno - 1]["rate"] = Val.ToDecimal(txtRate.Text).ToString();
                                m_dtbRejectionLot.Rows[m_update_srno - 1]["amount"] = Val.ToDecimal(txtAmount.Text).ToString();
                                break;
                            }
                        }
                    }
                    btnAdd.Text = "&Add";
                }
                dgvRejToMakTransfer.MoveLast();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool GenerateProcessDetails()
        {
            bool blnReturn = true;
            try
            {
                if (m_dtbRejectionLot.Rows.Count > 0)
                    m_dtbRejectionLot.Rows.Clear();

                m_dtbRejectionLot = new DataTable();

                m_dtbRejectionLot.Columns.Add("transfer_id", typeof(Int64));
                m_dtbRejectionLot.Columns.Add("purity_id", typeof(Int64));
                m_dtbRejectionLot.Columns.Add("purity_name", typeof(string));
                m_dtbRejectionLot.Columns.Add("kapan_id", typeof(Int64));
                m_dtbRejectionLot.Columns.Add("kapan_no", typeof(string));
                m_dtbRejectionLot.Columns.Add("section_id", typeof(Int64));
                m_dtbRejectionLot.Columns.Add("section_name", typeof(string));
                m_dtbRejectionLot.Columns.Add("manager_id", typeof(Int64));
                m_dtbRejectionLot.Columns.Add("manager_name", typeof(string));
                m_dtbRejectionLot.Columns.Add("purity_group", typeof(string));
                m_dtbRejectionLot.Columns.Add("type", typeof(string));
                m_dtbRejectionLot.Columns.Add("pcs", typeof(decimal)).DefaultValue = 0;
                m_dtbRejectionLot.Columns.Add("carat", typeof(decimal)).DefaultValue = 0;
                m_dtbRejectionLot.Columns.Add("rate", typeof(decimal)).DefaultValue = 0;
                m_dtbRejectionLot.Columns.Add("amount", typeof(decimal)).DefaultValue = 0;
                m_dtbRejectionLot.Columns.Add("sr_no", typeof(int)).DefaultValue = 1;
                m_dtbRejectionLot.Columns.Add("lot_srno", typeof(Int64)).DefaultValue = 1;

                grdRejToMakTransfer.DataSource = m_dtbRejectionLot;
                grdRejToMakTransfer.Refresh();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                if (!GenerateProcessDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                lueKapan.EditValue = null;
                luePurity.EditValue = null;
                dtpConfirmDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpConfirmDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpConfirmDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpConfirmDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpConfirmDate.EditValue = DateTime.Now;
                lblMode.Text = "Add Mode";
                lblMode.Tag = 0;
                m_Srno = 0;
                m_update_srno = 0;
                lblOsCarat.Text = "0";
                lueManager.EditValue = null;
                lueType.EditValue = "REJECTION";
                lueSection.EditValue = null;
                txtPurityGroup.Text = "";
                txtPcs.Text = "0";
                txtCarat.Text = "0";
                txtRate.Text = "0";
                lblDiffCarat.Text = "0";
                btnSave.Enabled = true;
                btnAdd.Enabled = true;
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        #endregion

        #region Export Grid
        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void MNExportPDF_Click(object sender, EventArgs e)
        {
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

        private void txtPcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Export_List("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Export("pdf", "Export Report to PDF", "PDF (*.PDF)|*.PDF");
        }
    }
}
