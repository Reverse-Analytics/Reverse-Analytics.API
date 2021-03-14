using Microsoft.AspNetCore.Mvc;

namespace ReverseAPI.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
