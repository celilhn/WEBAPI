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

        public User AddUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        public User GetUser(string email, string password)
        {
            return context.Users.SingleOrDefault(x => x.Email == email && x.Password == password);
        }

        public User GetUser(int userId)
        {
            return context.Users.SingleOrDefault(x => x.Id == userId);
        }

        public User UpdateUser(User user)
        {
            user.InsertionDate = DateTime.Now;
            user.UpdateDate = DateTime.Now;
            user.Status = 1;
            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();
            return user;
        }
    }
}
