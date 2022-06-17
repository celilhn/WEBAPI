using Application.Logging;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Middleware
{
    public class RequestHandler
    {
        private readonly RequestDelegate next;
        
        public RequestHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            ILoggerManager logger;
            try
            {
                logger = (ILoggerManager)context.RequestServices.GetService(typeof(ILoggerManager));
                context.Request.EnableBuffering();
                var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
                await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
                var requestBody = Encoding.UTF8.GetString(buffer);
                context.Request.Body.Seek(0, SeekOrigin.Begin);
                if (requestBody != "")
                {
                    logger.LogInfo(
                        Environment.NewLine +
                        Environment.NewLine + "*** REQUEST ***" +
                        Environment.NewLine + "RequestStartDate: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") +
                        Environment.NewLine + "IP Address: " + context.Connection.RemoteIpAddress.ToString() +
                        Environment.NewLine + "UserAgent: " + context.Request.Headers["User-Agent"].ToString() +
                        Environment.NewLine + requestBody
                    );
                }
                var originalBodyStream = context.Response.Body;
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;
                    await next(context);
                    context.Response.Body.Seek(0, SeekOrigin.Begin);
                    var response = await new StreamReader(context.Response.Body).ReadToEndAsync();
                    context.Response.Body.Seek(0, SeekOrigin.Begin);
                    if (response != "")
                    {
                        logger.LogInfo(
                            Environment.NewLine + "*** RESPONSE ***" +
                            Environment.NewLine + "RequestEndDate:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") +
                            Environment.NewLine + "HttpStatusCode: " + context.Response.StatusCode +
                            Environment.NewLine + response
                        );
                    }
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
