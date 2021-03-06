namespace TiendaServicios.Api.Autor.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TiendaServicios.Api.Autor.Aplicacion;
    using TiendaServicios.Api.Autor.DataTransferObjects;
    using TiendaServicios.Api.Autor.Modelo;

    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMediator mediator;

        public AutorController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<AutorDto>>> GetAutores()
        {
            return await mediator.Send(new Consulta.ListaAutor());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDto>> GetAutorLibro(string id)
        {
            return await mediator.Send(new ConsultaFiltro.AutorUnico { AutorGuid = id });
        }
    }
}
