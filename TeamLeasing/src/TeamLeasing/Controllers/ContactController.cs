using Microsoft.AspNetCore.Mvc;

namespace TeamLeasing.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult GetContact()
        {
            return View("Contact");

        }

    }
}