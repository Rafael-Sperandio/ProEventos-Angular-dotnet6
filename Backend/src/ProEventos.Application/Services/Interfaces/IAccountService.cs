using Microsoft.AspNetCore.Identity;
using ProEventos.Application.Dtos;


namespace ProEventos.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> UserExists(string username);
        Task<UserUpdateDto> GetUserByUserNameAsync(string username);

        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password);
        Task<UserUpdateDto> CreateAccountAsync(UserDto userDto);
        //UserDto
        Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto);
    }
}
