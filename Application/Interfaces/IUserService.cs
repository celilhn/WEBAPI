using Application.ViewModels;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IUserService
    {
        UserDto Authenticate(string mail, string password);
        UserDto GetUser(string email, string password);
        UserDto GetUser(int userId);
        UserDto saveUser(UserDto user);
    }
}
