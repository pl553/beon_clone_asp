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
if (databaseUrl != null)
{ //on heroku  
  if (databaseUrl != "DesignTime")
  {
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
    builder.Services.AddDbContext<BeonDbContext>(opts =>
    {
      opts.UseNpgsql(strb.ToString());
    });
  }
  else
  {
    builder.Services.AddDbContext<BeonDbContext>(opts =>
    {
      opts.UseNpgsql();
    });
    return;
  }
}
else
{ //on localhost
  builder.Services.AddDbContext<BeonDbContext>(opts =>
  {
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

builder.Services.ConfigureApplicationCookie(options =>
{
  options.LoginPath = "/Account/Login";
  options.ReturnUrlParameter = "returnUrl";
});

builder.Services.AddScoped<IRepository<Topic>, EFRepository<Topic>>();
builder.Services.AddScoped<IRepository<Board>, EFRepository<Board>>();
builder.Services.AddScoped<IRepository<TopicSubscription>, EFRepository<TopicSubscription>>();
builder.Services.AddScoped<IRepository<Post>, EFRepository<Post>>();
builder.Services.AddScoped<IRepository<Diary>, EFRepository<Diary>>();

builder.Services.AddScoped<IEmailSender, AuthMessageSender>();
builder.Services.AddScoped<ISmsSender, AuthMessageSender>();

builder.Services.AddScoped<IViewComponentRenderService, ViewComponentRenderService>();
builder.Services.AddScoped<TopicLogic, TopicLogic>();
builder.Services.AddScoped<TopicSubscriptionLogic, TopicSubscriptionLogic>();

string? userFileStorageType = Environment.GetEnvironmentVariable("USER_STORAGE_TYPE");

if (userFileStorageType == null)
{
  throw new Exception("please set le USER_STORAGE_TYPE envar ");
}

if (userFileStorageType == "S3")
{
  string? endpoint = Environment.GetEnvironmentVariable("S3_ENDPOINT");
  string? accessKeyId = Environment.GetEnvironmentVariable("S3_ACCESSKEY");
  string? secret = Environment.GetEnvironmentVariable("S3_SECRET");
  string? bucket = Environment.GetEnvironmentVariable("S3_BUCKET");

  if (endpoint == null || accessKeyId == null || secret == null || bucket == null)
  {
    throw new Exception("please set le s3 connection environment vvariablez");
  }

  builder.Services.AddScoped<IUserFileRepository>
    (provider => new S3UserFileRepository("i/user", endpoint, accessKeyId, secret, bucket));
}
else if (userFileStorageType == "DISK") {
  builder.Services.AddScoped<IUserFileRepository>(provider => new DiskUserFileRepository("i/user"));
}
else {
  throw new Exception("invalid USER_STORAGE_TYPE envar value");
}

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

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
}

CreateAdmin.Create(app, tryMigrate);

app.Run();
