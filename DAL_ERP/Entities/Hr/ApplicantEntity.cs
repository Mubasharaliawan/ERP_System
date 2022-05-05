using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_ERP.Entities.Applicant
{
    internal class ApplicantEntity
    {

        [Key]
        public int ap_id { get; set; }

        public string ap_name { get; set; }
        public string ap_cnic { get; set; }
        public string ap_phone1 { get; set; }
        public string ap_phone2 { get; set; }
        public string ap_email { get; set; }
        public string ap_image { get; set; }
        public int ap_gender { get; set; }
        public DateTime ap_dob { get; set; }


      


         
    }
}
