using Microsoft.AspNetCore.Mvc;

namespace BookWorldStore.Controllers
{
	public class UserControllerController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
