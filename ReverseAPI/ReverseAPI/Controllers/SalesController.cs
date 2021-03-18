using Microsoft.AspNetCore.Mvc;

namespace ReverseAPI.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
