using Microsoft.AspNetCore.Mvc;

namespace TeamLeasing.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}