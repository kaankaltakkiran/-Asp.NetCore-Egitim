using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace MyAspNetCoreApp.Web.Filters
{
    public class LogFilter:ActionFilterAttribute
    {
        //Action Filter
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Debug.WriteLine("Action Methot çalışmadan önce");
        }
        //Action Filter
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine("Action Methot çalıştıktan sonra");
        }
        //Result Filter
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Debug.WriteLine("Action Methot sonuç üretilmeden önce");
        }        
        //Result Filter
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Debug.WriteLine("Action Methot sonuç üretildikten sonra");
        }
       
    }
}
