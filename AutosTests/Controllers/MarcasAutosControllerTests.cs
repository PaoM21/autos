using Autos.Data;
using Autos.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutosTests.Controllers
{
    public class MarcasAutosControllerTests
    {
        private readonly MarcasAutosController _controller;
        private readonly DataBase _context;

        public MarcasAutosControllerTests()
        {
            // Configuración de un DbContext en memoria con un nombre único
            var options = new DbContextOptionsBuilder<DataBase>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataBase(options);
            _controller = new MarcasAutosController(_context);

            // Sembrar algunos datos de prueba
            _context.MarcasAutos.AddRange(new List<MarcasAutos>
            {
                new MarcasAutos { Id = 1, Marca = "Kia" },
                new MarcasAutos { Id = 2, Marca = "Audi" }
            });

            // Guardar cambios en la base de datos
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetMarcasAutos_ReturnsOkResult_WithListOfMarcas()
        {
            // Act
            var result = await _controller.GetMarcasAutos();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnMarcas = okResult.Value.Should().BeAssignableTo<List<MarcasAutos>>().Subject;
            returnMarcas.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetMarcasAutos_WithInvalidId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.GetMarcasAutos(999);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task PostMarcasAutos_ReturnsCreatedAtAction_WithNewMarca()
        {
            // Arrange
            var newMarca = new MarcasAutos { Marca = "Mercedes" };

            // Act
            var result = await _controller.PostMarcasAutos(newMarca);

            // Assert
            var createdResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdResult.Value.Should().BeEquivalentTo(newMarca, options => options.Excluding(m => m.Id));
            createdResult.RouteValues["id"].Should().NotBeNull();
        }

        [Fact]
        public async Task PutMarcasAutos_ValidId_UpdatesMarca()
        {
            // Arrange
            var existingMarca = await _context.MarcasAutos.FindAsync(1);
            existingMarca.Marca = "Nissan";

            // Act
            var result = await _controller.PutMarcasAutos(1, existingMarca);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            var marca = await _context.MarcasAutos.FindAsync(1);
            marca.Should().NotBeNull();
            marca.Marca.Should().Be("Nissan");
        }

        [Fact]
        public async Task PutMarcasAutos_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var updatedMarca = new MarcasAutos { Id = 999, Marca = "Nissan" };

            // Act
            var result = await _controller.PutMarcasAutos(1, updatedMarca);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task DeleteMarcasAutos_ValidId_ReturnsNoContent()
        {
            // Act
            var result = await _controller.DeleteMarcasAutos(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            var marca = await _context.MarcasAutos.FindAsync(1);
            // Verificar eliminación.
            marca.Should().BeNull();
        }

        [Fact]
        public async Task DeleteMarcasAutos_InvalidId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.DeleteMarcasAutos(999);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
