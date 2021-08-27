using System.Web.Mvc;

namespace MyShop.UI.Controllers
{
	[Authorize]
	public class AdminController : Controller
	{
		public ActionResult Index() {
			return View();
		}
	}
}       