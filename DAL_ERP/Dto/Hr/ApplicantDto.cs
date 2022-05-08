using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_ERP.Dto.Applicant
{
    public class ApplicantDto
    {
        [Key]
        public int ap_id { get; set; }

        [Required]
        
        public string? ap_name { get; set; }
        [Required]
        public string? ap_cnic { get; set; }
        [Required]
        public string? ap_phone1 { get; set; }
        [Required]
        public string? ap_phone2 { get; set; }
        [Required]
        public string? ap_email { get; set; }
     
      
        [Required]
        public int? ap_gender { get; set; }
        [Required]
        public string? ap_dob { get; set; }
        [Required]
        public string? ap_password { get; set; }


    }
}
