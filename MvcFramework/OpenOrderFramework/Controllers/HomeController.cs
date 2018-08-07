using System.Web.Mvc;

namespace OpenOrderFramework.Controllers {
    [RequireHttps]
    public class HomeController : Controller {
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult About() {
            ViewBag.Message = "Application Description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
