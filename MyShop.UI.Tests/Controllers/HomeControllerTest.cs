using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.UI.Controllers;
using MyShop.UI.Tests.Mocks;

namespace MyShop.UI.Tests.Controllers
{
	[TestClass]
	public class HomeControllerTest
	{
		IRepository<Product> productContext;
		IRepository<ProductCategory> categoryContext;

		[TestInitialize]
		public void Init() {
			productContext = new MockContext<Product>();
			categoryContext = new MockContext<ProductCategory>();

			productContext.Insert(new Product());
		}

		[TestMethod]
		public void IndexPageDoesReturnProducts() {
			// Arrange
			var controller = new HomeController(productContext, categoryContext);

			// Act
			var result = controller.Index() as ViewResult;
			var viewModel = (ProductListViewModel)result.ViewData.Model;
			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(viewModel);
			Assert.AreEqual(1, viewModel.Products.Count());
		}
	}
}