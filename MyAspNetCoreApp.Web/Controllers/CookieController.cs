using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class CookieController : Controller
    {
        public IActionResult CookieCreate()
        {
            //Cookie ekleme
            HttpContext.Response.Cookies.Append("background-color", "black",new CookieOptions() { Expires = DateTime.Now.AddDays(2), });
            return View();
        }
        public IActionResult CookieRead() {
            //Cookie Okuma
            var bgcolor=HttpContext.Request.Cookies["background-color"];
            ViewBag.BackColor = bgcolor;
            return View();
        }
    }
}
