using DAL_ERP.Connection;
using DAL_ERP.Dto.Applicant;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(options =>
{
    options.LoginPath = "/Applicant/Signin";

  /*  options.Events = new CookieAuthenticationEvents()
    {
        OnSigningIn =  async context=>
        {
            var principle = context.Principal;
            if (principle.HasClaim(c => c.Type==ClaimTypes.NameIdentifier))
            {
                if (principle.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.NameIdentifier).)
                {

                }
            }
        }


    };*/

});



builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
