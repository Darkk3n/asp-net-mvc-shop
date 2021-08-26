using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;

namespace MyShop.UI.Controllers
{
	public class ProductController : Controller
	{
		IRepository<Product> context;
		IRepository<ProductCategory> categoryContext;

		public ProductController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext) {
			context = productContext;
			categoryContext = productCategoryContext;
		}

		public ActionResult Index() => View(context.Collection().ToList());

		public ActionResult Create() {
			var viewModel = new ProductManagerViewModel();
			viewModel.Product = new Product();
			viewModel.Categories = categoryContext.Collection();
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Create(ProductManagerViewModel prod, HttpPostedFileBase file) {
			if (!ModelState.IsValid)
				return View(prod);
			if (file != null) {
				prod.Product.Image = $"{prod.Product.Id}{Path.GetExtension(file.FileName)}";
				file.SaveAs(Server.MapPath($"~//Content//ProductImages//{prod.Product.Image}"));
			}
			context.Insert(prod.Product);
			context.Commit();
			return RedirectToAction("Index");
		}

		public ActionResult Edit(string id) {
			var prodFound = context.Find(id);
			if (prodFound == null)
				return HttpNotFound();
			var productViewModel = new ProductManagerViewModel {
				Product = prodFound,
				Categories = categoryContext.Collection()
			};
			return View(productViewModel);
		}

		[HttpPost]
		public ActionResult Edit(ProductManagerViewModel prod, string id, HttpPostedFileBase file) {
			var editedProd = context.Find(id);
			if (editedProd == null)
				return HttpNotFound();

			if (!ModelState.IsValid)
				return View(editedProd);

			if (file != null) {
				editedProd.Image = $"{prod.Product.Id}{Path.GetExtension(file.FileName)}";
				file.SaveAs(Server.MapPath($"~//Content//ProductImages//{prod.Product.Image}"));
			}
			editedProd.Name = prod.Product.Name;
			editedProd.Category = prod.Product.Category;
			editedProd.Price = prod.Product.Price;
			editedProd.Name = prod.Product.Name;
			context.Commit();
			return RedirectToAction("Index");
		}

		public ActionResult Delete(string id) {
			var prodFound = context.Find(id);
			if (prodFound == null)
				return HttpNotFound();

			return View(prodFound);
		}

		[HttpPost]
		[ActionName("Delete")]
		public ActionResult ConfirmDelete(string id) {
			var productToDelete = context.Find(id);
			if (productToDelete == null)
				return HttpNotFound();
			context.Delete(id);
			context.Commit();
			return RedirectToAction("Index");
		}
	}
}