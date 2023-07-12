using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestKSK.Interfaces;
using TestKSK.Models.Requests;

namespace TestKSK.Controllers
{
    public class VendingMachineController : Controller
    {
        private readonly IVendingMachineService vendingMachineService;

        public VendingMachineController(IVendingMachineService vendingMachineService)
        {
            this.vendingMachineService = vendingMachineService;
        }

        public async Task<ActionResult> Index()
        {
            var machineModel = await vendingMachineService.GetVendingMachineModel();
            return View(machineModel);
        }

        [HttpPost("update-balance")]
        public async Task<ActionResult> UpdateBalance([FromBody] UpdateBalanceRequest request)
        {
            var result = await vendingMachineService.UpdateUserBalance(request);
            if (result.Succeeded)
                return Ok();
            return BadRequest(result.ErrorMsg);
        }

        [HttpPost("buy-beverage")]
        public async Task<ActionResult> BuyBeverage(BeverageBuyRequest request)
        {
            var result = await vendingMachineService.BuyBeverage(request);
            if (result.Succeeded)
                return Ok($"Ваша сдача: {result.Change} ₽");
            return BadRequest(result.ErrorMsg);
        }
    }
}
