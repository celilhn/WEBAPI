using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using Application.Models;
using Application.Logging;
using System.Net.Mail;
using System.Net;
using System;

namespace Application.Utilities
{
    public static class AppUtilities
    {
        private static IConfiguration _configuration;  
   
        public static void AppUtilitiesConfigure(IConfiguration configuration)
        {
            _configuration = configuration;          
        }

        public static string GetConfigurationValue(string key)
        {
            return _configuration.GetSection(key).Value;
        }

        public static string EncryptSHA256(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
