
using Microsoft.AspNetCore.Mvc;

namespace LocacaoVeiculos.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
