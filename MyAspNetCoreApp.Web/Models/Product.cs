namespace MyAspNetCoreApp.Web.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public decimal Price { get; set; }
		public int Stock { get; set; }
		public string? Color { get; set; }

		//?null atıo
		public bool IsPublish { get; set; }

		public int Expire { get; set; }

		public string Description { get; set; }

		public DateTime? PublishDate { get; set; }

       public string? ImagePath { get; set; }

		//Key Value
		public int CategoryId {get; set; }

        //1 ilişki (1-n n si)
		//1 category çok ürün
        public Category Category { get; set; }

    }
}
