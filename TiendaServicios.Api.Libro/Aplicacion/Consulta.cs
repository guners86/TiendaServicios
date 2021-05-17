using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.DataTransferObjects;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<LibroMaterialDto>>
        {

        }

        public class Manejador : IRequestHandler<Ejecuta, List<LibroMaterialDto>>
        {
            private readonly ContextoLibreria contexto;
            public readonly IMapper mapper;

            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                this.contexto = contexto;
                this.mapper = mapper;
            }

            public async Task<List<LibroMaterialDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await contexto.LibreriaMaterial.ToListAsync();
                var librosDto = mapper.Map<List<LibroMaterialDto>>(libros);

                return librosDto;
            }
        }
    }
}
