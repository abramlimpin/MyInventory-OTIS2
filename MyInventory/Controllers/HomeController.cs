using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyInventory.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using System.Net;
using System.Net.Mail;

namespace MyInventory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(Contact record)
        {
            using (MailMessage mail =
                new MailMessage("benilde.web.development@gmail.com", record.Email))
            { 
                mail.Subject = "Inquiry from " + record.Sender + " (" + record.Subject  + ")";
                string message = "Hello, " + record.Sender + "<br/><br/>" +
                    "We have received your inquiry. Here are the details: <br/><br/>" +
                    "Contact Number: " + record.ContactNo + "<br/>" +
                    "Message:<br>" + record.Message + "<br/><br/>" +
                    "Please wait for our reply. Thank you. 🐼";

                mail.Body = message;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential credential = new NetworkCredential(
                        "benilde.web.development@gmail.com", "Athisisalongpassword1234");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = credential;
                    smtp.Port = 465;
                    smtp.Send(mail);
                    ViewBag.Message = "Inquiry sent.";
                }
            }

            return View();
        }
    }
}
