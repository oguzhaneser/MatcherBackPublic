using Matcher.BLL.Interfaces;
using Matcher.DATA.Models;
using Matcher.Panel.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Matcher.Panel.Controllers
{
    [AllowAnonymous]
    public class AccountsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IBaseRepository<User> _userRep;
        public AccountsController(IConfiguration configuration, IBaseRepository<User> userRep)
        {
            _configuration = configuration;
            _userRep = userRep;
        }

        public IActionResult Index(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountVM model)
        {
            try
            {

                if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                    return View("Index");

                User dbUser = _userRep.Get(filter: u => u.Username == model.Username && u.Password == model.Password && u.IsActive == true);

                if (dbUser != null)
                {

                    List<Claim> authClaims = new()
                    {
                        new(ClaimTypes.Name,dbUser.Name),
                        new(ClaimTypes.Surname,dbUser.Surname),
                        new(ClaimTypes.SerialNumber,dbUser.Id.ToString()),
                        new(ClaimTypes.Gender,dbUser.Gender.ToString()),
                        new(ClaimTypes.MobilePhone,dbUser.PhoneNumber),
                        new(ClaimTypes.Email,dbUser.Email),
                    };

                    if(dbUser.Id== 1)
                    {
                        authClaims.Add(new(ClaimTypes.Role, "Admin"));
                    }

                    ClaimsIdentity userIdentity = new(authClaims, "login");
                    ClaimsPrincipal principal = new(userIdentity);

                    await HttpContext.SignInAsync(principal);
                    if(!string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }


            }
            catch (Exception exc)
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AccessDenied()
        {
            return RedirectToAction("Error", "Home", new { errorMessage = "Erişim izniniz bulunmamaktadır." });
        }


    }
}
