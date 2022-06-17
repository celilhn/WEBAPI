using Application.Extensions;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.Filters
{
    public class BasicAuthorizeAttribute : ActionFilterAttribute, IActionFilter
    {
        public BasicAuthorizeAttribute()
        {
            
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            UserDto userDto = null;
            try
            {
                userDto = context.HttpContext.Session.GetObjectFromJson<UserDto>("User");
                context.HttpContext.Items.Add("User", userDto);
            }
            catch
            {
                userDto = null;
            }

            if (userDto != null && userDto.Id > 0)
            {                
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
        }
    }
}
