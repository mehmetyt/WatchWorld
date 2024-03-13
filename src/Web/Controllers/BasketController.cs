using ApplicationCore.Entities;
using Microsoft.AspNetCore.Authorization;
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
		[HttpPost, ValidateAntiForgeryToken]
		public new async Task<IActionResult> Empty()
		{
			await _basketViewModelService.EmptyBasketAsync();
			TempData["Message"] = "Your basket is now empty.";
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> AddItem(int productId, int quantity = 1)
		{
			var basket = await _basketViewModelService.AddItemToBasketAsync(productId, quantity);
			return Json(basket);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> RemoveItem(int productId)
		{
			await _basketViewModelService.RemoveItemAsync(productId);
			TempData["Message"] = "Item Removed";
			return RedirectToAction("Index");
		}
		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Update([ModelBinder(Name = "quantities")] Dictionary<int, int> quantities)
		{
			await _basketViewModelService.SetQuantitiesAsync(quantities);
			TempData["Message"] = "Items Updated";
			return RedirectToAction("Index");
		}

		[Authorize]
		public async Task<IActionResult> Checkout()
		{
			var basketViewModel = await _basketViewModelService.GetBasketViewModelAsync();

			var checkoutViewModel = new CheckoutViewModel() { Basket = basketViewModel };

			return View(checkoutViewModel);
		}

		[Authorize, HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Checkout(CheckoutViewModel checkoutViewModel)
		{

			if (ModelState.IsValid)
			{
				await _basketViewModelService.CheckoutAsync(checkoutViewModel.Street, checkoutViewModel.City, checkoutViewModel.State, checkoutViewModel.Country, checkoutViewModel.ZipCode);
				return RedirectToAction("OrderConfirmed");
			}

			var basket = await _basketViewModelService.GetBasketViewModelAsync();
			checkoutViewModel.Basket = basket;
			return View(checkoutViewModel);
		}

		[Authorize]
		public async Task<IActionResult> OrderConfirmed()
		{
			return View();
		}




	}
}
