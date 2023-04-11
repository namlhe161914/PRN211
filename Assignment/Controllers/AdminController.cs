using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace project_prn211.Controllers
{
    public class AdminController : Controller
    {

        private readonly CenimaDBContext dBContext;
        private readonly IWebHostEnvironment _env;

        public AdminController(CenimaDBContext dBContext, IWebHostEnvironment env)
        {
            this.dBContext = dBContext;
            _env = env;

        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ListMovies()
        {
            if (HttpContext.Session.GetString("account") != null)
            {
                return RedirectToAction("Home", "Home");
            }
            else if (HttpContext.Session.GetString("account2") == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                List<Movie> movies = new List<Movie>();
                movies = dBContext.Movies.Include(g => g.Genre).ToList();
                return View("ListMovies", movies);
            }


        }

        public IActionResult CreateMovies()
        {
            if (HttpContext.Session.GetString("account") != null)
            {
                return RedirectToAction("Home", "Home");
            }
            else if (HttpContext.Session.GetString("account2") == null)
            {
                return RedirectToAction("Home", "Login");
            }

            else
            {
                ViewData["GenreId"] = new SelectList(dBContext.Genres, "GenreId", "Description");

                return View();
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateMovies(Movie movie, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("No file selected.");

            // Lấy tên file và đường dẫn file tới thư mục upload
            var fileName = file.FileName;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Image", fileName);


            // Lưu file vào đường dẫn đã chọn
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            movie.Status = 1;
            dBContext.Add(movie);
            dBContext.SaveChanges();



            //ViewData["GenreId"] = new SelectList(dBContext.Genres, "GenreId", "Description");

            return RedirectToAction("ListMovies");
            //return View(movie);
        }



        public IActionResult DeleteMovie(int id)
        {
            if (HttpContext.Session.GetString("account") != null)
            {
                return RedirectToAction("Home", "Home");
            }
            else if (HttpContext.Session.GetString("account2") == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                var movie = dBContext.Movies.Find(id);
                if (movie != null)
                {
                    if (dBContext.Rates.Any(x => x.MovieId == id))
                    {
                        ViewData["message"] = $"Không thể xoá phim vì có comment !";
                        return View("../Error");
                    }
                    else
                    {
                        dBContext.Movies.Remove(movie);
                        dBContext.SaveChanges();
                        return RedirectToAction("ListMovies");
                    }

                }


                return View();
            }



        }
        public ActionResult EditMovie(int id)
        {
            if (HttpContext.Session.GetString("account") != null)
            {
                return RedirectToAction("Home", "Home");
            }
            else if (HttpContext.Session.GetString("account2") == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                ViewData["GenreId"] = new SelectList(dBContext.Genres, "GenreId", "Description");
                var movie = dBContext.Movies.Find(id);
                if (movie != null)
                {
                    return View(movie);
                }

                return View("../Error");
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditMovie(Movie movie, IFormFile file)
        {
            if (file != null)
            {
                // Lấy tên file và đường dẫn file tới thư mục upload
                var fileName = file.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Image", fileName);

                // Lưu file vào đường dẫn đã chọn
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                movie.Image = fileName;
            }

            var m = dBContext.Movies.Find(movie.MovieId);
            if (m != null)
            {
                m.Title = movie.Title;
                m.Year = movie.Year;
                m.Description = movie.Description;
                m.GenreId = movie.GenreId;

                if (file != null)
                {
                    m.Image = movie.Image;
                }

                dBContext.Update(m);
                dBContext.SaveChanges();
            }

            return RedirectToAction("ListMovies");
        }


        public IActionResult ListGenres(int id)
        {
            if (HttpContext.Session.GetString("account") != null)
            {
                return RedirectToAction("Home", "Home");
            }
            else if (HttpContext.Session.GetString("account2") == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                List<Genre> genres = new List<Genre>();
                genres = dBContext.Genres.ToList();
                return View("ListGenres", genres);
            }

        }



        [HttpPost]
        public IActionResult CreateGenres(string Description)
        {
            Genre genre = new Genre();
            genre.Description = Description;
            dBContext.Add(genre);
            dBContext.SaveChanges();




            return RedirectToAction("ListGenres");
        }

        public IActionResult EditGenre(int id)
        {
            if (HttpContext.Session.GetString("account") != null)
            {
                return RedirectToAction("Home", "Home");
            }
            else if (HttpContext.Session.GetString("account2") == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                var genre = dBContext.Genres.Find(id);
                if (genre != null)
                {
                    return View(genre);
                }

                ViewData["message"] = $"Genre {id} not exist";
                return View("../Error");
            }

        }

        [HttpPost]
        public IActionResult EditGenre(Genre genre)
        {
            var m = dBContext.Genres.Find(genre.GenreId);
            if (m != null)
            {

                m.Description = genre.Description;

                dBContext.Update(m);
                dBContext.SaveChanges();
            }

            return RedirectToAction("ListGenres");
        }

        public IActionResult DeleteGenre(int id)
        {
            if (dBContext.Movies.Any(x => x.GenreId == id))
            {
                ViewData["message"] = $"Không thể xoá thể loại này vì có phim trong đó!";
                return View("../Error");
            }

            var genre = dBContext.Genres.Find(id);
            if (genre != null)
            {
                dBContext.Genres.Remove(genre);
                dBContext.SaveChanges();
                return RedirectToAction("ListGenres");
            }
            return View();
        }



        public IActionResult ListPerson()
        {

            List<Person> person = new List<Person>();
            person = dBContext.Persons.ToList();
            return View("ListPerson", person);
        }

        public IActionResult BlockPerson(int id)
        {



            var m = dBContext.Persons.Find(id);
            if (m != null)
            {
                m.IsActive = false;


                dBContext.Update(m);
                dBContext.SaveChanges();
            }

            //return View("ListPerson");
            return RedirectToAction("ListPerson");

        }

        public IActionResult UnblockPerson(int id)
        {



            var m = dBContext.Persons.Find(id);
            if (m != null)
            {
                m.IsActive = true;


                dBContext.Update(m);
                dBContext.SaveChanges();

            }

            return RedirectToAction("ListPerson");


        }

        public IActionResult UpdateRole(int id)
        {



            var m = dBContext.Persons.Find(id);
            if (m != null)
            {
                m.Type = 1;


                dBContext.Update(m);
                dBContext.SaveChanges();

            }

            return RedirectToAction("ListPerson");


        }

        public IActionResult UpdateRoleUser(int id)
        {



            var m = dBContext.Persons.Find(id);
            if (m != null)
            {
                m.Type = 2;


                dBContext.Update(m);
                dBContext.SaveChanges();

            }

            return RedirectToAction("ListPerson");


        }

        public IActionResult Statistical()
        {



            List<Movie> movies = new List<Movie>();
            var arlist = new ArrayList();
            var ratecount = new ArrayList();
            var cmtCount = new ArrayList();
            var name = new ArrayList();
            movies = dBContext.Movies.Include(g => g.Genre).ToList();
            foreach (Movie m in movies)
            {
                double? n = 0;

                var rate = dBContext.Rates.Where(r => r.MovieId == m.MovieId).ToList();
                foreach (var r in rate)
                {
                    n += r.NumericRating;

                }
                double? avg = n / rate.Count;

                arlist.Add(avg);
                ratecount.Add(rate.Count);

                int commentCount = dBContext.Rates.Where(r => r.Comment != null && r.MovieId == m.MovieId).Count();
                cmtCount.Add(commentCount);

                name.Add(m.Title);
            }
            ViewBag.name = name;
            ViewBag.avg = arlist;
            ViewBag.ratecount = ratecount;
            ViewBag.cmtCount = cmtCount;

            return View("Statistical", movies);


        }

        public IActionResult BlockMovie(int id)
        {


            var m = dBContext.Movies.Find(id);
            if (m != null)
            {
                m.Status = 0;


                dBContext.Update(m);
                dBContext.SaveChanges();

            }

            return RedirectToAction("ListMovies");


        }

        public IActionResult UnblockMovie(int id)
        {


            var m = dBContext.Movies.Find(id);
            if (m != null)
            {
                m.Status = 1;


                dBContext.Update(m);
                dBContext.SaveChanges();

            }

            return RedirectToAction("ListMovies");


        }

    }
}
