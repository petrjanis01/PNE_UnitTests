using System;
using Microsoft.Extensions.Configuration;

namespace DatabaseAccess.Tests
{
    public static class ConfigurationProvider
    {
        public static IConfiguration Configuration { get; set; }

        public static string ConnectionString
        {
            get
            {
                var connectionString = Configuration.GetConnectionString("TestContext");
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Could not find a connection string named 'TestContext'");
                }

                return connectionString;
            }
        }
    }
}
