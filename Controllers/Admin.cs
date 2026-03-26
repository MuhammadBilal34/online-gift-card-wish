using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mytheme.Models;
using System.Security.Claims;

namespace mytheme.Controllers
{
    public class Admin : Controller
    {
        
        
        
        StudentdbContext db = new StudentdbContext();

        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            return View(db.Studata.ToList());
        }
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateStudent(Studatum marzi)
        {
            if(ModelState.IsValid)
            {
                db.Studata.Add(marzi);
                db.SaveChanges();
                return Content("Thank you for submiting data");
            }
            return View();
        }
        [HttpGet]
        public IActionResult EditStudent(int id)
        {
           var abc=db.Studata.Find(id);

            return View(abc);
        }
        [HttpPost]
        public IActionResult EditStudent(Studatum marzi)
        {
            if (ModelState.IsValid)
            {
                db.Studata.Update(marzi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult DeleteStudent(int id)
        {
            var abc = db.Studata.Find(id);

            return View(abc);
        }
        [HttpPost]
        public IActionResult DeleteStudent(Studatum marzi)
        {
            if (ModelState.IsValid)
            {
                db.Studata.Remove(marzi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }



        [HttpGet]
        public IActionResult Details(int id)
        {
         

            var t = (from data in db.Studata where data.Id == id select data).FirstOrDefault();


            return View(t);
        }



        // category ka kaam ////
        [Authorize(Roles = "Admin")]
        public IActionResult showcateg()
        {
            return View(db.Categs.ToList());
        }


        [HttpGet]
        public IActionResult Createcateg()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Createcateg(Categ catt)
        {
            if (ModelState.IsValid)
            {
                db.Categs.Add(catt);
                db.SaveChanges();
                return Content("Thank you for submiting data");
            }
            return View();
        }
        [HttpGet]
        public IActionResult EditCateg(int id)
        {
            var abc = db.Categs.Find(id);

            return View(abc);
        }
        [HttpPost]
        public IActionResult EditCateg(Categ catt)
        {
            if (ModelState.IsValid)
            {
                db.Categs.Update(catt);
                db.SaveChanges();
                return RedirectToAction("showcateg");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Deletecateg(int id)
        {
            var abc = db.Categs.Find(id);

            return View(abc);
        }
        [HttpPost]
        public IActionResult Deletecateg(Categ catt)
        {
            if (ModelState.IsValid)
            {
                db.Categs.Remove(catt);
                db.SaveChanges();
                return RedirectToAction("showcateg");
            }
            return View();
        }


        /// ////////// product ka kaam .////////////

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult addpro()
        {
            ViewData["CatId"] = new SelectList(db.Categs, "Id", "Namee");

            return View();
        }
        [HttpPost]
        public IActionResult addpro(Product pr, IFormFile file)
        {
            var imageName = Path.GetFileName(file.FileName);
            string imagePath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Images/");
            string imagevalue = Path.Combine(imagePath, imageName);
            using (var stream = new FileStream(imagevalue, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            var dbimage = Path.Combine("/Images/", imageName);

            pr.Picture = dbimage;
            db.Products.Add(pr);
            db.SaveChanges();

          
            return RedirectToAction("product");
        }



        [Authorize(Roles = "Admin")]
        public IActionResult product()
        {
            var koibhi = db.Products.Include(p => p.Cat);
            return View(koibhi.ToList());
        }



        [HttpGet]
        public IActionResult Deletepro(int id)
        {
            var abc = db.Products.Find(id);

            return View(abc);
        }
        [HttpPost]
        public IActionResult Deletepro(Product pro)
        {
            if (ModelState.IsValid)
            {
                db.Products.Remove(pro);
                db.SaveChanges();
                return RedirectToAction("product");
            }
            return View();
        }

        [HttpGet]
        public IActionResult proupdate(int id)
        {


            ViewData["CatId"] = new SelectList(db.Categs, "Id", "Namee");
            var data = db.Products.Find(id);
            return View(data);
        }


        [HttpPost]
        public IActionResult proupdate(int id, Product vs, IFormFile file, string hid)
        {
            
                var dbimage = "";
                if (file != null && file.Length > 0)
                {
                    var imageName = Path.GetFileName(file.FileName);
                    string imagePath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Images/");
                    string imagevalue = Path.Combine(imagePath, imageName);
                    using (var stream = new FileStream(imagevalue, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    dbimage = Path.Combine("/Images/", imageName);
                    vs.Picture = dbimage;
                    db.Update(vs);
                    db.SaveChanges();
                }
                else
                {
                    vs.Picture = hid;
                    db.Update(vs);
                    db.SaveChanges();

                }
            
          
            return RedirectToAction("product");
        }



        [Authorize(Roles = "Admin")]
        public IActionResult uploads()
        {


            var purchases = from c in db.CusCards
                            join p in db.Products on c.TemplateId equals p.Id
     
                            select new PurchasesViewModel
                            {
                                ImageLink = p.Picture,
                                Name = p.Namee,
                                Description = p.Descp,
                                ProductId = p.Id,
                                // Extra info from card table (optional)
                                Message = c.Message,
                                FinalImagePath = c.FinalImagePath,
                                CreatedAt = c.CreatedAt
                            };

            if (!purchases.Any())
            {
                ViewData["HavePastOrders"] = false;
                return View();
            }

            ViewData["HavePastOrders"] = true;
            return View(purchases.ToList());
        }









    }
}
