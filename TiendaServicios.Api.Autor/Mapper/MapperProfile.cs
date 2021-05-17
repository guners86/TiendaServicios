namespace TiendaServicios.Api.Autor.Mapper
{
    using AutoMapper;
    using TiendaServicios.Api.Autor.DataTransferObjects;
    using TiendaServicios.Api.Autor.Modelo;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AutorLibro, AutorDto>();
        }
    }
}
