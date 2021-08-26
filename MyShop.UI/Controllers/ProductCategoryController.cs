using System.Linq;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;

namespace MyShop.UI.Controllers
{
	public class ProductCategoryController : Controller
	{
		IRepository<ProductCategory> context;

		public ProductCategoryController(IRepository<ProductCategory> context) {
			this.context = context;
		}

		public ActionResult Index() => View(context.Collection().ToList());

		public ActionResult Create() => View(new ProductCategory());

		[HttpPost]
		public ActionResult Create(ProductCategory cat) {
			if (!ModelState.IsValid) {
				return View(cat);
			}
			context.Insert(cat);
			context.Commit();
			return RedirectToAction("Index");
		}

		public ActionResult Edit(string id) {
			var categoryFound = context.Find(id);
			if (categoryFound == null)
				return HttpNotFound();
			return View(categoryFound);
		}

		[HttpPost]
		public ActionResult Edit(ProductCategory category, string id) {
			var editedCategory = context.Find(id);
			if (editedCategory == null)
				return HttpNotFound();
			if (!ModelState.IsValid)
				return View(editedCategory);
			editedCategory.CategoryName = category.CategoryName;
			context.Commit();
			return RedirectToAction("Index");
		}

		public ActionResult Delete(string id) {
			var categoryFound = context.Find(id);
			if (categoryFound == null)
				return HttpNotFound();

			return View(categoryFound);
		}

		[HttpPost]
		[ActionName("Delete")]
		public ActionResult ConfirmDelete(string id) {
			var categoryToDelete = context.Find(id);
			if (categoryToDelete == null) {
				return HttpNotFound();
			}
			context.Delete(id);
			context.Commit();
			return RedirectToAction("Index");
		}
	}
}