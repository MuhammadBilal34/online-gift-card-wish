using Microsoft.AspNetCore.Authentication.Cookies;
using mytheme.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ye b lazmi add krwani ha//
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
              op =>
              {
                  op.LoginPath = "/cookies/Login";
                  op.AccessDeniedPath = "/cookies/Login";
                  op.ExpireTimeSpan = TimeSpan.FromMinutes(60);
              }
              );






// ye wali line add ki ha sirf
builder.Services.AddDbContext<StudentdbContext>();

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


// ye teen line add krni ha lazmi
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Client}/{action=Index}/{id?}");

app.Run();
