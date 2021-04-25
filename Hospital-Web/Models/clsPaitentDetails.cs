using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Hospital.BL.MasterDetails
{
    class clsPaitentDetails
    {
        public int PaitentID;
        public string PaitentName;
        public int MobileNo;
        public string Gender;
        public int Age;
        public string ReferDoctor;
        public string Test;
        public decimal TestCharge;
        public decimal TotalAmount;
        public decimal ReciviedAmount;
        public string Adjestment;
        public clsPaitentDetails()
        {
            PaitentID = 0;
            PaitentName = ""; ;
            MobileNo=0;
            Gender = "";
            Age = 0;
            ReferDoctor = ""; ;
            Test = "";
            TestCharge = 0;
            TotalAmount = 0;
            ReciviedAmount = 0;
            Adjestment = "";
        }
        public string PaitentDetails_SaveRecord()
        {
            List<IDbDataParameter> oPara = new List<IDbDataParameter>();
            oPara.Add(new SqlParameter("@PaitentID", this.PaitentID));
            oPara.Add(new SqlParameter("@PaitentName", this.PaitentName));
            oPara.Add(new SqlParameter("@MobileNo", this.MobileNo));
            oPara.Add(new SqlParameter("@Gender", this.Gender));
            oPara.Add(new SqlParameter("@Age", this.Age));
            oPara.Add(new SqlParameter("@ReferDoctor", this.ReferDoctor));
            oPara.Add(new SqlParameter("@Test", this.Test));
            oPara.Add(new SqlParameter("@TestCharge", this.TestCharge));
            oPara.Add(new SqlParameter("@TotalAmount", this.TotalAmount));
            oPara.Add(new SqlParameter("@ReciviedAmount", this.ReciviedAmount));
            oPara.Add(new SqlParameter("@Adjestment", this.Adjestment));

            object obj = SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "PaitentMaster_SaveRecord", oPara);
            return obj.ToString();
        }
        public string PaitentDetails_DeleteRecord()
        {
            List<IDbDataParameter> oPara = new List<IDbDataParameter>();
            oPara.Add(new SqlParameter("@PaitentID", this.PaitentID));
            object obj = SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "PaitentMaster_DeleteRecord", oPara);
            return obj.ToString();
        }
        public DataTable PaitentDetails_SelecteRecord()
        {
            List<IDbDataParameter> oPara = new List<IDbDataParameter>();
            oPara.Add(new SqlParameter("@PaitentID", this.PaitentID));
            DataTable dt = SQLHelper.GetDataTable(CommandType.StoredProcedure, "PaitentMaster_SelectRecord", oPara);
            return dt;
        }
        public DataTable PaitentDetails_SelecteRecord_ReferDoctor()
        {
            string sql = "Select DoctorID,DoctorName From DoctorMaster";
             DataTable dt = SQLHelper.GetDataTable(CommandType.Text, sql);
            return dt;
        }
        public DataTable PaitentDetails_SearchRecord(string Search)
        {
            List<IDbDataParameter> oPara = new List<IDbDataParameter>();
            oPara.Add(new SqlParameter("@Search", Search));
            string sql = "Select p.PaitentID,p.PaitentName From PaitentMaster as p where p.PaitentName like '%'+@Search+'%' or p.ReferDoctor like '%'+@Search+'%'";
            DataTable dt = SQLHelper.GetDataTable(CommandType.Text, sql, oPara);
            return dt;
        }
    }
}
