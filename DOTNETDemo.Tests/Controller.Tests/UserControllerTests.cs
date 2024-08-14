using DOTNETDemo.Controllers;
using DOTNETDemo.Models.Entities;
using DOTNETDemo.Models.Request;
using DOTNETDemo.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DOTNETDemo.Tests.Controller.Tests;

public class UserControllerTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly UserController _userController;

    public UserControllerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _userController = new UserController(_userServiceMock.Object);
    }

    [Fact]
    public async Task GetUserAsync_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = 1;
        _userServiceMock.Setup(service => service.GetUserAsyncById(userId)).ReturnsAsync((User)null);

        // Act
        var result = await _userController.GetUserAsync(userId) as NotFoundResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task PostUserDataAsync_ReturnsOk_WhenPostSucceeds()
    {
        // Arrange
        var request = new UserCardRequest { Id = 0 };
        _userServiceMock.Setup(service => service.PostUserDataAsync(request)).ReturnsAsync(true);

        // Act
        var result = await _userController.PostUserDataAsync(request) as OkResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task PostUserDataAsync_ReturnsBadRequest_WhenPostFails()
    {
        // Arrange
        var request = new UserCardRequest { Id = 0 };
        _userServiceMock.Setup(service => service.PostUserDataAsync(request)).ReturnsAsync(false);

        // Act
        var result = await _userController.PostUserDataAsync(request) as BadRequestResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public async Task DeleteUserAsync_ReturnsOk_WhenUserIsDeleted()
    {
        // Arrange
        var userId = 1;
        _userServiceMock.Setup(service => service.DeleteUserAsync(userId)).ReturnsAsync(true);

        // Act
        var result = await _userController.DeleteUserAsync(userId) as OkResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task DeleteUserAsync_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = 1;
        _userServiceMock.Setup(service => service.DeleteUserAsync(userId)).ReturnsAsync(false);

        // Act
        var result = await _userController.DeleteUserAsync(userId) as NotFoundResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

}

