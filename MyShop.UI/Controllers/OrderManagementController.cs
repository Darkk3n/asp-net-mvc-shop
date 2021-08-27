using System.Collections.Generic;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;

namespace MyShop.UI.Controllers
{
	public class OrderManagementController : Controller
	{
		IOrderService orderService;

		public OrderManagementController(IOrderService orderService) {
			this.orderService = orderService;
		}

		public ActionResult Index() {
			var orders = orderService.GetOrders();
			return View(orders);
		}

		public ActionResult UpdateOrder(string id) {
			ViewBag.StatusList = new List<string> {
				"Order Created",
				"Payment Processed",
				"Order Shipped",
				"Order Complete"
			};
			return View(orderService.GetOrder(id));
		}

		[HttpPost]
		public ActionResult UpdateOrder(Order updatedOrder, string id) {
			var order = orderService.GetOrder(id);

			order.OrderStatus = updatedOrder.OrderStatus;
			orderService.UpdateOrder(order);

			return RedirectToAction("Index");
		}
	}
}