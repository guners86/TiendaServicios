namespace TiendaServicios.Api.Libro.Test
{
    using AutoMapper;
    using TiendaServicios.Api.Libro.DataTransferObjects;
    using TiendaServicios.Api.Libro.Modelo;

    public class MappingTest : Profile
    {
        public MappingTest()
        {
            CreateMap<LibreriaMaterial, LibroMaterialDto>();
        }
    }
}
