using Hospital_Web.Models.Admin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital_Web.Controllers
{
    public class TransationController : Controller
    {
        // GET: Transation
        public ActionResult OpenTransationTest()
        {
            TestTransationLine objTestTransationLine = new TestTransationLine();
            //objTestTransationLine.TransactionList = objTestTransationLine.ReturnJsonString();
            return View("~/Views/Transation/TestTransation.cshtml", objTestTransationLine);
        }
        public ActionResult SaveTransationTest(TestTransationLine objTestTransationLine)
        {
            if (objTestTransationLine.TestTransationLine_SaveRecord()>0)
            {
                objTestTransationLine.TransationDetail.TransationID = objTestTransationLine.TransationID;
                objTestTransationLine.TransationDetail.TestTransationDetails_SaveRecord();
                objTestTransationLine.TransationDetail= null;
                objTestTransationLine.TransationDetail = new TestTransationDetails();      
            }
            

            return View("~/Views/Transation/TestTransation.cshtml", objTestTransationLine);
        }
        public ActionResult GetTransationTestDetail(int TransationID)
        {
            TestTransationDetails TestTransationDetails = new TestTransationDetails();
            try
            {
                TestTransationDetails.TransationID= TransationID;
                
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(TestTransationDetails.TestTransationDetails_Get()));
        }
        public ActionResult GetTestData(int TestID)
        {
            TestMaster ObjTestMaster = new TestMaster();
            ObjTestMaster.TestID = TestID;
            return Content(JsonConvert.SerializeObject(ObjTestMaster.TestMaster_SelecteRecord()));
        }
        public ActionResult TestTransationDataIndex()
        {
            TestTransationLine ObjTestTransationLine = new TestTransationLine();
            return View("~/Views/Transation/TestTransationData.cshtml", ObjTestTransationLine);
        }
        [HttpPost]
        public ActionResult GetTestTransationData(TestTransationLine ObjTestTransationLine)
        {           
            return View("~/Views/Transation/TestTransationData.cshtml", ObjTestTransationLine);
        }
        
        public ActionResult GetTestTransationDataFromJS(string Date,string MobileNumber)
        {
            return Content(JsonConvert.SerializeObject(new TestTransationLine().TestTransationLine_GetRecord(Date, MobileNumber)));
            
        }
    }
}