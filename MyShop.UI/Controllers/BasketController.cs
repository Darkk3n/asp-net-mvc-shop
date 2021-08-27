using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;

namespace MyShop.UI.Controllers
{
	public class BasketController : Controller
	{
		IBasketService basketService;
		IOrderService orderService;

		public BasketController(IBasketService basketService, IOrderService orderService) {
			this.basketService = basketService;
			this.orderService = orderService;
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

		public ActionResult Checkout() {
			return View();
		}

		[HttpPost]
		public ActionResult Checkout(Order order) {
			var items = basketService.GetBasketItems(this.HttpContext);
			order.OrderStatus = "Order Created";

			//Process payment

			order.OrderStatus = "Payment Processed";
			orderService.CreateOrder(order, items);
			basketService.ClearBasket(this.HttpContext);
			return RedirectToAction("Thankyou", new { OrderId = order.Id });
		}

		public ActionResult Thankyou(string orderId) {
			ViewBag.OrderId = orderId;
			return View();
		}
	}
}