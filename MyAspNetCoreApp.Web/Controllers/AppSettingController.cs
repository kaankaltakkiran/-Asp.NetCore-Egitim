using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class AppSettingController : Controller
    {
        //Apssetingsten data okuma
        private readonly IConfiguration _configuration;
        public AppSettingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            //1. okuma yöntemi
            ViewBag.baseurl = _configuration["baseurl"];
            //2. okuma yöntemi
            ViewBag.smskey= _configuration["Keys:Sms"];
            //3. okuma yöntemi
            ViewBag.emailkey = _configuration.GetSection("Keys")["Email"];
            return View();
        }
    }
}
