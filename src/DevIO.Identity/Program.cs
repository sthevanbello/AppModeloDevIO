using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DevIO.Identity.Areas.Identity.Data;
using NuGet.LibraryModel;
using DevIO.Identity.Extensions;
using Microsoft.AspNetCore.Authorization;
using DevIO.Identity.Config;

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

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddAuthorizationConfig();
            builder.Services.AddIdentityConfig(builder.Configuration);
            builder.Services.ResolveDependencies();

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