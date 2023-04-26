using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
    AddCookie(opt => opt.LoginPath = "/Account/Login"); // add use cookies authentication
                                                        // login page: Account controller, Login method
builder.Services.AddSession(); // add before AddControllersWithVies to use session state

builder.Services.AddControllersWithViews(); // authentication

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseStatusCodePages(); // Authentication: for user friendly error messages for 404, 403 errors

app.UseRouting();

app.UseAuthentication(); // add for Authentication

app.UseAuthorization();

app.UseSession(); // for session state

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.UseEndpoints(endpoints =>
//{
//    // most specific route
//    endpoints.MapControllerRoute(
//        name: "slips_by_docks",
//        pattern: "{controller}/{action}/slips_by_docks");

//    // specific route
//    endpoints.MapControllerRoute(
//        name: "Index",
//        pattern: "{controller}/{action}");

//    // least specific
//    endpoints.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
//});


app.Run();
