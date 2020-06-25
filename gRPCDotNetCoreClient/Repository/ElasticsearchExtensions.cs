using System;
using gRPCDotNetCoreClient.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace gRPCDotNetCoreClient.Repository
{
    public static class ElasticsearchExtensions
    {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            var defaultIndexName = configuration["elasticsearch:defaultIndex"];
            var indexName = configuration["elasticsearch:index"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndexName)
                .DefaultMappingFor<BookDto>(m => m
                    .IndexName(indexName)
                    .PropertyName(p => p.Id, "Id")
                    .PropertyName(p => p.Name, "Name")
                    .PropertyName(p => p.Author, "Author")
                    .PropertyName(p => p.Cost, "Cost")
                    .PropertyName(p => p.Pages, "Pages")
                );


            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
        
        }
    }
}