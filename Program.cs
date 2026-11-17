
using LocacaoVeiculos.Data;
using LocacaoVeiculos.Repositories;
using LocacaoVeiculos.Repositories.Interfaces;
using LocacaoVeiculos.Services;
using LocacaoVeiculos.Services.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace LocacaoVeiculos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuração MVC
            builder.Services.AddControllersWithViews();

            // Configuração do banco de dados
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );

            // Registro dos Repositories
            builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
            builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
            builder.Services.AddScoped<ILocacaoRepository, LocacaoRepository>();

            // Registro dos Services
            builder.Services.AddScoped<IVeiculoService, VeiculoService>();
            builder.Services.AddScoped<ICategoriaService, CategoriaService>();
            builder.Services.AddScoped<IClienteService, ClienteService>();
            builder.Services.AddScoped<ILocacaoService, LocacaoService>();

            var app = builder.Build();

            // Configuração do pipeline HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
