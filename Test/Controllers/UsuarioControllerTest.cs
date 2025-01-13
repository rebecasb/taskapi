using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskAPI.Controllers;
using TaskAPI.DTO;
using TaskAPI.Interfaces;
using TaskAPI.Models;
using TaskAPI.Services;

namespace Test.Controllers;

public class UsuarioControllerTest
{
    private readonly Mock<IUsuario> _mockUsuarioService;
    private readonly Mock<IAuth> _mockAuthService;
    private readonly UsuarioController _controller;

    public UsuarioControllerTest()
    {
        _mockUsuarioService = new Mock<IUsuario>();
        _mockAuthService = new Mock<IAuth>();
        _controller = new UsuarioController(_mockUsuarioService.Object, _mockAuthService.Object);
    }

    [Fact]
    public async Task Registrar_ShouldReturnBadRequest_WhenEmailAlreadyExists()
    {
        // Arrange
        var registroDto = new RegistroDTO
        {
            NombreUsuario = "testuser",
            Email = "rebeca@abc.com",
            Password = "test123"
        };

        _mockUsuarioService.Setup(s => s.ExisteEmailsAsync(It.Is<string>(email => email == registroDto.Email)))
        .ReturnsAsync(true);


        // Act
        var result = await _controller.Registrar(registroDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("El correo electrónico ya está registrado.", badRequestResult.Value);
    }

}
