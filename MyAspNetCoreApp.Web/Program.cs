using Microsoft.EntityFrameworkCore;
using MyAspNetCoreApp.Web.Filters;
using MyAspNetCoreApp.Web.Helpers;
using MyAspNetCoreApp.Web.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Veritabaný iþleri
//AppDbContext oluþturduðumuz model contexti
builder.Services.AddDbContext<AppDbContext>(options =>
{
    //Kime baðlancak
    //Appsettingsjon a git ConnectionString deki SqlConu oku
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

//Programa Interface görünce ne yapcaðýný söyleme
builder.Services.AddTransient<IHelper, Helper>();

//Auto Mapper için
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//NotFoundFilter
builder.Services.AddScoped<NotFoundFilter>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

////Asterisk ile oluþturma
////Dinamik alan gelirse
////Hep article sayfasý gelir
//app.MapControllerRoute(
//    name: "pages",
//    pattern: "blog/{*article}",
//    defaults: new { controller = "Blog", action = "Article" });

////conventional example Routing tanýmlama
////Özellerde gerek yok
////Genel tanýmla
//app.MapControllerRoute(
//    name: "productpages",
//    pattern: "{controller}/{action}/{page}/{pageSize}");

////conventional example
//app.MapControllerRoute(
//    name: "productgetbyid",
//    pattern: "{controller}/{action}/{productid}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Örneðin index sayfasý görüntüleme
//baseUrl/Home/index
//baseurl/ornek/ýndex2
//Localdeki baseUrl https://localhost:7083/
//Canlýda ise: https://www.mysite.com
app.Run();
