using Hospital_Web.Models.Admin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Web.Mvc;

namespace Hospital_Web.Controllers
{
    public class AdminController : Controller
    {
        public DataSet GetAllCommonData()
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Action", "getall"));
                ds = DBManager.ExecuteDataSetWithParamiter("GetCommonProc", CommandType.StoredProcedure, SqlParameters);
                ds.Tables[0].TableName = "DepartmentLists";
                ds.Tables[1].TableName = "HospitalLists";
            }
            catch (Exception)
            {
                throw;
            }
            return (ds);
        }
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

        public ActionResult DepartmentLists(string type = "view")
        {
            DataSet ds = new DataSet();
            try
            {
                if (type == "view")
                {
                    ds = GetAllCommonData();
                    return View("~/Views/Admin/DepartmentMaster.cshtml", ds);
                }
                else
                    return DepartmentList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private ContentResult DepartmentList()
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Action", "selectall"));
                ds = DBManager.ExecuteDataSetWithParamiter("ManageDepartmentMaster", CommandType.StoredProcedure, SqlParameters);
                ds.Tables[0].TableName = "DepartmentLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(ds));
        }

        public ActionResult ManageDepartmentMaster()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Action", Convert.ToInt32(Request.Form["DepartmentId"]) > 0 ? "update" : "insert"));
                SqlParameters.Add(new SqlParameter("@DepartmentId", Convert.ToInt32(Request.Form["DepartmentId"])));
                SqlParameters.Add(new SqlParameter("@HospitalId", Request.Form["HospitalList"]));
                SqlParameters.Add(new SqlParameter("@DepartmentName", Request.Form["DepartmentName"]));
                dt = DBManager.ExecuteDataTableWithParamiter("ManageDepartmentMaster", CommandType.StoredProcedure, SqlParameters);
                ViewBag.Msg = Convert.ToString(dt.Rows[0]["msg"]);
                ds = GetAllCommonData();
            }
            catch (Exception)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/DepartmentMaster.cshtml", ds);
        }

        public ActionResult AppointmentLists(string type = "view", string ToDate = "")
        {
            DataSet ds = new DataSet();
            try
            {
                if (type == "view")
                {
                    return View("~/Views/Admin/AppointmentLists.cshtml");
                }
                else
                    return AppointmentList(ToDate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private ContentResult AppointmentList(string ToDate)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Action", "todayspatientappointment"));
                SqlParameters.Add(new SqlParameter("@ToDate", ToDate = (ToDate == "" ? DateTime.Now.ToString("yyyy/MM/dd") : ToDate)));
                ds = DBManager.ExecuteDataSetWithParamiter("ManagePatientAppointment", CommandType.StoredProcedure, SqlParameters);
                ds.Tables[0].TableName = "AppointmentLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(ds));
        }

        public ActionResult OPDAppointmentLists(string type = "view", string ToDate = "")
        {
            DataSet ds = new DataSet();
            try
            {
                if (type == "view")
                {
                    return View("~/Views/Admin/CreateOPDAppointment.cshtml");
                }
                else
                    return OPDAppointmentList(ToDate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private ContentResult OPDAppointmentList(string ToDate)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Action", "todaysopdappointment"));
                SqlParameters.Add(new SqlParameter("@ToDate", ToDate = (ToDate == "" ? DateTime.Now.ToString("yyyy/MM/dd") : ToDate)));
                ds = DBManager.ExecuteDataSetWithParamiter("ManagePatientAppointment", CommandType.StoredProcedure, SqlParameters);
                ds.Tables[0].TableName = "OPDAppointmentLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(ds));
        }

        public ActionResult ManageOPDAppointment()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Action", Request.Form["OPDId"] == "0" ? "insertopdappointment" : "updateopdappointment"));
                SqlParameters.Add(new SqlParameter("@OPDId", Request.Form["OPDId"]));
                SqlParameters.Add(new SqlParameter("@AppointmentNumber", Request.Form["AppointmentNumber"] == "0" ? "0" : Request.Form["contactnumber"]));
                SqlParameters.Add(new SqlParameter("@OPDNumber", Request.Form["contactnumber"]));
                SqlParameters.Add(new SqlParameter("@AppointmentId", Request.Form["AppointmentId"] == "0" ? "0" : Request.Form["AppointmentId"]));
                SqlParameters.Add(new SqlParameter("@Name", Request.Form["name"]));
                SqlParameters.Add(new SqlParameter("@Age", Request.Form["age"]));
                SqlParameters.Add(new SqlParameter("@Address", Request.Form["address"]));
                SqlParameters.Add(new SqlParameter("@ContactNumber", Request.Form["contactnumber"]));
                SqlParameters.Add(new SqlParameter("@EmailId", Request.Form["email"]));
                SqlParameters.Add(new SqlParameter("@OPDCharge", Request.Form["opdcharge"]));
                SqlParameters.Add(new SqlParameter("@AppointmentDate", Request.Form["appointmentdate"]));
                SqlParameters.Add(new SqlParameter("@AppointmentTime", Request.Form["appointmenttime"]));
                SqlParameters.Add(new SqlParameter("@Description", Request.Form["description"]));
                dt = DBManager.ExecuteDataTableWithParamiter("ManagePatientAppointment", CommandType.StoredProcedure, SqlParameters);
                ViewBag.Msg = Convert.ToString(dt.Rows[0]["msg"]);
            }
            catch (Exception)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/CreateOPDAppointment.cshtml");
        }

        public ActionResult TestMaster(int TestId=0)
        {
            TestMaster ObjTestMaster = new TestMaster();
            if (TestId == 0)
            {
                Session.Add("ObjTestMaster", "");
                ViewBag.Msg = "";
                ViewBag.TestId = 0;
               
            }
            else
            {                
                ObjTestMaster.TestID = TestId;
                ObjTestMaster.SelecteRecordBYID();             
            }
            ViewBag.ObjTestMaster = ObjTestMaster;
            string LinkURL = Request.Url.GetLeftPart(UriPartial.Authority) + Url.Content("~");
            ViewBag.ImageUploadURL = LinkURL + "CkediterImageUpload.ashx";
            return View("~/Views/Admin/TestMaster.cshtml");
        }
        public ActionResult ManageTestMaster()
        {
            TestMaster ObjTestMaster = new TestMaster();
           
                ObjTestMaster.TestID = Convert.ToInt32(Request.Form["TestId"]);
                ObjTestMaster.TestName = Convert.ToString(Request.Form["TestName"]);
                ObjTestMaster.Charge = Convert.ToDecimal(Request.Form["Charge"]);
                ObjTestMaster.IsDiscriptive = Convert.ToInt32(Request.Form["IsDescription"]);
                ObjTestMaster.Description = Convert.ToString(Request.Form["Description"]);
                if (ObjTestMaster.SaveRecord())
                {
                    ObjTestMaster.Message = "Record Saved";
                    ObjTestMaster.Status = "done";
                    var Objlist =  GetTestDetailed(ObjTestMaster.TestID);
                    ObjTestMaster.ObjTestMasterDetail = Objlist.tmdList;
                    Session.Add("ObjTestMaster", ObjTestMaster);
                }
                else
                {
                    ObjTestMaster.Status = "error";
                    ObjTestMaster.Message = "Record Not Saved";
                }
                ViewBag.ObjTestMaster = ObjTestMaster;
            return Content(JsonConvert.SerializeObject(ObjTestMaster));
        }

        public ActionResult ManageTestMasterDetail()
        {
            try
            {
                TestMaster ObjTestMaster = new TestMaster();
                if (Session["ObjTestMaster"] != null)
                {

                    ObjTestMaster = (TestMaster)Session["ObjTestMaster"];
                    ViewBag.TestId = ObjTestMaster.TestID;
                }
                else
                {
                    ViewBag.Msg = "Error in process ......";
                    return View("~/Views/Admin/TestMaster.cshtml");
                }
                TestMasterDetail ObjTestMasterDetail = new TestMasterDetail();
                ObjTestMasterDetail.TestMasterID = ObjTestMaster.TestID;
                ObjTestMasterDetail.TestDetailID = Convert.ToInt32(Request.Form["TestDetailID"]);
                ObjTestMasterDetail.HeadName = Convert.ToString(Request.Form["HeadName"]);
                ObjTestMasterDetail.FieldName = Convert.ToString(Request.Form["FieldName"]);
                ObjTestMasterDetail.FieldDefaultValue = Convert.ToString(Request.Form["FieldDefaultValue"]);
                ObjTestMasterDetail.Unit = Convert.ToString(Request.Form["Unit"]);
                ObjTestMasterDetail.TestDetailID = ObjTestMasterDetail.TestMasterDetail_SaveRecord();
                if (ObjTestMasterDetail.TestDetailID > 0)
                {                   
                    Session.Add("ObjTestMaster", ObjTestMaster);
                    return Content(JsonConvert.SerializeObject(GetTestDetailed(ObjTestMaster.TestID)));
                }
                else
                {
                    dynamic tmdDetailList = new ExpandoObject();
                    tmdDetailList.ObjTestMaster = ObjTestMaster;
                    tmdDetailList.TestMasterDetail = ObjTestMasterDetail;
                    tmdDetailList.Status = "error";
                    tmdDetailList.Message = "Record Not Saved";
                    tmdDetailList.TestID = 0;
                    return Content(JsonConvert.SerializeObject(tmdDetailList));
                }
            }
            catch (Exception ex)
            {
                return Content(JsonConvert.SerializeObject("ERROR"));
            }
        }
        public dynamic GetTestDetailed(int TestID)
        {
            DataTable dt = new TestMasterDetail().TestMasterDetail_SelecteRecord(TestID, 0);
            List<TestMasterDetail> tmdList = new List<TestMasterDetail>();
            foreach (DataRow dr in dt.Rows)
            {
                TestMasterDetail tmd = new TestMasterDetail();
                tmd.TestDetailID = Convert.ToInt32(dr["TestDetailID"]);
                tmd.TestMasterID = Convert.ToInt32(dr["TestMasterID"]);
                tmd.FieldName = Convert.ToString(dr["FieldName"]);
                tmd.FieldDefaultValue = Convert.ToString(dr["FieldDefaultValue"]);
                tmd.StatusID = Convert.ToInt32(dr["StatusID"]);
                tmd.HeadName = Convert.ToString(dr["HeadName"]);
                tmd.Unit = Convert.ToString(dr["Unit"]);
                tmdList.Add(tmd);
            }
            dynamic tmdDetailList = new ExpandoObject();
            tmdDetailList.tmdList = tmdList.Count > 0 ? tmdList : null;
            tmdDetailList.Status = "done";
            tmdDetailList.Message = "Record Added";
            tmdDetailList.TestID = TestID;
            return tmdDetailList;
        }

        public ActionResult TestMasterData(string type = "view")
        {
            if (type == "view")
            {
                return View("~/Views/Admin/TestMasterData.cshtml");
            }
            else
            {
                return TestMasterList();
            }
        }
        private ContentResult TestMasterList()
        {
            DataSet ds = new DataSet();
            try
            {
                TestMaster ObjTestMaster = new TestMaster();
                ds.Tables.Add(ObjTestMaster.TestMaster_SelecteRecord());
                ds.Tables[0].TableName = "TestMasterLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(ds));
        }
    }
}