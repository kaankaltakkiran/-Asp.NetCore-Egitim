using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class VisitorAjaxController : Controller
    {
        //
        private readonly IMapper _mapper;
        //
        private readonly AppDbContext _context;
        public VisitorAjaxController(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveVisitorComment(VisitorViewModel visitorViewModel)
        {

           
                var visitor = _mapper.Map<Visitor>(visitorViewModel);
                //Tarih alma
                visitor.Created = DateTime.Now;
                //Ekle
                _context.Visitor.Add(visitor);
                //Kaydet
                _context.SaveChanges();

            return Json(new { succes = "ture" });

        }
        //ajaxa istek
        [HttpGet]
        public IActionResult VisitorCommentList()
        {
            var visitors = _context.Visitor.OrderByDescending(x=>x.Created).ToList();

            var visitorViewModels = _mapper.Map<List<VisitorViewModel>>(visitors);

            return Json(visitorViewModels);
        }
    }
}

