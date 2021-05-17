namespace TiendaServicios.Api.Autor.Aplicacion
{
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using TiendaServicios.Api.Autor.DataTransferObjects;
    using TiendaServicios.Api.Autor.Modelo;
    using TiendaServicios.Api.Autor.Persistencia;

    public class ConsultaFiltro
    {
        public class AutorUnico : IRequest<AutorDto>
        {
            public string AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDto>
        {
            public readonly Contexto contexto;
            public readonly IMapper mapper;

            public Manejador(Contexto contexto, IMapper mapper)
            {
                this.contexto = contexto;
                this.mapper = mapper;
            }

            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await contexto.AutorLibro.Where(w => w.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync();

                if (autor == null)
                    throw new Exception("No se encontro el autor");

                var autorDto = mapper.Map<AutorDto>(autor);

                return autorDto;
            }
        }
    }
}
