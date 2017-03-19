using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.DAL;
using TeamLeasing.Models;

namespace TeamLeasing.Controllers
{
    public class ContactController : Controller
    {
        private TeamLeasingContext _context;

        public ContactController(TeamLeasingContext context)
        {
            _context = context;
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
            return View("Contact");
        }

    }
}