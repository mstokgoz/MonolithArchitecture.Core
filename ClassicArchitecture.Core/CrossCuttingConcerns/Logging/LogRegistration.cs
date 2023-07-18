using ClassicArchitecture.Core.CrossCuttingConcerns.Logging.Elasticsearch;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.CrossCuttingConcerns.Logging
{
    public static class LogRegistration
    {
        public static void AddElasticSearchLog(this WebApplicationBuilder builder, string appName)
        {
            builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Elasticsearch(
                new ElasticsearchSinkOptions(node: new Uri(ElasticEnvironmentManager.ElasticUrl))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = ElasticEnvironmentManager.IndexFormat, //"project-kubernetes-kafka"
                    ModifyConnectionSettings = x => x.BasicAuthentication(ElasticEnvironmentManager.ElasticUser, ElasticEnvironmentManager.ElasticPassword)
                    .ServerCertificateValidationCallback((o, cer, arg3, arg4) => { return true; })
                }
                )
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithClientIp()
                .Enrich.WithProperty("AppName", appName)
                .Enrich.WithCorrelationId(CorrelationId)
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning));

            builder.Services.AddHttpContextAccessor();
        }

        public static string? CorrelationId
        {
            get { return (string?)Activity.Current?.GetCustomProperty("CorrelationId"); }
        }
        
    }
}
