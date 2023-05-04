using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.VisualBasic;
using MyAspNetCoreApp.Web.Filters;
using MyAspNetCoreApp.Web.Helpers;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;
using System.Collections.Generic;

namespace MyAspNetCoreApp.Web.Controllers
{


	public class ProductsController : Controller
	{
		//
        private AppDbContext _context;
        //File Upload
        private readonly IFileProvider _fileProvider;

        //
        private readonly IMapper _mapper;

        //Kulancığımız işlemleri olan yeri ekledik
        private readonly ProductRepository _productRepository;

		//Constructor

	
		public ProductsController(AppDbContext context, IMapper mapper, IFileProvider fileProvider)
		{
			//DI container
			//Dependency Injection Pattern
			_productRepository = new ProductRepository();
			//
            _context = context;
			//
            _mapper = mapper;
            _fileProvider = fileProvider;
            //Veritabına veri ekle
            //Her Seferinde Kaydetme
            //Data Yoksa kaydet
            //if(!_context.Products.Any()) {
            //             _context.Products.Add(new Product() { Name = "Kalem 1", Price = 100, Stock = 100,Color="Red"});
            //             _context.Products.Add(new Product() { Name = "Kalem 2", Price = 100, Stock = 200, Color = "Blue"});
            //             _context.Products.Add(new Product() { Name = "Kalem 3", Price = 100, Stock = 300, Color = "Yellow"});

            //	//Veritabına Kaydet	
            //	_context.SaveChanges();
            //         }




            //Data Yoksa Bu iŞlemi YAP
            if (!_productRepository.GetAll().Any())
			{
			
			}
		}
        //[CacheResourceFilter]
		public IActionResult Index()
		{
            //category alma
            List<ProductViewModel> products = _context.Products.Include(x => x.Category).Select(x => new ProductViewModel()
            {
                //x=prodcut
                Id= x.Id,
                Name= x.Name,
                Price= x.Price,
                Stock= x.Stock,
                CategoryName=x.Category.Name,
                Description= x.Description,
                Expire= x.Expire,
                ImagePath=x.ImagePath,
                IsPublish=x.IsPublish,
                PublishDate=x.PublishDate

            }).ToList();

            //Veritabınından tüm produsctsları getir
            //Context üzerinden
            //var products = _context.Products.ToList();

            //Mapledik
            //return View(_mapper.Map<List<ProductViewModel>>(products));
            return View(products);
        }
        [Route("[controller]/[action]/{page}/{pageSize}",Name = "productpage")]
        //action=Pages
        public IActionResult Pages(int page,int pageSize)
        {
            //page=1 pagesize=3 ilk 3 kayıt
            //page= pagesize=3 ikinci 3 kayıt

            //Skip kaçıcından başlama
            //Take Ne kadar alcağı
            var products = _context.Products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.page=page;
            ViewBag.PageSize = pageSize;    
            return View(_mapper.Map<List<ProductViewModel>>(products));
        }
        [ServiceFilter(typeof(NotFoundFilter))]
        //Attribute Routing
        [Route("[controller]/[action]/{productid}",Name ="product")]
        //action=GetById
        //controller olamasada olur 
        public IActionResult GetById(int productid)
        {
            var product = _context.Products.Find(productid);
            return View(_mapper.Map<ProductViewModel>(product));
        }
		public IActionResult Remove(int id) {
            //Primary key olan id al
            //_context.Products veritabanı demek
            var product = _context.Products.Find(id);
			//
			_context.Products.Remove(product);
            //Veritabına Kaydet	
            _context.SaveChanges();
            return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult Add() {

			//radio ile ürün süresi
			ViewBag.Expire = new Dictionary<string, int>()
			{
				{"1Ay",1 },
				{"3Ay",3 },
				{"6Ay",6 },
				{"12Ay",12 }
			};
			//dropdown ile listeleme işlemi

			ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
			{
				new(){Data="Mavi",Value="Mavi"},
				new(){Data="Kırmızı",Value="Kırmızı"},
				new(){Data="Sarı",Value="Sarı"}
				//value göster sonra dataları
			}, "Value", "Data");
            //Category Dropdwonlist
            var categories=_context.Category.ToList();
            //İd tutup Name gözükcek
            ViewBag.categorySelect=new SelectList(categories,"Id","Name");

			

		return View();
		}
		//reqden verileri alma
		[HttpPost]
		//Veri Kaydetme
		public  IActionResult Add(ProductViewModel newProduct)
		{
            //model dışı hata mesajı
            //if (!newProduct.Name.StartsWith("A"))

            //         {
            //	ModelState.AddModelError(string.Empty, "A harfli ile başlamalıdır");
            //}
            //Alanlar dolu gelsin diye eklendi

            //radio ile ürün süresi
         

            //Model başarıylıysa kaydet
            if (ModelState.IsValid)
			{
				try
                {
                    var product = _mapper.Map<Product>(newProduct);
                    if (newProduct.Image!=null && newProduct.Image.Length > 0)
                    {
                        var root = _fileProvider.GetDirectoryContents("wwwroot");
                        var images = root.First(x => x.Name == "images");

                        var randomImageName = Guid.NewGuid() + Path.GetExtension(newProduct.Image.FileName);

                        var path = Path.Combine(images.PhysicalPath, randomImageName);

                        using var stream = new FileStream(path, FileMode.Create);

                        newProduct.Image.CopyTo(stream);
                       
                        product.ImagePath = randomImageName;
                    }
                   

                    //veri tabanına ekleme
                    //Mapleme ile 
                    _context.Products.Add(product);
                    //veri tabanına kaydetme
                    _context.SaveChanges();
                    //Mesaj ile kullanıcı bilgilendirme
                    TempData["status"] = "Ürün Başarıyla Eklendi.";
                    //post işlemi sonrası index sayfasına git
                    return RedirectToAction("Index");
                }
				catch(Exception) {
					//hata olursa
					ModelState.AddModelError(string.Empty,"Ürün Kaydedirken hata meydana geldi");
                    ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1Ay",1 },
                {"3Ay",3 },
                {"6Ay",6 },
                {"12Ay",12 }
            };
                    //dropdown ile listeleme işlemi

                    ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new(){Data="Mavi",Value="Mavi"},
                new(){Data="Kırmızı",Value="Kırmızı"},
                new(){Data="Sarı",Value="Sarı"}
				//value göster sonra dataları
			}, "Value", "Data");
                    return View();
                }
            
            }

			else {
                //Category Dropdwonlist
                var categories = _context.Category.ToList();
                //İd tutup Name gözükcek
                ViewBag.categorySelect = new SelectList(categories, "Id", "Name");

                ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1Ay",1 },
                {"3Ay",3 },
                {"6Ay",6 },
                {"12Ay",12 }
            };
                //dropdown ile listeleme işlemi

                ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new(){Data="Mavi",Value="Mavi"},
                new(){Data="Kırmızı",Value="Kırmızı"},
                new(){Data="Sarı",Value="Sarı"}
				//value göster sonra dataları
			}, "Value", "Data");
                return View();
			}

		
        }
        [HttpGet]
        public IActionResult Update(int id) { 

			//İdleri bul ve product a getir
			var product= _context.Products.Find(id);
            //Category Dropdwonlist
            var categories = _context.Category.ToList();
            //İd tutup Name gözükcek
            ViewBag.categorySelect = new SelectList(categories, "Id", "Name",product.CategoryId);


            //radio ile ürün süresi

            //Seçilen Değeri alma
            ViewBag.ExpireValue = product.Expire;

            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1Ay",1 },
                {"3Ay",3 },
                {"6Ay",6 },
                {"12Ay",12 }
            };
            //dropdown ile listeleme işlemi


            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new(){Data="Mavi",Value="Mavi"},
                new(){Data="Kırmızı",Value="Kırmızı"},
                new(){Data="Sarı",Value="Sarı"}
				//value göster sonra dataları
			}, "Value", "Data",product.Color);


            //prdoucta yolla
            return View(_mapper.Map<ProductUpdateViewModel>(product));
		}
        //Veri Günceleme
        [HttpPost]
		public IActionResult Update(ProductUpdateViewModel updateProduct)
		{
            if(!ModelState.IsValid)
            {
               

                //radio ile ürün süresi

                //Seçilen Değeri alma
                ViewBag.ExpireValue = updateProduct.Expire;

                ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1Ay",1 },
                {"3Ay",3 },
                {"6Ay",6 },
                {"12Ay",12 }
            };
                //dropdown ile listeleme işlemi


                ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new(){Data="Mavi",Value="Mavi"},
                new(){Data="Kırmızı",Value="Kırmızı"},
                new(){Data="Sarı",Value="Sarı"}
				//value göster sonra dataları
			}, "Value", "Data", updateProduct.Color);

                //Category Dropdwonlist
                var categories = _context.Category.ToList();
                //İd tutup Name gözükcek
                ViewBag.categorySelect = new SelectList(categories, "Id", "Name",updateProduct.CategoryId);

                return View();
            }

            if (updateProduct.Image != null && updateProduct.Image.Length > 0)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot");
                var images = root.First(x => x.Name == "images");

                var randomImageName = Guid.NewGuid() + Path.GetExtension(updateProduct.Image.FileName);

                var path = Path.Combine(images.PhysicalPath, randomImageName);

                using var stream = new FileStream(path, FileMode.Create);

                updateProduct.Image.CopyTo(stream);

                updateProduct.ImagePath = randomImageName;
            }








            //veri tabanına güncelleme
            _context.Products.Update(_mapper.Map<Product>(updateProduct));
            //veri tabanına kaydetme
            _context.SaveChanges();
			//Mesaj ile kullanıcı bilgilendirme
			TempData["status"] = "Ürün Başarıyla Güncellendi.";
			//Update sonrası ındex sayfasına git
			return RedirectToAction("Index");
		}

        //İsim veritabında var mı ?
        //Get veya post isteği olabilir
        [AcceptVerbs("Get","Post")]
        public IActionResult HasProductName(string Name)
        {
            var anyProduct=_context.Products.Any(x => x.Name.ToLower() == Name.ToLower());
            if (anyProduct)
            {
                return Json("Ürün İsmi Veritabanında Bulunmaktadır");
            }
            else
            {
                return Json(true);
            }
        }
    }
}
