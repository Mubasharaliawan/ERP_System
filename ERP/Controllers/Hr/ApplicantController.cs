using DAL_ERP.Dto.Applicant;
using DAL_ERP.Dto.Hr;
using DAL_ERP.Repositories.Applicant;
using IdentityModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Newtonsoft.Json;
using System.Security.Claims;

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
       
        /* private readonly UserManager<ApplicantDto> _usermaneger;
         public ApplicantController(UserManager<ApplicantDto> usermaneger)
         {
             _usermaneger = usermaneger;

         }*/
        public IActionResult SignUp()
        {
           
           
            return View();
        }
        public IActionResult SignIn()
        {
            if (TempData !=null)
            {
                ViewBag.message = TempData["responce"];
            }
            
            return View();
        }

        [Authorize]

        public IActionResult Profile()
        {
           
           

          

            return View();

        }

        [HttpGet]
        public JsonResult ApplicantProfile()
        {

            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity!.Claims;

            if (userIdentity.IsAuthenticated)
            {
                try
                {
                    var id = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value.ToString();
                    var list = rep.ApplicantProfile(id);
                    var json = JsonConvert.SerializeObject(list);
                    return Json(new
                    {
                        MessageCode = "01",
                        Message = "Record Found",
                        MessageType = true,
                        Result = json

                    });
                }
                catch (Exception e)
                {

                    return Json(new
                    {
                        MessageCode = "02",
                        Message = "Record Not Found",
                        MessageType = false,
                        error = e.Message + " " + e.HelpLink,

                    });

                }

            }
            return Json(new
            {
                MessageCode = "03",
                Message = "Login Again!",
                MessageType = false,
               
            });


        }
        public async Task<JsonResult> ApplicantProfileLogout()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {

                    await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
                    return Json(new
                    {
                        MessageCode = "01",
                        Message = "Signout Success",
                        MessageType = true,


                    });
                }
                catch (Exception e)
                {

                    return Json(new
                    {
                        MessageCode = "02",
                        Message = "Something Went Wrong!",
                        MessageType = false,

                    });
                }


            }
            return Json(new
            {
                MessageCode = "03",
                Message = "User Already Signed Out",
                MessageType = false,

            });



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

        /*public RedirectToActionResult ApplicantSignIn(SigninDto dto)
        {

            



        }*/





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(ApplicantDto dto)
        {
            if (!string.IsNullOrEmpty(dto.ap_email) && string.IsNullOrEmpty(dto.ap_password))
            {
                return RedirectToAction("Signin");
            }

            var list = rep.signIn(dto);
            var myemail = "";
            var mypassword = "";
            var myname = "";
            var myid=0-1;

            foreach (var data in list)
            {
                myemail = data.ap_email;
                mypassword = data.ap_password;
                myname = data.ap_name;
                myid = data.ap_id;


            }

            if (dto.ap_email == myemail && dto.ap_password == mypassword)
            {
                //Login log = new Login();


                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,myid.ToString()),
                    new Claim(ClaimTypes.Email,myemail),
                    new Claim("FullName",myname),
                    
                };
                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var claimsPrincipal = new ClaimsPrincipal(identity);
               
                // Set current principal
             var  currentThred=  Thread.CurrentPrincipal = claimsPrincipal;
                





                /*var claimsIdentity = new ClaimsIdentity(claims, "Signin");*/
               /* var claimsIdentity = new ClaimsIdentity(
          claims, CookieAuthenticationDefaults.AuthenticationScheme);*/

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                
                TempData["name"] = myname;
                return RedirectToAction("profile", "Applicant");
            }
            else
            {
                TempData["responce"] = "Your Email And Password is not matched!";
                
                return RedirectToAction("Signin", "Applicant");
            }





        }

       /* public ActionResult UserDashBoard()
        {
          
        }*/


















    }
}
