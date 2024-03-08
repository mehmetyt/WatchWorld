using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Interfaces;
using Web.Models;

namespace Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketViewModelService _basketViewModelService;

        public BasketController(IBasketViewModelService basketViewModelService)
        {
            _basketViewModelService = basketViewModelService;
        }
		public async Task<IActionResult> Index()
		{
			var basket = await _basketViewModelService.GetBasketViewModelAsync();
			return View(basket);
		}
        [HttpPost,ValidateAntiForgeryToken]
		public async Task<IActionResult> Empty()
		{
            await _basketViewModelService.EmptyBasketAsync();
            TempData["Message"] = "Your basket is now empty.";
			return RedirectToAction("Index");
		}


        [HttpPost]
        public async Task<IActionResult> AddItem(int productId, int quantity = 1)
        {
            var basket=await _basketViewModelService.AddItemToBasketAsync(productId, quantity);
            return Json(basket);
        }


	}
}
