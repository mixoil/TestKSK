using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestKSK.Interfaces;
using TestKSK.Models;
using TestKSK.Services;

namespace TestKSK.Controllers
{
    public class AdminController : Controller
    {
        private readonly IVendingMachineService vendingMachineService;

        public AdminController(IVendingMachineService vendingMachineService)
        {
            this.vendingMachineService = vendingMachineService;
        }

        [HttpGet("/admin")]
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var adminModel = await vendingMachineService.GetAdminModel();
            return View(adminModel);
        }

        [HttpPost("/admin")]
        [Authorize]
        public async Task<ActionResult> Index(AdminPanelModel adminModel)
        {
            var result = await vendingMachineService.UpdateState(adminModel);
            if (result.Succeeded)
                return Ok();
            return BadRequest(result.ErrorMsg);
        }
    }
}
