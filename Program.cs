using Microsoft.EntityFrameworkCore;
using Beon.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BeonDbContext>(opts => {
  opts.UseSqlite(
  builder.Configuration["ConnectionStrings:BeonConnection"]);
});

builder.Services.AddScoped<IBoardRepository, EFBoardRepository>();

var app = builder.Build();


app.UseStaticFiles();

app.MapControllerRoute("index", "", new { Controller = "Board", action = "Index"});
app.MapDefaultControllerRoute();

app.Run();
