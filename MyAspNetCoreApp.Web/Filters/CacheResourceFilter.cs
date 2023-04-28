using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyAspNetCoreApp.Web.Filters
{
    public class CacheResourceFilter : Attribute, IResourceFilter
    {
        private static IActionResult _cache;


        //Cevap üretildekten sonra
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _cache = context.Result;
        }
        //ilk çalışan
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if(_cache != null)
            {
                context.Result = _cache;
            }
        }
    }
}
