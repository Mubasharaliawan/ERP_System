using DAL_ERP.Dto.Applicant;
using DAL_ERP.Repositories.Applicant;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERP.Controllers.Hr
{
    public class ApplicantController : Controller
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;

        public ApplicantController(Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        ApplicantRep rep = new ApplicantRep();
        public IActionResult SignUp()
        {
           
           
            return View();
        }
        public IActionResult SignIn()
        {


            return View();
        }

        public JsonResult ApplicantSignUp(ApplicantDto dto)
        {

            try
            {


                string list = rep.insert(dto);
                

               var json = JsonConvert.SerializeObject(list);
                
                return /*RedirectToAction("Signup", "Applicant");*/
                Json(new
                                {
                                    MessageCode = "01",
                                    Message = "Record Found",
                                    MessageType = true,
                                    Result= json,   


                                });


            }
            catch (Exception e)
            {
                

                return 
              Json(new
                {
                    MessageCode = "02",
                    Message = "Record Not Found",
                    MessageType = false,
                    error = e.Message + " " + e.HelpLink,

                });



            }
        }

    }
}
