using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure
{
    /// <summary>
    ///  Generates a URL that the browser will be returned to after
    ///  the cart has been updated, taking into account the query string if there is one.
    /// </summary>
    public static class UrlExtensions
    {
        public static string PathAndQuery(this HttpRequest request) =>
         request.QueryString.HasValue
         ? $"{request.Path}{request.QueryString}"
         : request.Path.ToString();
    }
}
