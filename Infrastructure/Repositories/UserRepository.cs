using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TraDbContext context;
        public UserRepository(TraDbContext context)
        {
            this.context = context;
        }

        public User GetUser(string email, string password)
        {
            return context.Users.SingleOrDefault(x => x.Email == email && x.Password == password);
        }

        public User GetUser(int userId)
        {
            return context.Users.SingleOrDefault(x => x.Id == userId);
        }
    }
}
