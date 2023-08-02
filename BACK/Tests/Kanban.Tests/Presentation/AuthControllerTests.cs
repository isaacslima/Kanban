using Kanban.Application.Common.Models.Request;
using KanbanBackend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Kanban.Tests.Presentation;

public class AuthControllerTests
{
    private readonly AuthController _controller;
    private readonly Mock<IConfiguration> _mockConfiguration;

    public AuthControllerTests()
    {
        _mockConfiguration = new Mock<IConfiguration>();
        _mockConfiguration.Setup(x => x["Jwt:Key"]).Returns("config_jwt_secret_key_token");

        _controller = new AuthController(_mockConfiguration.Object);
    }

    [Fact]
    public void Login_When_ValidCredentials_ReturnsOkResultWithToken()
    {
        var loginRequest = new LoginRequest("letscode", "lets@123");

        var result = _controller.Login(loginRequest);

        var okResult = Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Login_InvalidCredentials_ReturnsUnauthorizedResult()
    {
        var loginRequest = new LoginRequest("loginIncorreto", "senhaIncorreta");

        var result = _controller.Login(loginRequest);

        Assert.IsType<UnauthorizedResult>(result);
    }
}
