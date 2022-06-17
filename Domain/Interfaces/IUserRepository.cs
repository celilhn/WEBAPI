using Domain.Models;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(string email, string password);
        User GetUser(int userId);
    }
}
