using System.Collections.Generic;
using System.Web;
using MyShop.Core.ViewModels;

namespace MyShop.Core.Contracts
{
	public interface IBasketService
	{
		void AddToBasket(HttpContextBase httpContext, string productId);
		List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext);
		BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext);
		void RemoveFromBasket(HttpContextBase httpContext, string itemId);
		void ClearBasket(HttpContextBase httpContext);
	}
}