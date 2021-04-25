using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Hospital_Web.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult HospitalLists(string type = "view")
        {
            try
            {
                if (type == "view")
                {
                    return View("~/Views/Admin/HospitalMaster.cshtml");
                }
                else
                    return HospitalList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private ContentResult HospitalList()
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Action", "selectall"));
                ds = DBManager.ExecuteDataSetWithParamiter("ManageHospitalMaster", CommandType.StoredProcedure, SqlParameters);
                ds.Tables[0].TableName = "HospitalLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(ds));
        }

        public ActionResult ManageHospitalMaster()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Action", Convert.ToInt32(Request.Form["HospitalId"]) > 0 ? "update" : "insert"));
                SqlParameters.Add(new SqlParameter("@HospitalID", Convert.ToInt32(Request.Form["HospitalId"])));
                SqlParameters.Add(new SqlParameter("@Type", Request.Form["Type"]));
                SqlParameters.Add(new SqlParameter("@HospitalName", Request.Form["HospitalName"]));
                SqlParameters.Add(new SqlParameter("@Address", Request.Form["HospitalAddress"]));
                SqlParameters.Add(new SqlParameter("@ContactNumber", Request.Form["ContactNumber"]));
                SqlParameters.Add(new SqlParameter("@EmailId", Request.Form["EmailId"]));
                SqlParameters.Add(new SqlParameter("@Website", Request.Form["WebsiteName"]));
                SqlParameters.Add(new SqlParameter("@Description", Request.Form["Description"]));
                dt = DBManager.ExecuteDataTableWithParamiter("ManageHospitalMaster", CommandType.StoredProcedure, SqlParameters);
                ViewBag.Msg = Convert.ToString(dt.Rows[0]["msg"]);
            }
            catch (Exception)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/HospitalMaster.cshtml", dt);
        }

        public ActionResult DoctorLists(string type = "view")
        {
            DataSet ds = new DataSet();
            try
            {
                if (type == "view")
                {
                    List<SqlParameter> SqlParameters = new List<SqlParameter>();
                    SqlParameters.Add(new SqlParameter("@Action", "selectall"));
                    ds = DBManager.ExecuteDataSetWithParamiter("ManageDoctorMaster", CommandType.StoredProcedure, SqlParameters);
                    ds.Tables[1].TableName = "DepartmentLists";
                    ds.Tables[2].TableName = "HospitalLists";
                    return View("~/Views/Admin/DoctorMaster.cshtml", ds);
                }
                else
                    return DoctorList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private ContentResult DoctorList()
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Action", "selectall"));
                ds = DBManager.ExecuteDataSetWithParamiter("ManageDoctorMaster", CommandType.StoredProcedure, SqlParameters);
                ds.Tables[0].TableName = "DoctorLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(ds));
        }

        public ActionResult ManageDoctorMaster()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Action", Convert.ToInt32(Request.Form["DoctorId"]) > 0 ? "update" : "insert"));
                SqlParameters.Add(new SqlParameter("@DoctorId", Convert.ToInt32(Request.Form["DoctorId"])));
                SqlParameters.Add(new SqlParameter("@HospitalId", Request.Form["HospitalList"]));
                SqlParameters.Add(new SqlParameter("@DepartmentId", Request.Form["DepartmentList"]));
                SqlParameters.Add(new SqlParameter("@DoctorName", Request.Form["DoctorName"]));
                SqlParameters.Add(new SqlParameter("@ContactNumber", Request.Form["ContactNumber"]));
                SqlParameters.Add(new SqlParameter("@Address", Request.Form["HospitalAddress"]));
                SqlParameters.Add(new SqlParameter("@EmailId", Request.Form["EmailId"]));
                SqlParameters.Add(new SqlParameter("@Website", Request.Form["WebsiteName"]));
                SqlParameters.Add(new SqlParameter("@Commission", Request.Form["Commission"]));
                SqlParameters.Add(new SqlParameter("@Description", Request.Form["Description"]));
                dt = DBManager.ExecuteDataTableWithParamiter("ManageDoctorMaster", CommandType.StoredProcedure, SqlParameters);
                ViewBag.Msg = Convert.ToString(dt.Rows[0]["msg"]);
            }
            catch (Exception)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/DoctorMaster.cshtml", dt);
        }
    }
}