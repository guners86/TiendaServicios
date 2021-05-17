using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.DataTransferObjects;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterfaces;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoDto>
        {
            public int CarritoSesionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {
            private readonly ContextoCarrito contexto;
            private readonly ILibroService libroService;

            public Manejador(ContextoCarrito contexto, ILibroService libroService)
            {
                this.contexto = contexto;
                this.libroService = libroService;
            }

            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await contexto.CarritoSesion.FirstOrDefaultAsync(x => x.CarritoSesionId.Equals(request.CarritoSesionId));
                var carritoSesionDetalle = await contexto.CarritoSesionDetalle.Where(x => x.CarritoSesionId.Equals(request.CarritoSesionId)).ToListAsync();

                List<CarritoDetalleDto> listaCarritoDetalle = new List<CarritoDetalleDto>();

                foreach (var libro in carritoSesionDetalle)
                {
                    var response = await libroService.GetLibro(new Guid(libro.ProductoSeleccionado));

                    if(response.resultado)
                    {
                        listaCarritoDetalle.Add(new CarritoDetalleDto 
                        {
                            AutorLibro = response.libro.AutorLibro.ToString(),
                            FechaPublicacion = response.libro.FechaPublicacion,
                            LibroId = response.libro.LibreriaMaterialId,
                            TituloLibro = response.libro.Titulo
                        });
                    }
                }

                CarritoDto carrito = new CarritoDto
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacion = carritoSesion.FechaCreacion,
                    ListaProductos = listaCarritoDetalle
                };

                return carrito;
            }
        }
    }
}
