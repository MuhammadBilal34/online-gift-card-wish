using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mytheme.Models;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace mytheme.Controllers
{
    public class Client : Controller
    {

        StudentdbContext db = new StudentdbContext();
        public IActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public IActionResult Contactus()
        {
            return View();
        }
        public IActionResult showproducts(int Id)
        {

            var resultt = db.Products.Where(x => x.CatId.Equals(Id));
            return View(resultt);
        }
        [HttpGet]
        public IActionResult Create(int Id)
        {
            var data = db.Products.Find(Id);

            ViewBag.Template = data;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile userImage, string message, int templateId, string finalImageData,string email)
        {
            if (string.IsNullOrEmpty(finalImageData))
            {
                ModelState.AddModelError("", "Image generation failed.");
                return View();
            }

            try
            {
                var base64Data = Regex.Match(finalImageData, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                byte[] imageBytes = Convert.FromBase64String(base64Data);

                string fileName = Guid.NewGuid().ToString() + ".png";
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string filePath = Path.Combine(folderPath, fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);
                String abc = User.FindFirst(ClaimTypes.Sid)?.Value;

                var sender = db.Logins.FirstOrDefault(x => x.Id == Convert.ToInt32(abc));
                var card = new CusCard
                {
                    TemplateId = templateId,
                    Message = message,
                    FinalImagePath = "/Images/" + fileName,
                    Userid = Convert.ToInt32(abc),
                    CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };

                db.CusCards.Add(card);
                await db.SaveChangesAsync();
                try
                {
                    var fromEmail = "mariakhanofficial78@gmail.com";
                    var fromPassword = "jbyhgaliiflyqbcs"; // Use App Password if using Gmail

                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                    {
                        EnableSsl = true,
                        Credentials = new NetworkCredential(fromEmail, fromPassword)
                    };

                    MailMessage msg = new MailMessage
                    {
                        From = new MailAddress(sender.Email),
                        Subject = "Your Customized E-Card",
                        Body = "Thank you for using our service! Find your customized card attached."
                    };

                    msg.To.Add(email); // Replace with actual customer email variable

                    // Attach the final image
                    string savedImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);
                    Attachment attachment = new Attachment(savedImagePath);
                    msg.Attachments.Add(attachment);

                    client.Send(msg);

                    ViewBag.message = "Mail sent successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.message = "Email send failed: " + ex.Message;
                }

                return RedirectToAction("showproducts");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred: " + ex.Message);
                return View();
            }
        }




        public IActionResult profile()
        {

            String abc = User.FindFirst(ClaimTypes.Sid)?.Value;
            int userId = Convert.ToInt32(abc); 

            var purchases = from c in db.CusCards
                            join p in db.Products on c.TemplateId equals p.Id
                            where c.Userid == userId
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
