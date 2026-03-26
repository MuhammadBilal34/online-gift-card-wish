using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mytheme.Models;
using System.Security.Claims;

namespace mytheme.Controllers
{
    public class Cookies : Controller
    {
        StudentdbContext db = new StudentdbContext();
        public IActionResult index()
        {

            return View(db.Logins.ToList());
        }

        public IActionResult registration()
        {
            //ViewData["RoleId"] = new SelectList(db.Roles, "Id", "Namee");
            return View();
        }


        [HttpPost]
        public IActionResult registration(Login lg)
        {
            if (ModelState.IsValid)
            {
                lg.RoleId = 2;
                db.Logins.Add(lg);
                db.SaveChanges();

            }
            return View();
        }

        //change password krna ha //
        [HttpGet]
        public ActionResult regEdit(int id)
        {
            var login = db.Logins.Find(id);
            //  Login p = (from data in db.Logins where data.Id == id select data).FirstOrDefault();
            return View(login);
        }
        [HttpPost]
        public ActionResult regEdit(int id, Login login)
        {

            if (ModelState.IsValid)
            {

                db.Update(login);
                db.SaveChanges();

            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(Login lg)
        {
            ClaimsIdentity identity = null;
            bool isAuthenticated = false;

            var res = db.Logins.FirstOrDefault(x => x.Email == lg.Email && x.Passwordd == lg.Passwordd);
            if (res != null)
            {


                if (res.RoleId == 1)
                {


                    identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, lg.Email),
                    new Claim(ClaimTypes.Sid, res.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                    isAuthenticated = true;
                }

                if (res.RoleId == 2)
                {

                    identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, lg.Email),
                     new Claim(ClaimTypes.Sid, res.Id.ToString()),
                    new Claim(ClaimTypes.Role, "User")
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                    isAuthenticated = true;
                }

                if (isAuthenticated && res.RoleId == 1)
                {
                    var principal = new ClaimsPrincipal(identity);

                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    var principal = new ClaimsPrincipal(identity);

                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Client");

                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult Profile()
        {
            string abc = User.FindFirst(ClaimTypes.Sid)?.Value;

   var mydata = db.UserDetails.Where(z => z.UserId.Equals
(Convert.ToInt32(abc)));
            return View(mydata.ToList());
        }
    }

}
