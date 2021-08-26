using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;

namespace MyShop.UI.Controllers
{
	public class HomeController : Controller
	{
		IRepository<Product> context;
		IRepository<ProductCategory> categoryContext;

		public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext) {
			context = productContext;
			categoryContext = productCategoryContext;
		}

		public ActionResult Index(string category = "") {
			var products = new List<Product>();
			if (!string.IsNullOrEmpty(category)) {
				products.AddRange(context.Collection().Where(r => r.Category == category).ToList());
			}
			else {
				products.AddRange(context.Collection().ToList());
			}
			var model = new ProductListViewModel {
				Products = products,
				Categories = categoryContext.Collection().ToList()
			};
			return View(model);
		}

		public ActionResult Details(string id) {
			var product = context.Find(id);
			if (product == null)
				return HttpNotFound();
			return View(product);
		}

		public ActionResult About() {
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact() {
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}