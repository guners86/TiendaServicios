using AutoMapper;
using TiendaServicios.Api.Libro.DataTransferObjects;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<LibreriaMaterial, LibroMaterialDto>();
        }
    }
}
