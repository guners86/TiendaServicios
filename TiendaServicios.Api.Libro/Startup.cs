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
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro
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
            // COnfiguracion de validacion de los datos mediante la libreria fluent validation
            // NOTA: Solo con una instancia a una clase a validar es suficiente
            services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

            // Configuracion base de datos Sql Server
            services.AddDbContext<ContextoLibreria>(opc =>
            {
                opc.UseSqlServer(Configuration.GetConnectionString("ConexionDb"));
            });


            // Configuracion el servicio de MeditR
            services.AddMediatR(typeof(Nuevo.Manejador).Assembly);

            // Configura la inyeccion de depndencias de autommaper en las diferentes clases que se utiliza
            services.AddAutoMapper(typeof(Consulta.Manejador));
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
