using DOTNETDemo.Models.Entities;
using DOTNETDemo.Models.Request;
using DOTNETDemo.Repositorys.UserRepository;
using DOTNETDemo.Services.UserService;
using Moq;
using Stanza.AzureFunctions.Services.UserService;

namespace DOTNETDemo.Tests.Service.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task GetUserAsyncById_ReturnsUser_WhenUserExists()
    {
        // Arrange
        var userId = 1;
        var user = new User { Id = userId };
        _userRepositoryMock.Setup(repo => repo.GetUserAsyncById(userId)).ReturnsAsync(user);

        // Act
        var result = await _userService.GetUserAsyncById(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.Id);
    }

    [Fact]
    public async Task GetUserAsyncById_ReturnsNull_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = 1;
        _userRepositoryMock.Setup(repo => repo.GetUserAsyncById(userId)).ReturnsAsync((User)null);

        // Act
        var result = await _userService.GetUserAsyncById(userId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task PostUserDataAsync_UpdatesUser_WhenUserExists()
    {
        // Arrange
        var request = new UserCardRequest
        {
            Id = 1,
            CardNumber = "1234567812345678",
            CVC = "123",
            ExpiryDate = DateTime.UtcNow.AddYears(1)
        };

        var existingUser = new User { Id = request.Id };
        _userRepositoryMock.Setup(repo => repo.GetUserAsyncById(request.Id)).ReturnsAsync(existingUser);
        _userRepositoryMock.Setup(repo => repo.AddUserAsync(It.IsAny<User>())).ReturnsAsync(true);

        // Act
        var result = await _userService.PostUserDataAsync(request);

        // Assert
        Assert.True(result);
        Assert.Equal(request.CardNumber, existingUser.CardNumber);
        Assert.Equal(request.CVC, existingUser.CVC);
        Assert.Equal(request.ExpiryDate, existingUser.ExpiryDate);
    }

    [Fact]
    public async Task PostUserDataAsync_AddsUser_WhenIdIsNotProvided()
    {
        // Arrange
        var request = new UserCardRequest
        {
            Id = 0,
            CardNumber = "1234567812345678",
            CVC = "123",
            ExpiryDate = DateTime.UtcNow.AddYears(1)
        };

        _userRepositoryMock.Setup(repo => repo.AddUserAsync(It.IsAny<User>())).ReturnsAsync(true);

        // Act
        var result = await _userService.PostUserDataAsync(request);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task PostUserDataAsync_ReturnsFalse_WhenUserDoesNotExist()
    {
        // Arrange
        var request = new UserCardRequest
        {
            Id = 1,
            CardNumber = "1234567812345678",
            CVC = "123",
            ExpiryDate = DateTime.UtcNow.AddYears(1)
        };

        _userRepositoryMock.Setup(repo => repo.GetUserAsyncById(request.Id)).ReturnsAsync((User)null);

        // Act
        var result = await _userService.PostUserDataAsync(request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task PostUserDataAsync_ReturnsFalse_OnException()
    {
        // Arrange
        var request = new UserCardRequest
        {
            Id = 1,
            CardNumber = "1234567812345678",
            CVC = "123",
            ExpiryDate = DateTime.UtcNow.AddYears(1)
        };

        _userRepositoryMock.Setup(repo => repo.GetUserAsyncById(request.Id)).ThrowsAsync(new Exception());

        // Act
        var result = await _userService.PostUserDataAsync(request);

        // Assert
        Assert.False(result);
    }
}
