﻿using BLL;
using BLL.FunctionClasses.Master;
using BLL.PropertyClasses.Master;
using System;
using System.Collections.Generic;
using System.Data;
using Global = DERP.Class.Global;

namespace DERP.Master
{
    public partial class FrmConfigMenuPermission : DevExpress.XtraEditors.XtraForm
    {
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.FormPer ObjPer = new BLL.FormPer();
        BLL.Validation Val = new BLL.Validation();

        ConfigMenuPermissionMaster objMenuPermission = new ConfigMenuPermissionMaster();

        #region Constructor

        public FrmConfigMenuPermission()
        {
            InitializeComponent();
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
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objMenuPermission);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }


        #endregion

        #region Events

        private void BtnSave_Click(object sender, EventArgs e)
        {
            ObjPer.SetFormPer();
            if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                return;
            }
            BtnSave.Enabled = false;

            if (SaveDetails())
            {
                GetData();
                BtnClear_Click(sender, e);
            }

            BtnSave.Enabled = true;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            lblMode.Tag = 0;
            lblMode.Text = "Add Mode";
            lueStartMenu.EditValue = null;
            lueRoleID.EditValue = null;
            ChkISPermission.Checked = false;
            lueStartMenu.Focus();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrmMenuPermission_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPRole(lueRoleID);
                Global.LOOKUPMenu(lueStartMenu);
                GetData();
                BtnClear_Click(BtnClear, null);
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        #region Grid Events
        private void GrdMenuPermission_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                DataRow Drow = GrdMenuPermission.GetDataRow(e.RowHandle);

                lblMode.Text = "Edit Mode";
                lblMode.Tag = Val.ToInt64(Drow["menu_detail_id"]);
                lueStartMenu.EditValue = Val.ToInt32(Drow["menu_id"]);
                lueRoleID.EditValue = Val.ToInt32(Drow["role_id"]);
                ChkISPermission.Checked = Val.ToBoolean(Drow["is_permisson"]);
                lueStartMenu.Focus();
            }
        }

        #endregion

        #endregion

        #region Function
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (lueRoleID.Text == "")
                {
                    lstError.Add(new ListError(13, "Role"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueRoleID.Focus();
                    }
                }
                if (lueStartMenu.Text == "")
                {
                    lstError.Add(new ListError(13, "Menu"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueStartMenu.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));

        }
        private bool SaveDetails()
        {
            bool blnReturn = true;
            ConfigMenuPermission_MasterProperty MenuPermissionMasterProperty = new ConfigMenuPermission_MasterProperty();
            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                MenuPermissionMasterProperty.menu_detail_id = Val.ToInt64(lblMode.Tag);
                MenuPermissionMasterProperty.menu_id = Val.ToInt(lueStartMenu.EditValue);
                MenuPermissionMasterProperty.role_id = Val.ToInt(lueRoleID.EditValue);
                MenuPermissionMasterProperty.is_permisson = Val.ToBoolean(ChkISPermission.Checked);

                int IntRes = objMenuPermission.Save(MenuPermissionMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Menu Permission Details");
                    lueRoleID.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Menu Permission Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Menu Permission Details Data Update Successfully");
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
                MenuPermissionMasterProperty = null;
            }

            return blnReturn;
        }
        private void GetData()
        {
            DataTable DTab = objMenuPermission.Start_Menu_Permission_GetData();

            dgvMenuPermission.DataSource = DTab;
            GrdMenuPermission.BestFitColumns();
        }
        #endregion
    }
}
