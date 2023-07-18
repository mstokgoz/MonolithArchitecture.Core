using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.Middlewares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string correlationId = await CorrelationProcess(context);
            Activity.Current?.SetCustomProperty("CorrelationId", correlationId);

            using (Serilog.Context.LogContext.PushProperty("CorrelationId", correlationId))
                await _next(context);
        }
        private async Task<string> CorrelationProcess(HttpContext context)
        {
            string correlationId = "";
            context.Request.Headers.TryGetValue("x-correlation-id", out var values);

            return values.Any() ? (correlationId = values.First() ?? "") : (correlationId = Guid.NewGuid().ToString());           
        }

    }
}
