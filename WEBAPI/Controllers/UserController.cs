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
            Token tokenn = null;
            try
            {
                tokenn = userService.Authenticate(email, password);
                processResults = ProcessResults.Success;
                HttpContext.Response.HttpContext.Items.Add("StatusCode", (int)processResults);
                if (tokenn == null)
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
            }

            if (processResults != ProcessResults.Unknown)
            {
                return new JsonResult(tokenn);
            }
            else
            {
                HttpContext.Response.HttpContext.Items.Add("StatusCode", (int)processResults);
                HttpContext.Response.HttpContext.Items.Add("Message", "İşlem yapılırken hata oluştu");
                return new JsonResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [ApiAuthorize]
        public JsonResult Login(string email, string password)
        {
            ProcessResults processResults = ProcessResults.Unknown;
            UserDto user = null;
            try
            {
                user = userService.GetUser(email, password);
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
                //return Ok(user);
                return new JsonResult(user);
            }
            else
            {
                HttpContext.Response.HttpContext.Items.Add("StatusCode", (int)processResults);
                HttpContext.Response.HttpContext.Items.Add("Message", "İşlem yapılırken hata oluştu");
                //return StatusCode((int)HttpStatusCode.InternalServerError);
                return new JsonResult(HttpStatusCode.InternalServerError);
            }
        }

    }
}
