using APP.Business.Models;
using APP.Business.Services;
using APP.DataAccess;
using CORE.Repositoires;
using CORE.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = @"server=(localdb)\mssqllocaldb;database=YBETradeDB;trusted_connection=true;";
// IoC Container: (Inversion of Control)
builder.Services.AddDbContext<DbContext, Db>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(RepoBase<>), typeof(Repo<>)); // service'lerde enjekte ediliyor
builder.Services.AddScoped<Service<Category, CategoryRequest, CategoryResponse>, CategoryService>(); // controller'larda enjekte ediliyor
builder.Services.AddScoped<Service<Product, ProductRequest, ProductResponse>, ProductService>(); // controller'larda enjekte ediliyor
builder.Services.AddScoped<Service<Store, StoreRequest, StoreResponse>, StoreService>(); // controller'larda enjekte ediliyor

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
