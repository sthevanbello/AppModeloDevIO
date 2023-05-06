using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DevIO.Identity.Areas.Identity.Data;
using NuGet.LibraryModel;

namespace DevIO.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var connectionString = builder.Configuration
                .GetConnectionString("DevIOIdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'DevIOIdentityContextConnection' not found.");

            builder.Services.AddDbContext<DevIOIdentityContext>(options =>
                                                                options.UseSqlServer(connectionString));

            builder.Services
                .AddDefaultIdentity<IdentityUser>(options => 
                                                    options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DevIOIdentityContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Adicionando o serviço no pipeline

            builder.Services.AddRazorPages();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/erro/500");
                app.UseStatusCodePagesWithRedirects("/erro/{0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Mapeando componentes Razor Pages (ex: Identity)

            app.MapRazorPages();

            app.Run();
        }
    }
}