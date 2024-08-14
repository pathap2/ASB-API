using DOTNETDemo.Models.Entities;

namespace DOTNETDemo.Repositorys.UserRepository;

public interface IUserRepository
{
    Task<User> GetUserAsyncById(int id);
    Task<bool> AddUserAsync(User user);

    Task<bool> UpdateUserAsync(User user);

    Task DeleteUserAsync(User user);
}
