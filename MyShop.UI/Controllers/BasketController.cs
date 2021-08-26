using System.Web.Mvc;
using MyShop.Core.Contracts;

namespace MyShop.UI.Controllers
{
	public class BasketController : Controller
	{
		IBasketService basketService;

		public BasketController(IBasketService basketService) {
			this.basketService = basketService;
		}

		public ActionResult Index() => View(basketService.GetBasketItems(this.HttpContext));

		public ActionResult AddToBasket(string productId) {
			basketService.AddToBasket(this.HttpContext, productId);
			return RedirectToAction("Index");
		}

		public ActionResult RemoveFromBasket(string basketItemId) {
			basketService.RemoveFromBasket(this.HttpContext, basketItemId);
			return RedirectToAction("Index");
		}

		public PartialViewResult BasketSummary() => PartialView(basketService.GetBasketSummary(this.HttpContext));
	}
}