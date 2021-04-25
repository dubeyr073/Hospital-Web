using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Hospital.BL.MasterDetails
{
    class TransationDetailsMaster
    {
        public int TransationDetailsID;
      public int TransactionID;
      public  int HospitalID;
      public int DoctorID;
      public int PaitentID;
      public int TestID;
      public decimal Rate;
      public string Description;
      public int Quantity;
      public decimal Amount;
      public string Remark;
      public TransationDetailsMaster()
      {
          TransationDetailsID = 0;
          TransactionID = 0;
          HospitalID = 0;
          DoctorID = 0;
          PaitentID = 0;
          TestID = 0;
          Rate = 0;
          Description = "";
          Quantity = 0;
          Amount = 0;
          Remark = "";

      }
      public static DataTable getRecord(string sql)
      {
          DataTable dt = SQLHelper.GetDataTable(CommandType.Text, sql);
          return dt;
      }
      public string TransationDetails_SaveRecord()
      {
          List<IDbDataParameter> oPara = new List<IDbDataParameter>();
          oPara.Add(new SqlParameter("@TransationID", this.TransactionID));
          oPara.Add(new SqlParameter("@TestID", this.TestID));
          oPara.Add(new SqlParameter("@Rate", this.Rate));
          oPara.Add(new SqlParameter("@Description", this.Description));
          oPara.Add(new SqlParameter("@Quantity", this.Quantity));
          oPara.Add(new SqlParameter("@Amount", this.Amount));
          oPara.Add(new SqlParameter("@Remark", this.Remark));
          

          object obj = SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "TransationDetailsMaster_SaveRecord", oPara);
          return obj.ToString();
      }
      public DataTable TransationDetails_SelectRecord(string sql)
      {
          List<IDbDataParameter> oPara = new List<IDbDataParameter>();
          oPara.Add(new SqlParameter("@TransationDetailsID", this.TransationDetailsID));
          DataTable dt = SQLHelper.GetDataTable(CommandType.Text, sql, oPara);
          return dt;
      }

      public DataTable TransationDetails_SelectRecord(int Par1)
      {
          List<IDbDataParameter> oPara = new List<IDbDataParameter>();
          oPara.Add(new SqlParameter("@TransationDetailsID", Par1));
          DataTable dt = SQLHelper.GetDataTable(CommandType.StoredProcedure, "TransationLineMaster_Getdata", oPara);
          return dt;
      }
      public string TransactionTestRecord_SaveRecord(int TransactionDetailsID, int TestMasterID, int TestDetailedID, string CurrentValue)
      {
          List<IDbDataParameter> oPara = new List<IDbDataParameter>();
          oPara.Add(new SqlParameter("@TransactionDetailsID", TransactionDetailsID));
          oPara.Add(new SqlParameter("@TestDetailedID", TestDetailedID));
          oPara.Add(new SqlParameter("@CurrentValue", CurrentValue));
          oPara.Add(new SqlParameter("@TestMasterID", TestMasterID)); 
          object obj = SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "TransactionTestRecord_SaveRecord", oPara);
          return obj.ToString();
      }
    }
}
