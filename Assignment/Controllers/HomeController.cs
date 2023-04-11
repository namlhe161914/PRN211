using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Text.Json;



namespace Assignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly CenimaDBContext dBContext;

        public HomeController(CenimaDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Sort(int a)
        {
            List<Movie> movies = new List<Movie>();
            ViewBag.Genres = dBContext.Genres.ToList();
            if (a==1)
            {

            }
            
            return View(movies);
        }
        public IActionResult Home(int pid)
        {
            List<Movie> movies = new List<Movie>();
            var name = new ArrayList();
            var year = new ArrayList();
            var type = new ArrayList();
            var av = new ArrayList();
            var id = new ArrayList();
            var img = new ArrayList();
            ViewBag.Genres = dBContext.Genres.ToList();
            if (pid != 0)
            {
                movies = dBContext.Movies.Include("Genre").Where(c => c.GenreId == pid && c.Status==1).ToList();
            }
            else
            {
                movies = dBContext.Movies.Include("Genre").Where(c=> c.Status == 1).ToList();
            }
            foreach (Movie m in movies)
            {
                double? n = 0;
                var rate = dBContext.Rates.Where(r => r.MovieId == m.MovieId).ToList();
                foreach (var r in rate)
                {
                    n += r.NumericRating;
                }
                double? avg = n / rate.Count;
                avg = Math.Round((double)avg, 2);
                name.Add(m.Title);
                year.Add(m.Year);
                type.Add(m.Genre.Description);
                av.Add(avg);
                id.Add(m.MovieId);
                img.Add(m.Image);
            }
            ViewBag.name = name;
            ViewBag.year = year;
            ViewBag.type = type;
            ViewBag.id = id;
            ViewBag.av = av;
            ViewBag.img = img;
            return View(movies);
        }

        public IActionResult Search(string key)
        {
            ViewBag.Genres = dBContext.Genres.ToList();
            List<Movie> products = new List<Movie>();
            List<Movie> search = new List<Movie>();
            products = dBContext.Movies.Include(p => p.Genre).ToList();
            if (key != null)
            {
                foreach (Movie product in products)
                {
                    string title = ConvertToUnsignString(product.Title);
                    key = ConvertToUnsignString(key);
                    if (title.ToLower().Contains(key.ToLower(), StringComparison.OrdinalIgnoreCase))
                    {
                        search.Add(product);
                    }
                }
                return View(search);
            }

            return View(products);
        }

        public IActionResult Detail(int id)
        {
            double? n = 0;
            var rate = dBContext.Rates.Where(r => r.MovieId == id).ToList();
            foreach (var r in rate)
            {
                n += r.NumericRating;
            }
            double? avg = n / rate.Count;
            if (rate.Count == 0)
            {
                ViewBag.avg = "Chưa có đánh giá";
            }
            else
            {
                ViewBag.avg = Math.Round((decimal)avg, 2);
            }
            ViewBag.id = id;
            ViewBag.rates = dBContext.Rates.Include("Person").Where(r => r.MovieId == id).ToList();
            ViewBag.movies = dBContext.Movies.Include("Genre").Include("Rates").Where(c => c.MovieId == id);
            return View();

        }

        public IActionResult Comment(int id)
        {
            if (HttpContext.Session.GetString("account") == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                
                double? n = 0;
                var rate = dBContext.Rates.Where(r => r.MovieId == id).ToList();
               
                foreach (var r in rate)
                {
                    n += r.NumericRating;
                }
                double? avg = n / rate.Count;
                if (rate.Count == 0)
                {
                    ViewBag.avg = "Chưa có đánh giá";
                }
                else
                {
                    ViewBag.avg = Math.Round((decimal)avg, 2);
                }
                ViewBag.id = id;
                ViewBag.rates = dBContext.Rates.Include("Person").Where(r => r.MovieId == id).ToList();
                ViewBag.movies = dBContext.Movies.Include("Genre").Include("Rates").Where(c => c.MovieId == id);
                
                 
                return View();
            }
            

        }

        [HttpPost]
        public IActionResult Comment(Rate rate)
        {

            //Kiểm tra dữ liệu tren form đã hợp lệ chưua
            if (ModelState.IsValid)
            {
                dBContext.Add(rate);
                dBContext.SaveChanges();
                double? n = 0;
                var ra = dBContext.Rates.Where(r => r.MovieId == rate.MovieId).ToList();
                foreach (var r in ra)
                {
                    n += r.NumericRating;
                }
                double? avg = n / ra.Count;
                if (ra.Count == 0)
                {
                    ViewBag.avg = "Chưa có đánh giá";
                }
                else
                {
                    ViewBag.avg = Math.Round((decimal)avg, 2);
                }
                ViewBag.id = rate.MovieId;
                ViewBag.rates = dBContext.Rates.Include("Person").Where(r => r.MovieId == rate.MovieId).ToList();
                ViewBag.movies = dBContext.Movies.Include("Genre").Include("Rates").Where(c => c.MovieId == rate.MovieId);
                return RedirectToAction("Detail", new { id = rate.MovieId });
            }

            return View("Home", "Home");
        }

        private string ConvertToUnsignString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            string[] signs = new string[] {
        "aAeEoOuUiIdDyY",
        "áàạảãâấầậẩẫăắằặẳẵ",
        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
        "éèẹẻẽêếềệểễ",
        "ÉÈẸẺẼÊẾỀỆỂỄ",
        "óòọỏõôốồộổỗơớờợởỡ",
        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
        "úùụủũưứừựửữ",
        "ÚÙỤỦŨƯỨỪỰỬỮ",
        "íìịỉĩ",
        "ÍÌỊỈĨ",
        "đ",
        "Đ",
        "ýỳỵỷỹ",
        "ÝỲỴỶỸ"
    };

            for (int i = 1; i < signs.Length; i++)
            {
                for (int j = 0; j < signs[i].Length; j++)
                {
                    str = str.Replace(signs[i][j], signs[0][i - 1]);
                }
            }

            return str;
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}