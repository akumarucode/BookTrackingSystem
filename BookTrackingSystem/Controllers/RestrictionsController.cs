using Microsoft.AspNetCore.Mvc;

namespace BookTrackingSystem.Controllers
{
    public class RestrictionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //restriction if not logged in
        public ActionResult Restricted()
        {
            TempData["alertMessage"] = "Please login!";
            return RedirectToAction("Index", "Home");
        }
    }
}
