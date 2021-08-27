using System.Collections.Generic;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;

namespace MyShop.Services
{
	public class OrderService : IOrderService
	{
		IRepository<Order> orderRepo;

		public OrderService(IRepository<Order> orderRepo) {
			this.orderRepo = orderRepo;
		}

		public void CreateOrder(Order baseOrder, List<BasketItemViewModel> items) {
			foreach (var item in items) {
				baseOrder.OrderItems.Add(new OrderItem {
					ProductId = item.Id,
					Image = item.Image,
					Price = item.Price,
					ProductName = item.ProductName,
					Quantity = item.Quantity
				});
			}

			orderRepo.Insert(baseOrder);
			orderRepo.Commit();
		}
	}
}