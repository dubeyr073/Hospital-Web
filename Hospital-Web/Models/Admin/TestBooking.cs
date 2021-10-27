using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital_Web.Models.Admin
{
	
    public class TestTransationLine: Controller
	{
		//[Key]
		public int TransationID { get; set; }
		public string Transation_No { get; set; }
		public DateTime Transation_Data { get; set; }
		public int Refrence_Type { get; set; }
		public int HospitalID { get; set; }
		public SelectList HospitalData { get; set; }
		public int DoctorID { get; set; }
		public SelectList DoctorData { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public string Sex { get; set; }
		public string MobileNumber { get; set; }
		public string Remark { get; set; }
		public TestTransationDetails TransationDetail { get; set; }
		
		
		public TestTransationLine()
        {
			DoctorData = new SelectList(DDLValueFromDB.GETDATAFROMDB("DoctorId", "DoctorName", "DoctorMaster",  "And Status=1"),"Id", "Value");
			HospitalData = new SelectList(DDLValueFromDB.GETDATAFROMDB("HospitalID", "HospitalName", "HospitalMaster",  "And Status=1"), "Id", "Value");
			TransationDetail = new TestTransationDetails();
			Transation_No = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
			Remark = "";
		}
		public int TestTransationLine_SaveRecord()
		{
			List<SqlParameter> oPara = new List<SqlParameter>();
			oPara.Add(new SqlParameter("@TransationID", this.TransationID));
			oPara.Add(new SqlParameter("@Transation_No", this.Transation_No));
			oPara.Add(new SqlParameter("@Refrence_Type", this.Refrence_Type));
			oPara.Add(new SqlParameter("@HospitalID", this.HospitalID));
			oPara.Add(new SqlParameter("@DoctorID", this.DoctorID));
			oPara.Add(new SqlParameter("@Name", this.Name));
			oPara.Add(new SqlParameter("@Age", this.Age));
			oPara.Add(new SqlParameter("@Sex", this.Sex));
			oPara.Add(new SqlParameter("@MobileNumber", this.MobileNumber));
			oPara.Add(new SqlParameter("@Remark", this.Remark));
			object obj = DBManager.ExecuteScalar("TestTransationLine_Save", CommandType.StoredProcedure, oPara);
			TransationID = Convert.ToInt32(obj);
			return Convert.ToInt32(obj);
		}

		public DataTable TestTransationLine_GetRecord(string ToDate,string MobileNumber)
        {
			List<SqlParameter> SqlParameters = new List<SqlParameter>();
			if(ToDate != "")
				SqlParameters.Add(new SqlParameter("@ToDate", (ToDate == "" ? null : ToDate)));
			if (MobileNumber != "")
				SqlParameters.Add(new SqlParameter("@MobileNumber", MobileNumber));
			DataTable dt = DBManager.ExecuteDataTableWithParamiter("TestTransation_SelectRecord", CommandType.StoredProcedure, SqlParameters);
			return dt;
		}


	}
    public class TestTransationDetails
    {
		public int TransationDetailsID { get; set; }
		public int TransationID { get; set; }
		public int TestID { get; set; }
		public SelectList TestData { get; set; }
		public decimal Amount { get; set; }
		public decimal Remark { get; set; }
		public TestTransationDetails()
        {
			
			TestData = new SelectList(DDLValueFromDB.GETDATAFROMDB("TestID", "TestName", "TestMaster", "And Status=1"), "Id", "Value");
			
		}
		public int TestTransationDetails_SaveRecord()
		{
			List<SqlParameter> oPara = new List<SqlParameter>();
			oPara.Add(new SqlParameter("@TransationID", this.TransationID));
			oPara.Add(new SqlParameter("@TransationDetailsID", this.TransationDetailsID));
			oPara.Add(new SqlParameter("@TestID", this.TestID));
			oPara.Add(new SqlParameter("@Amount", this.Amount));
			oPara.Add(new SqlParameter("@Remark", this.Remark));
			object obj = DBManager.ExecuteScalar("TestTransationDetails_Save", CommandType.StoredProcedure, oPara);
			return Convert.ToInt32(obj);
		}
		public DataTable TestTransationDetails_Get()
        {
			DataSet ds = new DataSet();
			List<SqlParameter> SqlParameters = new List<SqlParameter>();
			SqlParameters.Add(new SqlParameter("@TransationID", TransationID));
			ds = DBManager.ExecuteDataSetWithParamiter("TestTransationDetails_GetData", CommandType.StoredProcedure, SqlParameters);
			ds.Tables[0].TableName = "DetailLists";
			return ds.Tables[0];
		}
		//string Remark;
	}

	
}