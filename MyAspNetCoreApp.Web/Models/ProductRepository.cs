namespace MyAspNetCoreApp.Web.Models
{
	public class ProductRepository
	{
		//Listelme İçin 
		//static memoryde
		private static List<Product> _products = new List<Product>()
		{
			(new() { Id = 1, Name = "Kalem", Price = 50, Stock = 200 }),
		    (new () { Id = 2, Name = "Silgi", Price = 150, Stock = 100 }),
			(new () { Id = 3, Name = "Defter", Price = 250, Stock = 50 }),
		};

		//Liste Gezme _products return
		public List<Product> GetAll() => _products;
		//Ekleme İşlemi
		public void Add(Product newProduct)=> _products.Add(newProduct);
		//Silme İşlemi
		public void Remove(int id)
		{
			//Ürün Var Mı Yok Mu?
			//p Proudct'ın idsi
			var hasProduct=_products.FirstOrDefault(p => p.Id == id);
			//Ürün Yoksa Hata Fırlatma
			if (hasProduct==null)
			{
				throw new Exception($"Bu İd({id}) ye sahip ürün yok");
			}
			//Silme İşlemi
			_products.Remove(hasProduct);
		}
		//Update İşlemi
		public void Update(Product updateProduct)
		{
			//Güncellenecek Ürün Var Mı Yok Mu?
			//p Proudct'ın idsi
			var hasProduct = _products.FirstOrDefault(p => p.Id == updateProduct.Id);
			//Ürün Yoksa Hata Fırlatma
			if (hasProduct == null)
			{
				throw new Exception($"Bu İd({updateProduct.Id}) ye sahip ürün yok");
			}
			//Update İşlemi
			hasProduct.Name = updateProduct.Name;
			hasProduct.Price = updateProduct.Price;
			hasProduct.Stock = updateProduct.Stock;

			//Dizindeki İndexi Bulma
			var index= _products.FindIndex(p=>p.Id == updateProduct.Id);
			//İndexleri Güncelle
			_products[index] = hasProduct;	
		}
	}
}
