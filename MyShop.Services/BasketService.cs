using System;
using System.Linq;
using System.Web;
using MyShop.Core.Contracts;
using MyShop.Core.Extensions;
using MyShop.Core.Models;

namespace MyShop.Services
{
	public class BasketService
	{
		IRepository<Product> productRepository;
		IRepository<Basket> basketRepository;

		public const string BasketSessionName = "eCommerceBasket";

		public BasketService(IRepository<Product> ProductRepository, IRepository<Basket> BasketRepository) {
			this.productRepository = ProductRepository;
			this.basketRepository = BasketRepository;
		}

		private Basket GetBasket(HttpContextBase httpContext, bool createIfNull) {
			var cookie = httpContext.Request.Cookies.Get(BasketSessionName);
			var basket = new Basket();
			if (cookie != null) {
				string basketId = cookie.Value;
				if (basketId.HasValue()) {
					basket = basketRepository.Find(basketId);
				}
				else {
					if (createIfNull) {
						basket = CreateBasket(httpContext);
					}
				}
			}
			else {
				if (createIfNull) {
					basket = CreateBasket(httpContext);
				}
			}
			return basket;
		}

		private Basket CreateBasket(HttpContextBase httpContext) {
			var basket = new Basket();
			basketRepository.Insert(basket);
			basketRepository.Commit();

			var cookie = new HttpCookie(BasketSessionName) {
				Value = basket.Id,
				Expires = DateTime.Now.AddDays(30)
			};

			httpContext.Response.Cookies.Add(cookie);
			return basket;
		}

		public void AddToBasket(HttpContextBase httpContext, string productId) {
			var basket = GetBasket(httpContext, true);
			var item = basket.BasketItems.FirstOrDefault(r => r.ProductId == productId);
			if (item == null) {
				item = new BasketItem() {
					BasketId = basket.Id,
					ProductId = productId,
					Quantity = 1
				};
				basket.BasketItems.Add(item);
			}
			else {
				item.Quantity++;
			}
			basketRepository.Commit();
		}

		public void RemoveFromBasket(HttpContextBase httpContext, string itemId) {
			var basket = GetBasket(httpContext, true);
			var item = basket.BasketItems.FirstOrDefault(r => r.Id == itemId);
			if (item == null) {
				basket.BasketItems.Remove(item);
				basketRepository.Commit();
			}
		}
	}
}