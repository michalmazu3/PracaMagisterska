using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TeamLeasing.DAL;
using TeamLeasing.Models;
using TeamLeasing.Services;
using TeamLeasing.Services.Mail;

namespace TeamLeasing.Controllers
{
    public class ContactController : Controller
    {
        private TeamLeasingContext _context;
        private readonly ISendEmail _sendEmail;

        public ContactController(TeamLeasingContext context, ISendEmail sendEmail)
        {
            _context = context;
            _sendEmail = sendEmail;
        }
        public IActionResult Index()
        {
            return View("Contact");

        }

        [HttpGet]
        public IActionResult SendMessage()
        {
           // var s = ViewBag.Name.ToString();
            return View("SendMessage");
        }

        [HttpPost]
        public IActionResult SendMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                _context.Messages.Add(message);
                _context.SaveChanges();
                _sendEmail.EmailMessage = _sendEmail.CreateMessage(message.Email, message.Content);
                _sendEmail.Send(_sendEmail.EmailMessage);
                ViewBag.Name = message.Name;
                return RedirectToAction("SendMessage");
            }
            else
            {
                return View("Contact", message);
            }

           
        }

    }
}