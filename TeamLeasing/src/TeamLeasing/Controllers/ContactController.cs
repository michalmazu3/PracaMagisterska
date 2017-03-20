using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.DAL;
using TeamLeasing.Models;
using TeamLeasing.Services;

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
        public IActionResult Contact()
        {
            return View("Contact");

        }
        [HttpPost]
        public IActionResult Contact(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
            _sendEmail.EmailMessage = _sendEmail.CreateMessage(message.Email, message.Content);
            _sendEmail.Send(_sendEmail.EmailMessage);
            return View("Contact");
        }

    }
}