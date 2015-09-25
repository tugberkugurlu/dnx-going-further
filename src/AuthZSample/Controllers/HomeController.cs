using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace AuthZSample.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Constants.WebsiteReadPolicy)]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize(Constants.WebsiteWritePolicy)]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
