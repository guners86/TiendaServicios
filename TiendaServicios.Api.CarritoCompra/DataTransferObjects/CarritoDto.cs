using System;
using System.Collections.Generic;

namespace TiendaServicios.Api.CarritoCompra.DataTransferObjects
{
    public class CarritoDto
    {
        public int CarritoId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public List<CarritoDetalleDto> ListaProductos { get; set; }
    }
}
