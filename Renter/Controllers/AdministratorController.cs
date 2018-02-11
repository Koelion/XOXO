using Microsoft.AspNetCore.Mvc;

namespace Renter.Controllers
{
    public class AdministratorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}