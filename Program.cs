using Microsoft.EntityFrameworkCore;
using Beon.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BeonDbContext>(opts => {
  opts.UseSqlite(
  builder.Configuration["ConnectionStrings:BeonConnection"]);
});

builder.Services.AddScoped<IBoardRepository, EFBoardRepository>();
builder.Services.AddScoped<ITopicRepository, EFTopicRepository>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();


app.UseStaticFiles();

app.MapControllerRoute("index", "", new { Controller = "Board", action = "Index"});
app.MapControllerRoute("ShowBoard", "Board/{boardId:int}", new { Controller = "Board", action = "Show"});
app.MapControllerRoute("cr8topic", "Topic/Create", new { Controller = "Topic", action = "Create" });

app.MapDefaultControllerRoute();

app.Run();
