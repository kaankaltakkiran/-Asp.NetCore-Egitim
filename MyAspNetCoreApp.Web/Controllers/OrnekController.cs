using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers
{
    //Model Yollama
    public class Product2
    {
        public int Id { get; set; }
        public string Ad { get; set; }
    }
    public class OrnekController : Controller
    {
        public IActionResult Index()
        {
            //Model İle Veri Taşıma
            var productList = new List<Product2>()
            {
                new() {Id=1,Ad="Kalem"},
                new() {Id=2,Ad="Defter"},
                new() {Id=3,Ad="Silgi"}
            };

            //ViewBag İle Veri Taşıma
            ViewBag.name = "Asp.Net Core";
            //ViewData İle Veri Taşıma
            ViewData["Age"] = 22;
            ViewData["Name"]=new List<string>() {"kaan","durdu" };
            //TempData İle Başka Sayafaya Veri Taşıma
            TempData["SurName"] = "Kaltakkiran";

            //Model İle Veri Yollama
            //Normalde Böyle return View();
            return View(productList);
        }
        public IActionResult Index3()
        {
            return View();
        }
        //Yeni Sayfa Oluşturma
        public IActionResult Index2()
        {
            //Normal Sayfa Dönme
            //return View();

            //İlgili Sayfaya İstek Gelirse(Index2) İstenen Sayfaya Yönlendirme(Index Sayfasına)
            //ControlName De Verebilirdik
            //return RedirectToAction("Index");
            return RedirectToAction("Index","Ornek");
        }
        //Parametre Yollama
        public IActionResult ParametreWiew(int id)
        {
            //3.Paramtetre Route Valuesi
            return RedirectToAction("JsonResultParametre", "Ornek", new {id=id});
        }
        public IActionResult JsonResultParametre(int id)
        {
            return Json(new { Id = id});
        }

        //ContentResult Oluşturma(String İfade Yollars)
        public IActionResult ContentResult()
        {
            return Content("ContentResult Oluşturuldu");
        }
        //JsonResult Oluşturma(Json Döner)
        public IActionResult JsonResult()
        {
            return Json(new {Id=1,name="Kaan",SurName="Kaltakkiran",Age=22 });
        }
        //EmptyResult(Hiçbir Şey Dönmez)
        public IActionResult EmptyResult()
        {
            return new EmptyResult();
        }

    }
}
