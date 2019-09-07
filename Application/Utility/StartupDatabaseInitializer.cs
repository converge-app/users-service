using System;
using Application.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Application.Utility
{
    public class StartupDatabaseInitializer
    {
        public static void InitializeDatabase(IServiceCollection services)
        {

            var databaseSettings = new DatabaseSettings();
            databaseSettings.ReadFromEnvironment();
            var config = databaseSettings.GetConfiguration();

            services.Configure<DatabaseSettings>(config);

            services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
        }
    }
}