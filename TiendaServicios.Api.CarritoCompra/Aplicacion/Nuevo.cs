using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Modelo;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime FechaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoCarrito contexto;

            public Manejador(ContextoCarrito contexto)
            {
                this.contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carrito = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion
                };

                contexto.CarritoSesion.Add(carrito);
                var value = await contexto.SaveChangesAsync();

                if(value == 0)
                {
                    throw new Exception("No se pudo guardar el carrito");
                }

                int id = carrito.CarritoSesionId;

                List<CarritoSesionDetalle> carritoSesionDetalle = request.ProductoLista.Select(s => new CarritoSesionDetalle
                {
                    FechaCreacion = DateTime.Now,
                    CarritoSesionId = carrito.CarritoSesionId,
                    ProductoSeleccionado = s
                }).ToList();

                contexto.CarritoSesionDetalle.AddRange(carritoSesionDetalle);

                value = await contexto.SaveChangesAsync();

                return value > 0 ? Unit.Value : throw new Exception("No se pudo guardar el detalle del carrito");
            }
        }

    }
}
