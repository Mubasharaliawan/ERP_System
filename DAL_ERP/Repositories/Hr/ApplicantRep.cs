using DAL_ERP.Connection;
using DAL_ERP.Dto.Applicant;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL_ERP.Repositories.Applicant
{
    public class ApplicantRep
    {
       private string connection = new DbConnection().path;
    


        public string insert(ApplicantDto applicant)
        {
            string message = "0";

            SqlConnection conn = new SqlConnection(connection);
            try
            {

                SqlCommand cmd = new SqlCommand("sp_insertEmployee", conn);
                conn.Open();
                cmd.CommandType =CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ap_name", applicant.ap_name);
                cmd.Parameters.AddWithValue("@ap_cnic", applicant.ap_cnic);
                cmd.Parameters.AddWithValue("@ap_phone1", applicant.ap_phone1);
                cmd.Parameters.AddWithValue("@ap_phone2", applicant.ap_phone2);
                cmd.Parameters.AddWithValue("@ap_email", applicant.ap_email);
              /*  cmd.Parameters.AddWithValue("@ap_image", image);*/
                cmd.Parameters.AddWithValue("@ap_gender", applicant.ap_gender);
                cmd.Parameters.AddWithValue("@ap_dob", applicant.ap_dob);
                cmd.Parameters.AddWithValue("@ap_password", applicant.ap_password);
                cmd.ExecuteNonQuery();
                message = "1";



            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
          

            return message;
        }



    }
}
