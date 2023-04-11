using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace Assignment.Controllers
{
    public class LoginController : Controller
    {
      
        public readonly CenimaDBContext db;

        public LoginController(CenimaDBContext db)
        {
            this.db = db;
        }
        
        public IActionResult Home()
        {
            if (HttpContext.Session.GetString("account") != null)
            {
                return RedirectToAction("Home", "Home");
            }
            else if(HttpContext.Session.GetString("account2") != null)
            {
                return RedirectToAction("ListMovies", "Admin");
            }
            else
            {
                if (TempData.ContainsKey("SuccessMessage"))
                {
                    string sc = TempData["SuccessMessage"].ToString();
                    ViewData["SuccessMessage"] = sc;
                }
                return View();
            }
               
        }
        [HttpPost]
        public IActionResult Home(Person pn)

        {
            string err = "";
           
            if(pn.Email==null && pn.Password==null)
            {
                ViewData["Error1"] = "Email không để trống";
                ViewData["Error2"] = "Mật khẩu không để trống";
                return View();
            }
            else
            {
                var person = db.Persons.SingleOrDefault(p => p.Email == pn.Email
                    && p.Password == pn.Password);
                if(person == null)
                {
                    err = "Email hoặc mật khẩu không đúng";
                    ViewData["Error"] = err;
                    return View();

                }
                else
                {
                    if(person.IsActive == false)
                    {
                        err = "Tài khoản không hoạt động";
                        ViewData["Error"] = err;
                        return View();
                    }
                    else
                    {
                        if(person.Type == 1)
                        {
                            HttpContext.Session.SetString("account2", JsonSerializer.Serialize(person));
                            return RedirectToAction("ListMovies","Admin");

                        }
                        else
                        {
                            HttpContext.Session.SetString("account", JsonSerializer.Serialize(person));
                            return RedirectToAction("Home", "Home");
                        }
                      
                    }
                   
                }
                
            }
        }
        public IActionResult SignOut()
        {
            if (HttpContext.Session.GetString("account") != null || HttpContext.Session.GetString("account2") != null) 
                HttpContext.Session.Remove("account");
            HttpContext.Session.Remove("account2");
            return RedirectToAction("Home", "Home");
        }
    }
}
