using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyAspNetCoreApp.Web.Models;
using System.Diagnostics;

namespace MyAspNetCoreApp.Web.Filters
{
    public class NotFoundFilter: ActionFilterAttribute
    {
        private readonly AppDbContext _context;

        public NotFoundFilter(AppDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //İd çekme
            //ActionArguments=parametre
            var idValue =context.ActionArguments.Values.First();
            //int çevir
            var id=(int)idValue;
            //any ture veya false döner eşitse true
            var hasProduct= _context.Products.Any(x=>x.Id==id);
            if (hasProduct==false)
            {
                //Error sayfasına yönlendirme
                context.Result = new RedirectToActionResult("Error", "Home",new ErrorViewModel() {Errors= new List<string>(){$"Id({id})'ye sahip ürün veritabında bulunamamıştır."} });
            }
        }
    }
}
