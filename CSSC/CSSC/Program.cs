using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CSSC.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("CSSCContextConnection") ?? throw new InvalidOperationException("Connection string 'CSSCContextConnection' not found.");

builder.Services.AddDbContext<CSSCContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<CSSCUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<CSSCContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
