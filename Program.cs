using Microsoft.EntityFrameworkCore;
using Beon.Models;
using Microsoft.AspNetCore.Identity;
using Beon.Services;
using Beon.Infrastructure;
using Npgsql;
using Beon.Hubs;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("IdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityDbContextConnection' not found.");

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

bool tryMigrate = false;

//set DESIGNTIME if you only need to create migrations
var designTime = Environment.GetEnvironmentVariable("DESIGNTIME");

if (designTime != null)
{
  if (designTime == "SQLITE")
  {
    builder.Services.AddDbContext<BeonDbContext>(opts =>
    {
      opts.UseSqlite();
    });
  }
  else if (designTime == "POSTGRESQL")
  {
    builder.Services.AddDbContext<BeonDbContext>(opts =>
    {
      opts.UseNpgsql();
    });
  }
  else
  {
    throw new Exception("invalid DESIGNTIME envar value");
  }
  builder.Build();
  return;
}

var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

if (databaseUrl != null)
{ //on heroku  
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
{ //on localhost
  builder.Services.AddDbContext<BeonDbContext>(opts =>
  {
    opts.UseSqlite(
    builder.Configuration["ConnectionStrings:BeonConnection"]);
  });
}

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
builder.Services.AddScoped<IRepository<Post>, EFRepository<Post>>();
builder.Services.AddScoped<IRepository<Diary>, EFRepository<Diary>>();
builder.Services.AddScoped<IRepository<UserDiary>, EFRepository<UserDiary>>();
builder.Services.AddScoped<IRepository<UserDiaryEntry>, EFRepository<UserDiaryEntry>>();
builder.Services.AddScoped<IRepository<TopicSubscription>, EFRepository<TopicSubscription>>();
builder.Services.AddScoped<IRepository<Comment>, EFRepository<Comment>>();
builder.Services.AddScoped<IRepository<Diary>, EFRepository<Diary>>();
builder.Services.AddScoped<IRepository<FriendLink>, EFRepository<FriendLink>>();
builder.Services.AddScoped<IRepository<DiaryEntryCategory>, EFRepository<DiaryEntryCategory>>();

builder.Services.AddScoped<IEmailSender, AuthMessageSender>();
builder.Services.AddScoped<ISmsSender, AuthMessageSender>();

builder.Services.AddScoped<IViewComponentRenderService, ViewComponentRenderService>();
builder.Services.AddScoped<TopicSubscriptionService, TopicSubscriptionService>();
builder.Services.AddScoped<UserDiaryEntryService, UserDiaryEntryService>();
builder.Services.AddScoped<TopicService, TopicService>();

string userFileStorageType = Environment.GetEnvironmentVariable("USER_STORAGE_TYPE") ?? "DISK";

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
else if (userFileStorageType == "DISK")
{
  builder.Services.AddScoped<IUserFileRepository>(provider => new DiskUserFileRepository("i/user"));
}
else
{
  throw new Exception("invalid USER_STORAGE_TYPE envar value");
}

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

string? bAuthUserName = Environment.GetEnvironmentVariable("BAUTH_USERNAME");
if (bAuthUserName != null)
{
  string bAuthPassword = Environment.GetEnvironmentVariable("BAUTH_PASSWORD") ?? "";
  app.UseMiddleware<BasicAuthMiddleware>(Options.Create(new BasicAuthOptions(bAuthUserName, bAuthPassword)));
}

app.UseStaticFiles();

app.MapHub<TopicHub>("/SignalR");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.MapDefaultControllerRoute();
app.UseAuthentication();
app.UseAuthorization();

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
}

if (tryMigrate)
{
  BeonDbContext context = (app as IApplicationBuilder).ApplicationServices
  .CreateScope().ServiceProvider.GetRequiredService<BeonDbContext>();

  if (context.Database.GetPendingMigrations().Any())
  {
    context.Database.Migrate();
  }
}

CreateAdmin.Create(app);

app.Run();
