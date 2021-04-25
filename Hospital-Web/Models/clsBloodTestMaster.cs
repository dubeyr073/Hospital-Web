using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Hospital.BL.MasterDetails
{
    class clsBloodTestMaster
    {
        public int BloodTestID;
        public string TestName;
        public decimal Charge;
        public decimal MinValue;
        public decimal MaxValue;
        public string Description;
        public decimal Discount;
        public decimal Commission;
       
        public clsBloodTestMaster()
        {
            BloodTestID = 0;
            TestName = "";
            Charge = 0;
            MinValue = 0;
            MaxValue = 0;
            Description = "";
            Discount = 0;
            Commission = 0;
        }
        public string BloodTestMaster_SaveRecord()
        {
            List<IDbDataParameter> oPara = new List<IDbDataParameter>();
            oPara.Add(new SqlParameter("@BloodTestID", this.BloodTestID));
            oPara.Add(new SqlParameter("@TestName", this.TestName));
            oPara.Add(new SqlParameter("@Charge", this.Charge));
            oPara.Add(new SqlParameter("@MinValue", this.MinValue));
            oPara.Add(new SqlParameter("@MaxValue", this.MaxValue));
            oPara.Add(new SqlParameter("@Description", this.Description));
            oPara.Add(new SqlParameter("@Discount", this.Discount));
            oPara.Add(new SqlParameter("@Commission", this.Commission));

            object obj = SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "BloodTestMaster_SaveRecord", oPara);
            return obj.ToString();
        }
        public string BloodTestMaster_DeleteRecord()
        {
            List<IDbDataParameter> oPara = new List<IDbDataParameter>();
            oPara.Add(new SqlParameter("@BloodTestID", this.BloodTestID));
            object obj = SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "BloodTestMaster_DeleteRecord", oPara);
            return obj.ToString();
        }
        public DataTable BloodTestMaster_SelecteRecord()
        {
            List<IDbDataParameter> oPara = new List<IDbDataParameter>();
            oPara.Add(new SqlParameter("@BloodTestID", this.BloodTestID));
            DataTable dt = SQLHelper.GetDataTable(CommandType.StoredProcedure, "BloodTestMaster_SelectRecord", oPara);
            return dt;
        }
    }
    class clsTestMasterDetail
    {
        public int TestDetailID;
        public int TestMasterID;
        public string FieldName;
        public string FieldDefaultValue;
        public int StatusID;
        public int TestMasterDetail_SaveRecord()
        {
            try
            {
                List<IDbDataParameter> oPara = new List<IDbDataParameter>();
                oPara.Add(new SqlParameter("@TestDetailID", this.TestDetailID));
                oPara.Add(new SqlParameter("@TestMasterID", this.TestMasterID));
                oPara.Add(new SqlParameter("@FieldName", this.FieldName));
                oPara.Add(new SqlParameter("@FieldDefaultValue", this.FieldDefaultValue));
                oPara.Add(new SqlParameter("@StatusID", this.StatusID));

                object obj = SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "TestDetailMaster_SaveRecord", oPara);
                return Convert.ToInt32(obj);
            }
            catch (Exception Ex)
            {
               return -1;
            }
        }

        public DataTable TestMasterDetail_SelecteRecord(Int32 PTestMasterID, Int32 TransationDetailID)
        {
            List<IDbDataParameter> oPara = new List<IDbDataParameter>();
            oPara.Add(new SqlParameter("@TestMasterID", PTestMasterID));
            oPara.Add(new SqlParameter("@TransationDetailID", TransationDetailID));
            
            DataTable dt = SQLHelper.GetDataTable(CommandType.StoredProcedure, "TestDetailMaster_GetRecord", oPara);
            return dt;
        }
    }
}
