namespace TiendaServicios.Api.Autor.Aplicacion
{
    using FluentValidation;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using TiendaServicios.Api.Autor.Modelo;
    using TiendaServicios.Api.Autor.Persistencia;

    public class Nuevo
    {
        public class Ejecuta : IRequest 
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly Contexto contexto;

            public Manejador(Contexto contexto)
            {
                this.contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                AutorLibro autorLibro = new AutorLibro
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    FechaNacimiento = request.FechaNacimiento,
                    AutorLibroGuid = Guid.NewGuid().ToString()
                };

                contexto.AutorLibro.Add(autorLibro);
                var valor = await contexto.SaveChangesAsync();

                if(valor > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el autor");
            }
        }
    }
}
