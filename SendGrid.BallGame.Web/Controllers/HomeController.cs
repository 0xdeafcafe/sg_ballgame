using System.Web.Mvc;

namespace SendGrid.BallGame.Web.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Game()
		{
			return View();
		}
	}
}