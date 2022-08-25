using Microsoft.EntityFrameworkCore;
using Beon.Models;
using Microsoft.AspNetCore.Identity;
using Beon.Services;
using Beon.Infrastructure;
using Npgsql;
using Beon.Hubs;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("IdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityDbContextConnection' not found.");

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

bool tryMigrate = false;

var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

//to create migrations for heroku (postgres):
//DATABASE_URL=DesignTime dotnet ef migrations add Initial
if (databaseUrl != null) { //on heroku  
  if (databaseUrl != "DesignTime") {  
    tryMigrate = true;
    var databaseUri = new Uri(databaseUrl);
    var userInfo = databaseUri.UserInfo.Split(':');

    var strb = new NpgsqlConnectionStringBuilder
    {
        Host = databaseUri.Host,
        Port = databaseUri.Port,
        Username = userInfo[0],
        Password = userInfo[1],
        Database = databaseUri.LocalPath.TrimStart('/'),
        TrustServerCertificate = true
    };
    builder.Services.AddDbContext<BeonDbContext>(opts => {
      opts.UseNpgsql(strb.ToString());
    });
  }
  else {
    builder.Services.AddDbContext<BeonDbContext>(opts => {
      opts.UseNpgsql();
    });
  }
}
else { //on localhost
  builder.Services.AddDbContext<BeonDbContext>(opts => {
    opts.UseSqlite(
    builder.Configuration["ConnectionStrings:BeonConnection"]);
  });
}

/*builder.Services.AddDbContext<IdentityDbContext>(opts => {
  opts.UseSqlite(
  builder.Configuration["ConnectionStrings:IdentityDbContextConnection"]);
});*/

builder.Services.AddDefaultIdentity<BeonUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BeonDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});



builder.Services.AddScoped<IBoardRepository, EFBoardRepository>();
builder.Services.AddScoped<ITopicRepository, EFTopicRepository>();
builder.Services.AddScoped<ITopicSubscriptionRepository, EFTopicSubscriptionRepository>();
builder.Services.AddScoped<IPostRepository, EFPostRepository>();
builder.Services.AddScoped<IDiaryRepository, EFDiaryRepository>();
builder.Services.AddScoped<IEmailSender, AuthMessageSender>();
builder.Services.AddScoped<ISmsSender, AuthMessageSender>();
builder.Services.AddScoped<IViewComponentRenderService, ViewComponentRenderService>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();


app.UseStaticFiles();

app.MapHub<TopicHub>("/SignalR");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

/*app.MapControllerRoute("ShowBoard", "Board/{boardId:int}", new { Controller = "Board", action = "Show"});
app.MapControllerRoute("cr8topic", "Topic/Create", new { Controller = "Topic", action = "Create" });
*/

app.MapDefaultControllerRoute();
app.UseAuthentication();
app.UseAuthorization();
//app.MapRazorPages();

if (!app.Environment.IsDevelopment()) {
  app.UseExceptionHandler("/Error");
}

CreateAdmin.Create(app, tryMigrate);

app.Run();
