using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Hospital.BL.MasterDetails
{
    class clsDoctorDetails
    {
        public int DoctorID;
        public string DoctorName;
        public string ContactNumber;
        public string Address;
        public string EmailID;
        public string ShopName;
        public string ShopeAddress;
        public string PhoneNo;
        public string Website;
        public string Specilization;
        public string Description;
        public int Commission;
        public clsDoctorDetails()
        {
            DoctorID = 0;
            DoctorName = "";
            ContactNumber = "";
            Address = "";
            EmailID = "";
            ShopName = "";
            ShopeAddress = "";
            PhoneNo = "";
            Website = "";
            Specilization = "";
            Description = "";
            Commission = 0;
        }
        public string DoctorDetails_SaveRecord()
        {
            List<IDbDataParameter> oPara = new List<IDbDataParameter>();
            oPara.Add(new SqlParameter("@DoctorID", this.DoctorID));
            oPara.Add(new SqlParameter("@DoctorName", this.DoctorName));
            oPara.Add(new SqlParameter("@ContactNumber", this.ContactNumber));
            oPara.Add(new SqlParameter("@Address", this.Address));
            oPara.Add(new SqlParameter("@EmailID", this.EmailID));
            oPara.Add(new SqlParameter("@ShopeAddress", this.ShopeAddress));
            oPara.Add(new SqlParameter("@ShopName", this.ShopName));
            oPara.Add(new SqlParameter("@PhoneNo", this.PhoneNo));
            oPara.Add(new SqlParameter("@Website", this.Website));
            oPara.Add(new SqlParameter("@Specilization", this.Specilization));
            oPara.Add(new SqlParameter("@Description", this.Description));
            oPara.Add(new SqlParameter("@Commission", this.Commission));
            object obj = SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "DoctorMaster_SaveRecord", oPara);
            return obj.ToString();
        }
        public string DoctorDetails_DeleteRecord()
        {
            List<IDbDataParameter> oPara = new List<IDbDataParameter>();
            oPara.Add(new SqlParameter("@DoctorID", this.DoctorID));
            object obj = SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "DoctorMaster_DeleteRecord", oPara);
            return obj.ToString();
        }
        public DataTable DoctorDetails_SelecteRecord()
        {
            List<IDbDataParameter> oPara = new List<IDbDataParameter>();
            oPara.Add(new SqlParameter("@DoctorID", this.DoctorID));
            DataTable dt = SQLHelper.GetDataTable(CommandType.StoredProcedure, "DoctorMaster_SelectRecord", oPara);
            return dt;
        }
        
    }
}
