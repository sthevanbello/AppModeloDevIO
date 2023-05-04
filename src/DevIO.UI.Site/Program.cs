using DevIO.UI.Site.Data;
using DevIO.UI.Site.Data.Context;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

namespace DevIO.UI.Site
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Tudo inicia a partir do builder
            var builder = WebApplication.CreateBuilder(args);

            // Adicionando o MVC ao container
            builder.Services.AddControllersWithViews();

            // Configura��o das Areas com nome personalizado da pasta
            builder.Services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/Modulos/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Modulos/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");

            });

            // Inje��o de depend�nia

            builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
            
            // Utiliza��o do contexto
            builder.Services.AddDbContext<MeuDbContext>( options => 
                        options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLServer")));

            // Realizando o buid das configura��es que resultar� na App
            var app = builder.Build();

            // Ativando a pagina de erros caso seja ambiente de desenvolvimento
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            // Adicionando Rota padr�o
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Adicionando rotas de Areas
            app.MapAreaControllerRoute(
                name: "areas",
                areaName: "Produtos",
                pattern: "{controller=Cadastro}/{action=Index}/{id?}");
            app.MapAreaControllerRoute(
                name: "areas",
                areaName: "Vendas",
                pattern: "{controller=Pedidos}/{action=Index}/{id?}");

            // Colocando a App para rodar
            app.Run();
        }
    }
}