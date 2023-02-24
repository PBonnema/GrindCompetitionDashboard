using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Threading.Tasks;

namespace GrindCompetitionAPI.Middleware
{
    public class ExceptionHandlerComponent
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerComponent(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to process request for {Url}", context.Request.Host);

                try
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject("Failed to process request"));
                }
                catch { /* Do nothing */ }
            }
        }
    }
}
