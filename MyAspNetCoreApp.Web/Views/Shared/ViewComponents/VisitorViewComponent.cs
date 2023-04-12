using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Views.Shared.ViewComponents
{
	public class VisitorViewComponent : ViewComponent
	{
		//Listleme için veritabanıdan veri çekme
		private readonly AppDbContext _context;

		private readonly IMapper _mapper;
		public  VisitorViewComponent(AppDbContext context, IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}

        public async Task<IViewComponentResult>InvokeAsync()
		{
			var visitors = _context.Visitor.ToList();

			var visitorViewModels = _mapper.Map<List<VisitorViewModel>>(visitors);
			ViewBag.visitorViewModels = visitorViewModels;
			return View();
		}
	}
}
