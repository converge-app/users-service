using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public class StartupDevelopment
    {
        public StartupDevelopment(IConfiguration configuration)
        {
            // ElasticSearch
            Environment.SetEnvironmentVariable("ELASTICSEARCH_URI", "http://localhost:9200");

            // Database
            Environment.SetEnvironmentVariable("CollectionName", "Models");
            Environment.SetEnvironmentVariable("ConnectionString", "mongodb://localhost:27017");
            Environment.SetEnvironmentVariable("DatabaseName", "ApplicationDb");
            Environment.SetEnvironmentVariable("MONGO_INITDB_ROOT_USERNAME", "application");
            Environment.SetEnvironmentVariable("MONGO_INITDB_ROOT_PASSWORD", "password");

            Environment.SetEnvironmentVariable("MONGO_SERVICE_NAME", "localhost");
            Environment.SetEnvironmentVariable("MONGO_SERVICE_PORT", "27017");

            _startup = new Startup(configuration);
        }

        private Startup _startup;

        public void ConfigureServices(IServiceCollection services)
        {
            _startup.ConfigureServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            _startup.Configure(app, env);
        }
    }
}