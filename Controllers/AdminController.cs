using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestKSK.Interfaces;
using TestKSK.Models;
using TestKSK.Models.Requests;
using TestKSK.Services;

namespace TestKSK.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        private readonly IVendingMachineService vendingMachineService;

        public AdminController(IVendingMachineService vendingMachineService)
        {
            this.vendingMachineService = vendingMachineService;
        }

        [HttpGet("/admin")]
        public async Task<ActionResult> Index()
        {
            var adminModel = await vendingMachineService.GetAdminModel();
            return View(adminModel);
        }

        [HttpGet("/admin/add-beverage")]
        public async Task<ActionResult> AddBeverage()
        {
            return View();
        }

        [HttpPost("/admin/add-beverage")]
        public async Task<ActionResult> AddBeverage(BeverageModel model)
        {
            var result = await vendingMachineService.AddBeverage(model);
            if (result.Succeeded)
                return Ok();
            return BadRequest(result.ErrorMsg);
        }

        [HttpGet("/admin/edit-beverage/{id}")]
        public async Task<ActionResult> EditBeverage([FromRoute] Guid Id)
        {
            var beverage = await vendingMachineService.GetBeverageModel(Id);
            return View(beverage);
        }

        [HttpPost("/admin/edit-beverage/{id}")]
        public async Task<ActionResult> EditBeverage(BeverageModel model)
        {
            var result = await vendingMachineService.EditBeverage(model);
            if (result.Succeeded)
                return Ok();
            return BadRequest(result.ErrorMsg);
        }

        [HttpPost("update-unit-availability")]
        public async Task<ActionResult> ChangeMoneyUnitAvailability(
            [FromBody] UpdateMoneyUnitAvailabilityRequest request)
        {
            var result = await vendingMachineService
                .SwitchMoneyUnitAvailability(request);
            if (result.Succeeded)
                return Ok();
            return BadRequest(result.ErrorMsg);
        }
    }
}
