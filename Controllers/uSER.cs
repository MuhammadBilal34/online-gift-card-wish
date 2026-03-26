using Microsoft.AspNetCore.Mvc;
using mytheme.Models;

namespace mytheme.Controllers
{
    public class uSER : Controller
    {
        StudentdbContext db = new StudentdbContext();
        public IActionResult Index()
        {
            return View(db.Products.ToList());
        }
        

        public IActionResult Contact()
        {
            return View();
        }
    }
}
