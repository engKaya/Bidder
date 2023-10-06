using Bidder.Application.Common.Extension.ValueTypeExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.Elasticsearch; 

namespace Bidder.Application.Common.Extension
{
    public static class ElasticSearchContext
    {
        public static void AddElasticWithSerilog(this IServiceCollection services, string assemblyName, IConfiguration configure, string env)
        { 
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.Console()
        .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Day).WriteTo.Elasticsearch(ConfigureElasticSink(assemblyName,configure, env))
                .Enrich.WithProperty("Environment", env)
                .ReadFrom.Configuration(configure)
                .CreateLogger();
            services.AddSerilog();
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(string assemblyName, IConfiguration configuration, string environment)
        { 
            string indexFormat = $"{assemblyName.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}";
             
            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = indexFormat.RemoveTurkishChars(),
            };
        } 
    }
}
