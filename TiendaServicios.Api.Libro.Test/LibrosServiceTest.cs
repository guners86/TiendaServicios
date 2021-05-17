using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;
using Xunit;

namespace TiendaServicios.Api.Libro.Test
{
    public class LibrosServiceTest
    {
        [Fact]
        public async Task GetLibros()
        {
            var mockContexto = CrearContexto();
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mockMapper = mapConfig.CreateMapper();

            Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object, mockMapper);
            Consulta.Ejecuta request = new Consulta.Ejecuta();
            var lista = await manejador.Handle(request, new CancellationToken());

            Assert.True(lista.Any());
        }

        [Fact]
        public async Task GetLibroPorId()
        {
            var mockContexto = CrearContexto();
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mockMapper = mapConfig.CreateMapper();

            ConsultaFiltro.Manejador manejador = new ConsultaFiltro.Manejador(mockContexto.Object, mockMapper);
            ConsultaFiltro.LibroUnico request = new ConsultaFiltro.LibroUnico();
            request.LibroId = Guid.Empty;

            var libro = await manejador.Handle(request, new CancellationToken());

            Assert.NotNull(libro);
            Assert.True(libro.LibreriaMaterialId == Guid.Empty);
        }

        [Fact]
        public async Task GuardarLibro()
        {
            var options = new DbContextOptionsBuilder<ContextoLibreria>()
                .UseInMemoryDatabase(databaseName: "BaseDatosLibro").Options;

            var contexto = new ContextoLibreria(options);

            var request = new Nuevo.Ejecuta 
            {
                Titulo = "Libro Microservices",
                AutorLibro = Guid.Empty,
                FechaPublicacion = DateTime.Now
            };

            var manejador = new Nuevo.Manejador(contexto);
            var response = await manejador.Handle(request, new CancellationToken());
            Assert.True(response != null);
        }

        private Mock<ContextoLibreria> CrearContexto()
        {
            var dataPrueba = ObtenerDataPrueba().AsQueryable();

            var dbSet = new Mock<DbSet<LibreriaMaterial>>();
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>()
                .Setup(x => x.GetAsyncEnumerator(new CancellationToken()))
                .Returns(new AsyncEnumerator<LibreriaMaterial>(dataPrueba.GetEnumerator()));

            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(dataPrueba.Provider));

            var contexto = new Mock<ContextoLibreria>();
            contexto.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);
            return contexto;
        }

        private IEnumerable<LibreriaMaterial> ObtenerDataPrueba()
        {
            A.Configure<LibreriaMaterial>()
                .Fill(x => x.Titulo).AsArticleTitle()
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<LibreriaMaterial>(30);

            // Se simula un guid unico para cuando se vaya hacer el test de consultar libro por guid
            lista[0].LibreriaMaterialId = Guid.Empty;

            return lista;
        }
    }
}
