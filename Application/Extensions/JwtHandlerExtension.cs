using Application.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Application.Extensions
{
    public static class JwtHandlerExtension
    {
        public static IApplicationBuilder UseJwtHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtHandler>();
        }
    }
}
