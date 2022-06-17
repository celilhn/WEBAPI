using Application.Interfaces;
using Application.Models;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using Application.ViewModels;
using static Domain.Constants.Constants;

namespace Application.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public ApiAuthorizeAttribute()
        {
            
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string userID = "";
            UserDto user = null;
            IJwtService jwtService = context.HttpContext.RequestServices.GetService<IJwtService>();
            IUserService userService = context.HttpContext.RequestServices.GetService<IUserService>();
            string authHeader = (string)context.HttpContext.Request.Headers["Authorization"];
            if (authHeader != null && (authHeader.Contains("Bearer ") || authHeader.Contains("bearer ")))
            {
                authHeader = authHeader.Replace("Bearer ", "").Replace("bearer ", "");
            }
            userID = jwtService.ValidateAccessToken(authHeader);
            if(userID == "")
            {
                CommonApiResponse.Create(ProcessResults.Authorization, null, "");
                context.Result = new UnauthorizedResult();
            }
            else
            {
                user = userService.GetUser(Convert.ToInt32(userID));
                if (user != null)
                {
                    context.HttpContext.Items["User"] = user;
                }
                else
                {
                    context.Result = new UnauthorizedResult();
                }                
            }
        }
    }
}
