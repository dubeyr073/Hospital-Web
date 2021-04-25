using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Hospital_Web.Models
{
    class clsHospitalDetails
    {
        public string Action;
        public int HospitalID;
        public string HospitalName;
        public string Address;
        public string ContactNumber;
        public string EmailId;
        public string Website;
        public string Description;
        public clsHospitalDetails()
        {
            Action = "";
            HospitalID = 0;
            HospitalName = "";
            Address = "";
            ContactNumber = "";
            EmailId = "";
            Website = "";
            Description = "";
        }
    }
}
