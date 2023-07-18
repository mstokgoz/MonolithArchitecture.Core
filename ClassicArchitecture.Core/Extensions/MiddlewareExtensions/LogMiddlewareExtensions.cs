using ClassicArchitecture.Core.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.Extensions.MiddlewareExtensions
{
    public static class LogMiddlewareExtensions
    {
        public static IApplicationBuilder UseElasticLogMiddleware(this IApplicationBuilder app) => app.UseMiddleware<LogMiddleware>();

        
    }
}
