using Microsoft.EntityFrameworkCore;

namespace MyAspNetCoreApp.Web.Models
{
    public class AppDbContext:DbContext
    {
        //Miras Aldığı Yere Gidiyor 
        //Optionsı Programcs De dolcurulacak
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        //Entity tanımlama
        //Products Tablo ismi
        public DbSet<Product> Products { get; set; }
        //Visitor için dbset
        public DbSet<Visitor> Visitor { get; set; }
      
    }
}
