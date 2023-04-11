using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Assignment.Controllers
{
    public class SignupController : Controller
    {
        public readonly CenimaDBContext db;

        public SignupController(CenimaDBContext db)
        {
            this.db = db;
        }
        
        public IActionResult Home()
        {
            if (HttpContext.Session.GetString("account") != null)
            {
                return RedirectToAction("Home", "Home");
            }
            else if (HttpContext.Session.GetString("account2") != null)
            {
                return RedirectToAction("ListMovies", "Admin");
            }
            else
            {
                return View();
            }
           
        }
        [HttpPost]
        public IActionResult Home( Person pn)
        {
            string err = "";

            if (pn.Email == null && pn.Password == null)
            {
                ViewData["Error1"] = "Email không để trống";
                ViewData["Error2"] = "Mật khẩu không để trống";
                return View();
            }
            else if(pn.Fullname == null)
            {
                ViewData["Error4"] = "Tên không được trống";
                return View();
            }
            else if (pn.Gender == null)
            {
                ViewData["Error3"] = "Vui lòng chọn giới tính";
                return View();
            }
            else
            {
                var person = db.Persons.SingleOrDefault(p => p.Email == pn.Email);

                if (person != null)
                {
                    err = "Email đã tồn tại";
                    ViewData["Error"] = err;
                    return View();

                }
                else
                {
                    
                    pn.Type = 2;
                    pn.IsActive= true;
                        db.Persons.Add(pn);
                        
                    
                        
                        string sc = "Đăng kí thành công";
                        TempData["SuccessMessage"] = sc;

                    db.SaveChanges();
                    return RedirectToAction("Home","Login");
                

                }

            }
        }
    }
}
