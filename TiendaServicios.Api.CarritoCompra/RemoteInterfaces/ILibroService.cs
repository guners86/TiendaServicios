using System;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoCompra.RemoteInterfaces
{
    public interface ILibroService
    {
        Task<(bool resultado, LibroRemote libro, string ErrorMesage)> GetLibro(Guid libroId);
    }
}
