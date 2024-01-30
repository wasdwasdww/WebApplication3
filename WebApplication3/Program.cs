using Microsoft.AspNetCore.Identity;
//using WebApplication3.Middleware;
using WebApplication3.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AuthDbContext>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();
builder.Services.AddDataProtection();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 12;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});

builder.Services.ConfigureApplicationCookie(config =>
{
	config.LoginPath = "/login";
});

var app = builder.Build();





app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStatusCodePagesWithRedirects("/errors/{0}");

app.UseRouting();
app.UseSession();


app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();


//app.UseMiddleware<SessionTimeoutMiddleware>();
app.Run();

