﻿using BLL;
using BLL.FunctionClasses.Master.Store;
using BLL.PropertyClasses.Master.Store;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Master.Store
{
    public partial class FrmMfgItemTypeMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.Validation Val;
        BLL.FormPer ObjPer;

        MfgItemTypeMaster objItemType;
        #endregion

        #region Constructor
        public FrmMfgItemTypeMaster()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            Val = new BLL.Validation();
            ObjPer = new BLL.FormPer();

            objItemType = new MfgItemTypeMaster();
        }
        public void ShowForm()
        {
            ObjPer.FormName = this.Name.ToUpper();
            if (ObjPer.CheckPermission() == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionViwMsg);
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
            objBOFormEvents.ObjToDispose.Add(objItemType);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmMfgGroupMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            ObjPer.FormName = this.Name.ToUpper();
            ObjPer.SetFormPer();
            if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                return;
            }
            btnSave.Enabled = false;

            if (SaveDetails())
            {
                GetData();
                btnClear_Click(sender, e);
            }

            btnSave.Enabled = true;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblMode.Tag = 0;
                lblMode.Text = "Add Mode";
                txtItemType.Text = string.Empty;
                txtSequenceNo.Text = string.Empty;
                txtRemark.Text = string.Empty;
                chkActive.Checked = true;
                GetData();
                txtItemType.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region GridEvents  
        private void dgvItemTypeMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvItemTypeMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["item_type_id"]);
                        txtItemType.Text = Val.ToString(Drow["item_type_name"]);
                        txtSequenceNo.Text = Val.ToString(Drow["sequence_no"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtItemType.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        #endregion

        #endregion

        #region Functions
        private bool SaveDetails()
        {
            bool blnReturn = true;
            MfgItemType_MasterProperty ItemTypeMasterProperty = new MfgItemType_MasterProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                ItemTypeMasterProperty.item_type_id = Val.ToInt32(lblMode.Tag);
                ItemTypeMasterProperty.item_type_name = txtItemType.Text.ToUpper();
                ItemTypeMasterProperty.sequence_no = Val.ToInt(txtSequenceNo.Text);
                ItemTypeMasterProperty.remarks = txtRemark.Text.ToUpper();
                ItemTypeMasterProperty.active = Val.ToBoolean(chkActive.Checked);

                int IntRes = objItemType.Save(ItemTypeMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Item Type Details");
                    txtItemType.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Item Type Details Data Successfully");
                    }
                    else
                    {
                        Global.Confirm("Item Type Details  Update Successfully");
                    }
                }

            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                blnReturn = false;
            }
            finally
            {
                ItemTypeMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtItemType.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Item Type"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtItemType.Focus();
                    }
                }


                if (!objItemType.ISExists(txtItemType.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Item Type"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtItemType.Focus();
                    }

                }
                if (txtSequenceNo.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Sequence No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtSequenceNo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));

        }
        public void GetData()
        {
            try
            {
                DataTable DTab = objItemType.GetData();
                grdItemTypeMaster.DataSource = DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
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
                            dgvItemTypeMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvItemTypeMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvItemTypeMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvItemTypeMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvItemTypeMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvItemTypeMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvItemTypeMaster.ExportToCsv(Filepath);
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
    }
}
