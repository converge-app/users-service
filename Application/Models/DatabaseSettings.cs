using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Application.Models
{
    public interface IDatabaseSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        MongoCredential Credentials { get; set; }

        void ReadFromEnvironment();

        IConfiguration GetConfiguration();

        MongoClientSettings GetSettings();
    }

    public class DatabaseSettings : IDatabaseSettings
    {
        private const string _collectionName = "CollectionName";
        private const string _connectionString = "ConnectionString";
        private const string _databaseName = "DatabaseName";
        private const string _mongoUsername = "MONGO_INITDB_ROOT_USERNAME";
        private const string _mongoPassword = "MONGO_INITDB_ROOT_PASSWORD";

        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public MongoCredential Credentials { get; set; }

        public void ReadFromEnvironment()
        {
            CollectionName = Environment.GetEnvironmentVariable(_collectionName);
            ConnectionString = Environment.GetEnvironmentVariable(_connectionString);
            DatabaseName = Environment.GetEnvironmentVariable(_databaseName);
            try
            {
                Credentials = MongoCredential.CreateCredential(
                    "ApplicationDb",
                    Environment.GetEnvironmentVariable(_mongoUsername),
                    Environment.GetEnvironmentVariable(_mongoPassword));
            }
            catch (Exception)
            {
                throw new EnvironmentNotSet("Mongodb credentials not given");
            }

            if (string.IsNullOrEmpty(CollectionName) ||
                string.IsNullOrEmpty(ConnectionString) ||
                string.IsNullOrEmpty(DatabaseName) ||
                Credentials == null)
                throw new EnvironmentNotSet("Database variables not set");
        }

        public IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .AddEnvironmentVariables(_collectionName)
                .AddEnvironmentVariables(_connectionString)
                .AddEnvironmentVariables(_databaseName)
                .AddEnvironmentVariables(_mongoUsername)
                .AddEnvironmentVariables(_mongoPassword)
                .Build();
        }

        public MongoClientSettings GetSettings()
        {
            ReadFromEnvironment();

            return new MongoClientSettings
            {
                Credential = Credentials,
                Server = new MongoServerAddress("localhost", 27017)
            };
        }
    }
}