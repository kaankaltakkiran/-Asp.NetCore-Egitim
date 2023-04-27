using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class BlogController1 : Controller
    {
        public IActionResult Article()
        {
            return View();
        }
    }
}
