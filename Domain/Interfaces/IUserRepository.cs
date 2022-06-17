using Domain.Models;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        User AddUser(User user);
        User GetUser(string email, string password);
        User GetUser(int userId);
        User UpdateUser(User user);
    }
}
