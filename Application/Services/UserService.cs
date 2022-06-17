using Application.Interfaces;
using Application.Utilities;
using Application.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public UserDto Authenticate(string mail, string password)
        {
            //User user = userRepository.GetUser(mail, AppUtilities.EncryptSHA256(password));
            User user = userRepository.GetUser(mail, password);
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(AppUtilities.GetConfigurationValue("JwtToken"));
            var key = Encoding.ASCII.GetBytes("34yujjhtrt78o905yt4wesdrgthyjukılo809787");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("Email", user.Email.ToString()),
                    new Claim("Password", user.Password.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            UserDto userDto = mapper.Map<UserDto>(user);
            userDto.Token = tokenHandler.WriteToken(token);

            return userDto;
        }

        public UserDto GetUser(int userId)
        {
            UserDto user = mapper.Map<UserDto>(userRepository.GetUser(userId));
            return user;
        }

        public UserDto GetUser(string email, string password)
        {
            UserDto user = mapper.Map<UserDto>(userRepository.GetUser(email, password));
            return user;
        }

        public UserDto saveUser(UserDto user)
        {
            if (user.Id > 0)
            {
                user = mapper.Map<UserDto>(userRepository.UpdateUser(mapper.Map<User>(user)));
            }
            else
            {
                user = mapper.Map<UserDto>(userRepository.AddUser(mapper.Map<User>(user)));
            }
            return user;
        }
    }
}
