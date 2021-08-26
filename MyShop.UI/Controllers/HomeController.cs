using System.Linq;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;

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

		public ActionResult Index() {
			var products = context.Collection().ToList();
			return View(products);
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