using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MyAspNetCoreApp.Web.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="İsim Alanı Zorunlu")]
        [StringLength(50,ErrorMessage ="50 karaktere kadar girebilirsiniz")]
        [Remote(action: "HasProductName",controller:"Products")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Fiyat Alanı Zorunlu")]
        [Range(1, 1000, ErrorMessage = "1-1000 arasında seçebilirsiniz")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})",ErrorMessage ="Fiyat 2 Basamaklı olmalıdır")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Stock  Alanı Zorunlu")]
        [Range(1,250,ErrorMessage = "1-250 arasında seçebilirsiniz")]
        public int? Stock { get; set; }
        [Required(ErrorMessage = "Renk  Alanı Zorunlu")]
        public string? Color { get; set; }

        //?null atıo
        public bool IsPublish { get; set; }

        [Required(ErrorMessage = "Süre  Alanı Zorunlu")]
        public int? Expire { get; set; }

        [Required(ErrorMessage = "Description  Alanı Zorunlu")]
        [StringLength(300,MinimumLength =50, ErrorMessage = "50 ile 300 karaktere kadar girebilirsiniz")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Tarih  Alanı Zorunlu")]
        public DateTime? PublishDate { get; set; }

        [ValidateNever]
        public IFormFile? Image { get; set; }

        [ValidateNever]
        public string? ImagePath { get; set; }

        [Required(ErrorMessage = "Category Alanı Zorunlu")]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
