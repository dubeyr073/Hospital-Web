
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Hospital.BL.MasterDetails
{
    class TransactionLine
    {
        public string Transation_Ref_No;
        public int Transation_No;
        public int TransationID;
        public int HospitalID;
        public int DoctorID;
        public int PaitentID;
        public string Remark;
        public TransactionLine()
        {
            Transation_Ref_No = "";
            Transation_No = 0;
            HospitalID = 0;
            DoctorID = 0;
            PaitentID = 0;
            Remark = "";
            TransationID = 0;
        }
        public string TransationDetails_SaveRecord()
        {
            List<IDbDataParameter> oPara = new List<IDbDataParameter>();
            oPara.Add(new SqlParameter("@TransationID", this.TransationID));
            oPara.Add(new SqlParameter("@Transation_Ref_No", this.Transation_Ref_No));
            oPara.Add(new SqlParameter("@Transation_No", this.Transation_No));
            oPara.Add(new SqlParameter("@HospitalID", this.HospitalID));
            oPara.Add(new SqlParameter("@DoctorID", this.DoctorID));
            oPara.Add(new SqlParameter("@PaitentID", this.PaitentID));
            oPara.Add(new SqlParameter("@Remark", this.Remark));
            object obj = SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "TransactionLine_Save", oPara);
            return obj.ToString();
        }
        public DataTable TransationDetails_SearchData(int Par1,string Par2)
        {
            List<IDbDataParameter> oPara = new List<IDbDataParameter>();
            oPara.Add(new SqlParameter("@TransationID", Par1));
            oPara.Add(new SqlParameter("@MobileNo", Par2));
            DataTable dt = SQLHelper.GetDataTable(CommandType.StoredProcedure, "TransationLineMaster_AllData", oPara);
            return dt;
        }
        
    }
}
