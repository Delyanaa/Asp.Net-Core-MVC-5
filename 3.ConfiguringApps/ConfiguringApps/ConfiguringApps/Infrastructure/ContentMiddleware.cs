using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfiguringApps.Infrastructure
{
    /// <summary>
    ///  Content(for clients)-generating middleware component without the complexity of MVC
    /// </summary>
    public class ContentMiddleware
    {
        private RequestDelegate nextDelegate;
        private UptimeService uptimeService;

        public ContentMiddleware(RequestDelegate next, UptimeService service)
        {
            nextDelegate = next;
            uptimeService = service;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.ToString().ToLower() == "/middleware")
            {
                await httpContext.Response.WriteAsync(
                    "This is from the content middleware " +
                    $"(uptime: {uptimeService.Uptime}ms)", Encoding.UTF8);
            }
            else
            {
                await nextDelegate.Invoke(httpContext);
            }
        }
    }
}
