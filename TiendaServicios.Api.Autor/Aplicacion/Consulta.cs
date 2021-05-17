namespace TiendaServicios.Api.Autor.Aplicacion
{
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using TiendaServicios.Api.Autor.DataTransferObjects;
    using TiendaServicios.Api.Autor.Modelo;
    using TiendaServicios.Api.Autor.Persistencia;

    public class Consulta
    {
        public class ListaAutor : IRequest<List<AutorDto>>
        {

        }

        public class Manejador : IRequestHandler<ListaAutor, List<AutorDto>>
        {
            public readonly Contexto contexto;
            public readonly IMapper mapper;

            public Manejador(Contexto contexto, IMapper mapper)
            {
                this.contexto = contexto;
                this.mapper = mapper;
            }

            public async Task<List<AutorDto>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                var autores = await contexto.AutorLibro.ToListAsync();
                var autoresDto = mapper.Map<List<AutorDto>>(autores);

                return autoresDto;
            }
        }
    }
}
