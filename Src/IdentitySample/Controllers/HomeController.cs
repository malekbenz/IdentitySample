using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IdentitySample.Models;

namespace IdentitySample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> OldLogin(string name, string ReturnUrl)
        {
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction(nameof(Index));
            }
            List<Claim> UserClaims = new List<Claim>();
            UserClaims.Add(new Claim(ClaimTypes.Name, name));
            UserClaims.Add(new Claim(ClaimTypes.Role, "users"));

            if (name == "benzemam")
            {
                UserClaims.Add(new Claim(ClaimTypes.Role, "admins"));
            }
            else
            {
                UserClaims.Add(new Claim(ClaimTypes.MobilePhone, "0561 12 12 15"));
            }


            var identity = new ClaimsIdentity(
                UserClaims, CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal);

            if (!string.IsNullOrEmpty(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }

            return RedirectToAction(nameof(Index));

        }

        [Authorize(Policy = "mustBeBenz")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize(Policy = "mustHavePhone")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }


        public IActionResult Forbidden()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
