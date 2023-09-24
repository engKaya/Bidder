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
            
            indexFormat = RemoveTurkishChars(indexFormat);
            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = indexFormat,
            };
        }

        private static string RemoveTurkishChars(string str)
        {
            str = str.Trim();
            if (str.Length > 0)
                str = str.Replace("İ", "I").Replace("ı", "i").Replace("Ğ", "G").Replace("ğ", "g").Replace("Ü", "U").Replace("ü", "u").Replace("Ş", "S").Replace("ş", "s").Replace("Ö", "O").Replace("ö", "o").Replace("Ç", "C").Replace("ç", "c");

            return str;
        }
    }
}
