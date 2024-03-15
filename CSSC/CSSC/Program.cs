using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CSSC.Areas.Identity.Data;
using CSSC;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var cultureInfo = new CultureInfo("pt-PT");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var connectionString = builder.Configuration.GetConnectionString("CSSCContextConnection") ?? throw new InvalidOperationException("Connection string 'CSSCContextConnection' not found.");

builder.Services.AddDbContext<CSSCContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<CSSCUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
 .AddEntityFrameworkStores<CSSCContext>()
 .AddDefaultUI()
 .AddDefaultTokenProviders();
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddPolicy("corsPolicy",
        policy =>
        {
            policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseCors("corsPolicy");

app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using var scope = app.Services.CreateScope();
await Configurations.CreateRoles(scope.ServiceProvider);

app.Run();
