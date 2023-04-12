using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Views.Shared.ViewComponents
{
	public class ProductListViewComponent:ViewComponent
	{
		//db için
		private readonly AppDbContext _context;
		//Constructor
		public ProductListViewComponent(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync(int type=1)
		{
			//Modeli Alma
			var viewmodels = _context.Products.Select(x => new ProductListComponentViewModel()
			{
				Name = x.Name,
				Description = x.Description
			}).ToList();
			if(type== 1)
			{
				//Model Dönme
				return View("Default",viewmodels);
			}
			else
			{
				return View("Type2",viewmodels);
			}
		
		}
	}
}
