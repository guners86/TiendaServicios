using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.RemoteInterfaces;
using TiendaServicios.Api.CarritoCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoCompra.RemoteServices
{
    public class LibroService : ILibroService
    {
        private readonly IHttpClientFactory httpCLient;
        private readonly ILogger<LibroService> logger;

        public LibroService(IHttpClientFactory httpCLient, ILogger<LibroService> logger)
        {
            this.httpCLient = httpCLient;
            this.logger = logger;
        }

        public async Task<(bool resultado, LibroRemote libro, string ErrorMesage)> GetLibro(Guid libroId)
        {
            try
            {
                logger.LogInformation("Entro yahoooooooooooooooooooooooo");
                var cliente = httpCLient.CreateClient("Libros");
                var response = await cliente.GetAsync($"api/LibroMaterial/{libroId}");

                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    LibroRemote libro = JsonSerializer.Deserialize<LibroRemote>(content, options);
                    return (true, libro, string.Empty);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
