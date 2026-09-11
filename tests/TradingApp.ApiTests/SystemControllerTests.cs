using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TradingApp.Api.Controllers;
using Xunit;

namespace TradingApp.ApiTests;

public class SystemControllerTests
{
    [Fact]
    public void GetStatus_ReturnsOkResult_WithSystemStatus()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<SystemController>>();
        var controller = new SystemController(loggerMock.Object);

        // Act
        var result = controller.GetStatus();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        
        // Use reflection to check properties since it's an anonymous type
        var statusProperty = okResult.Value.GetType().GetProperty("Status");
        Assert.NotNull(statusProperty);
        Assert.Equal("Online", statusProperty.GetValue(okResult.Value));
    }
}
