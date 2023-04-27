using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Filters;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;
using System.Diagnostics;

namespace MyAspNetCoreApp.Web.Controllers
{
    [LogFilter]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //
        private readonly AppDbContext _context;
        //
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
			_context= context;
            _mapper= mapper;

		}

        public IActionResult Index()
        {
            //Datalrı Azalan sıralaama ve seçme
            var products = _context.Products.OrderByDescending(x => x.Id).Select(x => new ProductPartialViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock,
            }).ToList();
            //Viewbag ile taşıma
            ViewBag.ProductListPartialViewModel = new ProductListPartialViewModel()
            {
                Products = products
        };

			return View();
        }

        public IActionResult Privacy()
        {
			//Datalrı Azalan sıralaama ve seçme
			var products = _context.Products.OrderByDescending(x => x.Id).Select(x => new ProductPartialViewModel()
			{
				Id = x.Id,
				Name = x.Name,
				Price = x.Price,
				Stock = x.Stock,
			}).ToList();
			//Viewbag ile taşıma
			ViewBag.ProductListPartialViewModel = new ProductListPartialViewModel()
			{
				Products = products
			};
			return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel errorViewModel)
        {
            errorViewModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View(errorViewModel);
        }
        public IActionResult Visitor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveVisitorComment(VisitorViewModel visitorViewModel)
        {
            try
            {
				var visitor = _mapper.Map<Visitor>(visitorViewModel);
                //Tarih alma
               visitor.Created= DateTime.Now;
				//Ekle
				_context.Visitor.Add(visitor);
				//Kaydet
				_context.SaveChanges();
				//Mesaj taşıma
				TempData["result"] = "Yorum Kaydedildi";
				return RedirectToAction("Visitor");

			}
			catch (Exception)
            {
				//Mesaj taşıma
				TempData["result"] = "Yorum Kaydedilemedi.";
				return RedirectToAction("Visitor");
			}
       
		}
    }
}