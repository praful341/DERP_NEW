﻿using BLL.PropertyClasses.Master;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master
{
    public class EmployeeMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        public int Save(Employee_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int64);
            Request.AddParams("@employee_code", pClsProperty.employee_code, DbType.String);
            Request.AddParams("@first_name", pClsProperty.first_name, DbType.String);
            Request.AddParams("@middle_name", pClsProperty.middle_name, DbType.String);
            Request.AddParams("@last_name", pClsProperty.last_name, DbType.String);
            Request.AddParams("@short_name", pClsProperty.short_name, DbType.String);
            Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int64);
            Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int64);
            Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int64);
            Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
            Request.AddParams("@designation_id", pClsProperty.designation_id, DbType.Int32);
            Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@email", pClsProperty.email, DbType.String);
            Request.AddParams("@email_password", pClsProperty.email_password, DbType.String);
            Request.AddParams("@sale_premium", pClsProperty.sale_premium, DbType.String);
            Request.AddParams("@sale_discount", pClsProperty.sale_discount, DbType.String);
            Request.AddParams("@emp_address", pClsProperty.emp_address, DbType.String);
            Request.AddParams("@joining_date", pClsProperty.joining_date, DbType.Date);
            Request.AddParams("@reference_by", pClsProperty.reference_by, DbType.String);
            Request.AddParams("@reference_mobile", pClsProperty.reference_mobile, DbType.String);
            Request.AddParams("@employee_mobile", pClsProperty.employee_mobile, DbType.String);
            Request.AddParams("@aadhar_no", pClsProperty.aadhar_no, DbType.String);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            Request.AddParams("@leave_date", pClsProperty.leave_date, DbType.Date);
            Request.AddParams("@dob", pClsProperty.dob, DbType.Date);
            Request.AddParams("@age", pClsProperty.age, DbType.Int32);
            Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MST_Employee_Save;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public DataTable GetData(int active = 0, string designation = "")
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@active", active, DbType.Int32);
            Request.AddParams("@designation", designation, DbType.String);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.CommandText = BLL.TPV.SProc.MST_Employee_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetData_ActiveManager(int active = 0, string designation = "")
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@active", active, DbType.Int32);
            Request.AddParams("@designation", designation, DbType.String);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.CommandText = BLL.TPV.SProc.MST_Active_Manager_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetManager(int active = 0, string designation = "")
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.AddParams("@active", active, DbType.Int32);
            Request.AddParams("@designation", designation, DbType.String);
            Request.CommandText = BLL.TPV.SProc.MST_Manager_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetEmployee(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@active", active, DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.CommandText = BLL.TPV.SProc.MST_Employee_Without_Manager_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetEmployee_History(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@active", active, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MST_Employee_History_Emp_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetCompanyWiseEmpData(int company, int branch, int location, int department, int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@active", active, DbType.Int32);
            Request.AddParams("@company_id", company, DbType.Int32);
            Request.AddParams("@branch_id", branch, DbType.Int32);
            Request.AddParams("@location_id", location, DbType.Int32);
            Request.AddParams("@department_id", department, DbType.Int32);
            Request.CommandText = BLL.TPV.SProc.MST_Employee_GetCompanyWiseData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public string ISExistsCode(string EmpCode, Int64 EmpId)
        {
            Validation Val = new Validation();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MST_Employee", "employee_code", "AND employee_code = '" + EmpCode + "' AND NOT employee_id =" + EmpId));
        }
    }
}
