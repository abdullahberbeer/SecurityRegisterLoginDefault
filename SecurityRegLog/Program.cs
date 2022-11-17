
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NToastNotify;
using SecurityRegLog.CustomValidations;
using SecurityRegLog.Data;
using SecurityRegLog.EmailServices;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();




builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddPasswordValidator<CustomPasswordValidation>().AddErrorDescriber<CustomIdentityErrorDescriber>().AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages().AddNToastNotifyToastr(new ToastrOptions()
{ 
    CloseButton = true,
    ProgressBar = true,
    PositionClass = ToastPositions.BottomRight
}); 
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = ".SecurityRegLog.Security.Cookie",
        SameSite = SameSiteMode.Strict
    };
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);

}
);
builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(i=>new SmtpEmailSender(
   builder.Configuration["EmailSender:Host"],
   builder.Configuration.GetValue<int>("EmailSender:Port"),
   builder.Configuration.GetValue<bool>("EmailSender:enableSSL"),
   builder.Configuration["EmailSender:UserName"],
   builder.Configuration["EmailSender:Password"]
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseNToastNotify();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
