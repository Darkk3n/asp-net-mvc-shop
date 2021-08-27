using System.Collections.Generic;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;

namespace MyShop.Core.Contracts
{
	public interface IOrderService
	{
		void CreateOrder(Order baseOrder, List<BasketItemViewModel> items);
		IEnumerable<Order> GetOrders();
		Order GetOrder(string id);
		void UpdateOrder(Order order);
	}
}