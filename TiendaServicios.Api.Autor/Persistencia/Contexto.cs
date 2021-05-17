namespace TiendaServicios.Api.Autor.Persistencia
{
    using Microsoft.EntityFrameworkCore;
    using TiendaServicios.Api.Autor.Modelo;

    public class Contexto : DbContext
    {
        public DbSet<AutorLibro> AutorLibro { get; set; }
        public DbSet<GradoAcademico> GradoAcademico { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    }
}
