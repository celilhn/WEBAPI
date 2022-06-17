using Application.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using static Domain.Constants.Constants;

namespace Application.Middleware
{
    public class ResponseHandler
    {
        private readonly RequestDelegate next;

        public ResponseHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            ProcessResults processResults = ProcessResults.Success;
            int statusCode = 0;
            string message = "";
            try
            {
                statusCode = context.Response.StatusCode;
                

                var currentBody = context.Response.Body;
                using (var memoryStream = new MemoryStream())
                {
                    context.Response.Body = memoryStream;
                    await next(context);
                    context.Response.Body = currentBody;
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    string readToEnd = new StreamReader(memoryStream).ReadToEnd();
                    var objResult = JsonSerializer.Deserialize<object>(readToEnd);

                    bool statusCodeResult = false;
                    statusCodeResult = Int32.TryParse("" + context.Response.HttpContext.Items["StatusCode"], out statusCode);
                    if (statusCodeResult)
                    {
                        message = "" + context.Items["Message"];
                    }
                    else
                    {
                        statusCode = context.Response.StatusCode;
                    }

                    var result = CommonApiResponse.Create(processResults, objResult, message);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(result));
                }
            }
            catch (Exception ex)
            {
               //do nothing
            }
           
        }

    }
}

