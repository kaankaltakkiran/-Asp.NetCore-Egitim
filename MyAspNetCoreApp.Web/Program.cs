using Microsoft.EntityFrameworkCore;
using MyAspNetCoreApp.Web.Filters;
using MyAspNetCoreApp.Web.Helpers;
using MyAspNetCoreApp.Web.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Veritaban� i�leri
//AppDbContext olu�turdu�umuz model contexti
builder.Services.AddDbContext<AppDbContext>(options =>
{
    //Kime ba�lancak
    //Appsettingsjon a git ConnectionString deki SqlConu oku
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

//Programa Interface g�r�nce ne yapca��n� s�yleme
builder.Services.AddTransient<IHelper, Helper>();

//Auto Mapper i�in
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

////Asterisk ile olu�turma
////Dinamik alan gelirse
////Hep article sayfas� gelir
//app.MapControllerRoute(
//    name: "pages",
//    pattern: "blog/{*article}",
//    defaults: new { controller = "Blog", action = "Article" });

////conventional example Routing tan�mlama
////�zellerde gerek yok
////Genel tan�mla
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

//�rne�in index sayfas� g�r�nt�leme
//baseUrl/Home/index
//baseurl/ornek/�ndex2
//Localdeki baseUrl https://localhost:7083/
//Canl�da ise: https://www.mysite.com
app.Run();
