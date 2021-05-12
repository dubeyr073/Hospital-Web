using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Hospital_Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public JsonResult Authenticate(string loginid = "", string password = "")
        {
            string result = "Fail";
            DataSet dt = new DataSet();
            DataTable loginAuth = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@LoginId", loginid));
                SqlParameters.Add(new SqlParameter("@Password", password));
                loginAuth = DBManager.ExecuteDataTableWithParamiter("Proc_User_Authentication", CommandType.StoredProcedure, SqlParameters);

                if (loginAuth.Rows.Count > 0)
                {
                    Session["UserName"] = Convert.ToString(loginAuth.Rows[0].ItemArray[3]);
                    Session["UserId"] = Convert.ToString(loginAuth.Rows[0].ItemArray[0]);
                    result = "Success";
                }
                else
                {
                    ViewBag.Msg = "username and password is wrong..!";
                }
            }
            catch (Exception)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login()
        {
            if (Session["UserName"] != null)
            {
                return View("~/Views/Admin/DashBoard.cshtml");
            }
            else
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }
        public ActionResult DashBoard()
        {
            DataSet dt = new DataSet();
            try
            {

            }
            catch (Exception)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/DashBoard.cshtml");
        }
        public ActionResult Index()
        {
            return View("~/Views/Home/Index.cshtml");
        }
        public ActionResult About()
        {
            return View("~/Views/Home/about.cshtml");
        }
        public ActionResult Contact()
        {
            return View("~/Views/Home/contact.cshtml");
        }
        [HttpPost]
        public JsonResult PatientAppointment()
        {
            string result = "Fail";
            DataTable status = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Action", "insertappointment"));
                SqlParameters.Add(new SqlParameter("@AppointmentNumber", Request.Form["contactnumber"])); ;
                SqlParameters.Add(new SqlParameter("@Name", Request.Form["name"]));
                SqlParameters.Add(new SqlParameter("@Age", Request.Form["age"]));
                SqlParameters.Add(new SqlParameter("@Address", Request.Form["address"]));
                SqlParameters.Add(new SqlParameter("@ContactNumber", Request.Form["contactnumber"]));
                SqlParameters.Add(new SqlParameter("@EmailId", Request.Form["email"]));
                SqlParameters.Add(new SqlParameter("@AppointmentDate", Request.Form["appointmentdate"]));
                SqlParameters.Add(new SqlParameter("@AppointmentTime", Request.Form["appointmenttime"]));
                SqlParameters.Add(new SqlParameter("@Description", Request.Form["description"]));
                status = DBManager.ExecuteDataTableWithParamiter("ManagePatientAppointment", CommandType.StoredProcedure, SqlParameters);

                if (status.Rows.Count > 0)
                {
                    result = "Success";
                }
                else
                {
                    ViewBag.Msg = "username and password is wrong..!";
                }
            }
            catch (Exception)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}