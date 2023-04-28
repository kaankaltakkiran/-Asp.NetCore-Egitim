using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Filters
{
    public class CustomExceptionFilter:ExceptionFilterAttribute
    {
        //Hata Yakalama
        public override void OnException(ExceptionContext context)
        {
            //Mesaj yakala
            context.ExceptionHandled = true;
            var error = context.Exception.Message;

            //Error sayfasına yönlendirme
            context.Result = new RedirectToActionResult("Error", "Home", new ErrorViewModel() { Errors = new List<string>() { $"{error}" } });
        }
    }
}
