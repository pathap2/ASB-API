using DOTNETDemo.Models.Entities;
using DOTNETDemo.Models.Request;

namespace DOTNETDemo.Services.UserService;

public interface IUserService
{
    Task<User> GetUserAsyncById(int id);
    Task<bool> PostUserDataAsync(UserCardRequest request);
    Task<bool> DeleteUserAsync(int id);
    Task<bool> UpdateUserAsync(UserCardRequest request);
}
