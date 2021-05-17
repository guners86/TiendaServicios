using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Aplicacion;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterfaces;
using TiendaServicios.Api.CarritoCompra.RemoteServices;

namespace TiendaServicios.Api.CarritoCompra
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ILibroService, LibroService>();

            // COnfiguracion de validacion de los datos mediante la libreria fluent validation
            // NOTA: Solo con una instancia a una clase a validar es suficiente
            services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

            // Configuracion base de datos MySql
            services.AddDbContext<ContextoCarrito>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("ConexionDb"));
            });

            // Configuracion el servicio de MeditR
            services.AddMediatR(typeof(Nuevo.Manejador).Assembly);

            // Configuracion el servicio de consulta al microservicio libros
            services.AddHttpClient("Libros", config => {
                config.BaseAddress = new Uri(Configuration["Services:Libros"]);
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
