using DOTNETDemo.Models.Entities;
using DOTNETDemo.Models.Request;
using DOTNETDemo.Repositorys.UserRepository;
using DOTNETDemo.Services.UserService;
using Microsoft.EntityFrameworkCore;

namespace Stanza.AzureFunctions.Services.UserService;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<User> GetUserAsyncById(int id)
    {
        return await _userRepository.GetUserAsyncById(id);
    }

    public async Task<bool> PostUserDataAsync(UserCardRequest request)
    {
        try
        {
            var user = new User
            {
                Name = request.Name ?? string.Empty,
                CardNumber = request.CardNumber,
                CVC = request.CVC,
                ExpiryDate = request.ExpiryDate
            };

            await _userRepository.AddUserAsync(user);
            return true;
        }
        catch (Exception)
        {
            // Log exception details here if needed
            return false;
        }
    }


    public async Task<bool> UpdateUserAsync(UserCardRequest request)
    {
        try
        {
            var user = await _userRepository.GetUserAsyncById(request.Id);
            if (user == null)
            {
                return false; // Handle the case where the user is not found
            }

            // Update user fields
            user.Name = request.Name;
            user.CardNumber = request.CardNumber;
            user.CVC = request.CVC;
            user.ExpiryDate = request.ExpiryDate;

            var result = await _userRepository.UpdateUserAsync(user);
            return result;
        }
        catch (Exception)
        {
            // Log exception details here if needed
            return false;
        }
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        try
        {
            var user = await _userRepository.GetUserAsyncById(id);
            if (user == null)
            {
                return false; // User not found
            }

            await _userRepository.DeleteUserAsync(user);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
