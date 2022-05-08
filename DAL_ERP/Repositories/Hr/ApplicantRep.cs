using DAL_ERP.Connection;
using DAL_ERP.Dto.Applicant;
using DAL_ERP.Dto.Hr;
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

        public List<ApplicantDto> signIn(ApplicantDto dto)
        {
           
            List<ApplicantDto> list = new List<ApplicantDto>(); 

            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand("sp_signin", conn);
           
            cmd.Parameters.AddWithValue("@ap_email", dto.ap_email);
            cmd.Parameters.AddWithValue("@ap_password", dto.ap_password);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                
                
               
              SqlDataReader sdr = cmd.ExecuteReader();
                 ApplicantDto dto2 = new ApplicantDto();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                      dto2.ap_name= Convert.ToString(sdr["ap_name"]);
                      dto2.ap_email= Convert.ToString(sdr["ap_email"]);
                      dto2.ap_password= Convert.ToString(sdr["ap_password"]);
                      dto2.ap_id= Convert.ToInt32(sdr["ap_id"]);
                     

                    }
                    list.Add(dto2);


                }
                conn.Close();
               

            }
            catch (Exception e)
            {

                throw;
            }
            
           

            return list;
        }

        public List<ApplicantDto> ApplicantProfile(string obj)
        {
            List<ApplicantDto> list = new List<ApplicantDto>();
                
            SqlConnection conn=new SqlConnection(connection);

            SqlCommand cmd = new SqlCommand("sp_applicantDetails", conn);

            int id = Convert.ToInt32(obj);
            cmd.Parameters.AddWithValue("@ap_id", id);
            cmd.CommandType=CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader sdr= cmd.ExecuteReader();
                if (sdr.HasRows)

                {
                    ApplicantDto dto2= new ApplicantDto();  
                    while (sdr.Read())
                    {
                        dto2.ap_name = Convert.ToString(sdr["ap_name"]);
                        dto2.ap_email = Convert.ToString(sdr["ap_email"]);
                        dto2.ap_phone1 = Convert.ToString(sdr["ap_phone1"]);
                        dto2.ap_phone2 = Convert.ToString(sdr["ap_phone2"]);
                        dto2.ap_cnic = Convert.ToString(sdr["ap_cnic"]);
                        dto2.ap_dob = Convert.ToString(sdr["ap_dob"]);
                        dto2.ap_gender = Convert.ToInt32(sdr["ap_gender"]);
                        
                    }
                    list.Add(dto2);

                }


            }
            catch (Exception e)
            {

                throw;
            }
       





            return list;


        }




    }
}
