using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestKSK.Services;

namespace TestKSK.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet("/admin")]
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View();
        }
    }
}
