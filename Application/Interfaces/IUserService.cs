using Application.ViewModels;
using System.Collections.Generic;
using Application.Models;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Token Authenticate(string mail, string password);
        UserDto GetUser(string email, string password);
        UserDto GetUser(int userId);
    }
}
