using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfiguringApps.Infrastructure
{
    /// <summary>
    ///  doesn’t generate a response. Instead, it changes requests
    ///  before they reach other components later in the chain
    /// </summary>
    public class BrowserTypeMiddleware
    {
        private RequestDelegate nextDelegate; 

        public BrowserTypeMiddleware(RequestDelegate next) => nextDelegate = next;
        
        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Items["EdgeBrowser"]
            = httpContext.Request.Headers["User-Agent"]
            .Any(v => v.ToLower().Contains("edge"));
            await nextDelegate.Invoke(httpContext);
        }
    }
}
