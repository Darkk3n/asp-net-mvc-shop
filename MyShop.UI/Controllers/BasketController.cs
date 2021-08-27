using System.Linq;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;

namespace MyShop.UI.Controllers
{
	public class BasketController : Controller
	{
		IBasketService basketService;
		IOrderService orderService;
		IRepository<Customer> customerRepo;

		public BasketController(IBasketService basketService, IOrderService orderService, IRepository<Customer> customerRepo) {
			this.basketService = basketService;
			this.orderService = orderService;
			this.customerRepo = customerRepo;
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

		[Authorize]
		public ActionResult Checkout() {
			var cust = customerRepo.Collection().FirstOrDefault(r => r.Email == User.Identity.Name);
			if (cust != null) {
				var order = new Order {
					Email = cust.Email,
					City = cust.City,
					State = cust.State,
					Street = cust.Street,
					FirstName = cust.FirstName,
					SurName = cust.LastName,
				};
				return View(order);
			}

			return RedirectToAction("Error");
		}

		[HttpPost]
		[Authorize]
		public ActionResult Checkout(Order order) {
			var items = basketService.GetBasketItems(this.HttpContext);
			order.OrderStatus = "Order Created";
			order.Email = User.Identity.Name;

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