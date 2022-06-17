using Application.Filters;
using Application.Interfaces;
using Application.Models;
using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using static Domain.Constants.Constants;

namespace MobileApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[ApiAuthorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public JsonResult Authentication(string email, string password)
        {
            ProcessResults processResults = ProcessResults.Unknown;
            UserDto user = null;
            Token tokenn = null;
            try
            {
                user = userService.Authenticate("celilhnkadioglu@gmail.com", "2345");
                //user = userService.saveUser(user);
                tokenn = new Token();
                tokenn.token = user.Token;
                processResults = ProcessResults.Success;
                HttpContext.Response.HttpContext.Items.Add("StatusCode", (int)processResults);
                if (user == null)
                {
                    HttpContext.Response.HttpContext.Items.Add("Message", "Bu kullanıcı'ya ait veri bulunamamıştır.");
                }
                else
                {
                    HttpContext.Response.HttpContext.Items.Add("Message", "Token işlemi başlatılmıştır.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            if (processResults != ProcessResults.Unknown)
            {
                //return Ok(tokenn);
                return new JsonResult(tokenn.token);
            }
            else
            {
                HttpContext.Response.HttpContext.Items.Add("StatusCode", (int)processResults);
                HttpContext.Response.HttpContext.Items.Add("Message", "İşlem yapılırken hata oluştu");
                //return StatusCode((int)HttpStatusCode.InternalServerError);
                return new JsonResult(tokenn);
            }
        }

        [HttpPost]
        [ApiAuthorize]
        public IActionResult Login(string email, string password)
        {
            ProcessResults processResults = ProcessResults.Unknown;
            UserDto user = null;
            try
            {
                user = userService.GetUser("celilhnkadioglu@gmail.com", "2345");
                processResults = ProcessResults.Success;
                if (user == null)
                {
                    HttpContext.Response.HttpContext.Items.Add("StatusCode", (int)processResults);
                    HttpContext.Response.HttpContext.Items.Add("Message", "Bu kullanıcı'ya ait veri bulunamamıştır.");
                }
                else
                {
                    HttpContext.Response.HttpContext.Items.Add("StatusCode", (int)processResults);
                    HttpContext.Response.HttpContext.Items.Add("Message", "Login işlemi başlamıştır.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            if (processResults != ProcessResults.Unknown)
            {
                return Ok(user);
            }
            else
            {
                HttpContext.Response.HttpContext.Items.Add("StatusCode", (int)processResults);
                HttpContext.Response.HttpContext.Items.Add("Message", "İşlem yapılırken hata oluştu");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
