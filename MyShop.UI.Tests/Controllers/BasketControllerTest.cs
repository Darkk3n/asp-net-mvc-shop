using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Services;
using MyShop.UI.Tests.Mocks;

namespace MyShop.UI.Tests.Controllers
{
	[TestClass]
	public class BasketControllerTest
	{
		IRepository<Basket> basketRepo;
		IRepository<Product> productRepo;
		IBasketService service;
		MockHttpContext mockHttpContext;

		[TestInitialize]
		public void Init() {
			basketRepo = new MockContext<Basket>();
			productRepo = new MockContext<Product>();
			service = new BasketService(productRepo, basketRepo);
			mockHttpContext = new MockHttpContext();
		}

		[TestMethod]
		public void CanAddBasketItem() {

			service.AddToBasket(mockHttpContext, "1");

			var basket = basketRepo.Collection().FirstOrDefault();

			Assert.IsNotNull(basket);
			Assert.AreEqual(1, basket.BasketItems.Count);
			Assert.AreEqual("1", basket.BasketItems.FirstOrDefault().ProductId);
		}
	}
}