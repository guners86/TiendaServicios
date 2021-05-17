namespace TiendaServicios.Api.Autor
{
    using AutoMapper;
    using FluentValidation.AspNetCore;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using TiendaServicios.Api.Autor.Aplicacion;
    using TiendaServicios.Api.Autor.Persistencia;

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
            // NOTA: Solo con se instancia una clase a validar es suficiente
            services.AddControllers().AddFluentValidation(conf => conf.RegisterValidatorsFromAssemblyContaining<Nuevo>());


            // Configuracion base de datos PostGres
            services.AddDbContext<Contexto>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("ConexionDataBase"));
            });

            // Configuracion el servicio de MeditR
            services.AddMediatR(typeof(Nuevo.Manejador).Assembly);

            // COnfigura la inyeccion de depndencias de autommaper en las diferentes clases que se utiliza
            services.AddAutoMapper(typeof(Consulta.Manejador));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
