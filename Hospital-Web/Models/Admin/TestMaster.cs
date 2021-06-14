using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Hospital_Web.Models.Admin
{
    
   public class TestMaster
    {
        public int TestID;
        public string TestName;
        public decimal Charge;
        public string Description;
        public int IsDiscriptive;
        public string Message;
        public string Status;
        public List<TestMasterDetail> ObjTestMasterDetail;
        public TestMaster()
        {
            TestID = 0;
            TestName = "";
            Charge = 0;           
            Description = "";           
            IsDiscriptive = 0;
            Message = "";
            Status = "";
            ObjTestMasterDetail = new List<TestMasterDetail>();
        }
        public bool SaveRecord()
        {
            try
            {
                TestID = TestMaster_SaveRecord();
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }
        private int TestMaster_SaveRecord()
        {
            List<SqlParameter> oPara = new List<SqlParameter>();
            oPara.Add(new SqlParameter("@TestID", this.TestID));
            oPara.Add(new SqlParameter("@TestName", this.TestName));
            oPara.Add(new SqlParameter("@Charge", this.Charge));
            oPara.Add(new SqlParameter("@Description", this.Description));          
            oPara.Add(new SqlParameter("@IsDiscription", this.IsDiscriptive));
            object obj = DBManager.ExecuteScalar("TestMaster_SaveRecord", CommandType.StoredProcedure,oPara);
            return Convert.ToInt32(obj);
        }
        public string TestMaster_DeleteRecord()
        {
            List<SqlParameter> oPara = new List<SqlParameter>();
            oPara.Add(new SqlParameter("@TestID", this.TestID));
            object obj = DBManager.ExecuteDataTableWithParamiter("TestMaster_DeleteRecord", CommandType.StoredProcedure,  oPara);
            return obj.ToString();
        }
        public DataTable TestMaster_SelecteRecord()
        {
            List<SqlParameter> oPara = new List<SqlParameter>();
            oPara.Add(new SqlParameter("@TestID", this.TestID));
            DataTable dt = DBManager.ExecuteDataTableWithParamiter("TestMaster_SelectRecord", CommandType.StoredProcedure,  oPara);
            return dt;
        }
        public void SelecteRecordBYID()
        {
          DataTable DT = TestMaster_SelecteRecord();
            foreach (DataRow DR in DT.Rows)
            {
                TestName = DR["TestName"].ToString();
                Charge = Convert.ToDecimal(DR["Charge"]);
                IsDiscriptive = Convert.ToInt32(DR["IsDiscription"]);
                Description = Convert.ToString(DR["Description"]);
            }
        }

    }
   public class TestMasterDetail
    {
        public int TestDetailID;
        public int TestMasterID;
        public string FieldName;
        public string FieldDefaultValue;
        public int StatusID;
        public string HeadName;
        public string Unit;

        public int TestMasterDetail_SaveRecord()
        {
            try
            {
                List<SqlParameter> oPara = new List<SqlParameter>();
                oPara.Add(new SqlParameter("@TestDetailID", this.TestDetailID));
                oPara.Add(new SqlParameter("@TestMasterID", this.TestMasterID));
                oPara.Add(new SqlParameter("@FieldName", this.FieldName));
                oPara.Add(new SqlParameter("@FieldDefaultValue", this.FieldDefaultValue));
                oPara.Add(new SqlParameter("@StatusID", this.StatusID));
                oPara.Add(new SqlParameter("@HeadName", this.HeadName));
                oPara.Add(new SqlParameter("@Unit", this.Unit));                                                                      
                DataTable dt = DBManager.ExecuteDataTableWithParamiter("TestMasterDetail_SaveRecord", CommandType.StoredProcedure,  oPara);
                return Convert.ToInt32(dt.Rows[0]["ID"]);
            }
            catch (Exception Ex)
            {
                return -1;
            }
        }

        public DataTable TestMasterDetail_SelecteRecord(Int32 PTestMasterID, Int32 TransationDetailID)
        {
            List<SqlParameter> oPara = new List<SqlParameter>();
            oPara.Add(new SqlParameter("@TestMasterID", PTestMasterID));
            oPara.Add(new SqlParameter("@TestDetailID", TransationDetailID));

            DataTable dt = DBManager.ExecuteDataTableWithParamiter("TestMasterDetail_GetRecord", CommandType.StoredProcedure,  oPara);
            return dt;
        }
    }

    
}