using Microsoft.AspNetCore.Mvc;
using TestKSK.Services;

namespace TestKSK.Controllers
{
    public class AdminController : Controller
    {
        public async Task<ActionResult> Index()
        {
            return View();
        }
    }
}
