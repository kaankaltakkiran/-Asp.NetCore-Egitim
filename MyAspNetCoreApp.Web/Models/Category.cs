namespace MyAspNetCoreApp.Web.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Producttan referans alma
        //1 ilişki (1-n in 1 si)
        //1 category çok ürün
        public List<Product>? Products { get; set; }
    }
}
