using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchWebApi.Elastic
{
    public static class ElasticSearchServiceExtension
    {

        public static IServiceCollection AddElasticSearch(this IServiceCollection services, ConnectionSettings ESConnectionSettings)
        {

            var esClient = new ElasticClient(ESConnectionSettings);
            if (esClient == null)
                throw new Exception("Elastic Connectionstring not supplied");

            services.AddSingleton<IElasticClient>(esClient);
            return services;
        }
    }
}
