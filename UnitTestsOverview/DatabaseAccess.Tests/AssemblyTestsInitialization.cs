using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseAccess.Tests
{
    [TestClass]
    public class AssemblyTestsInitialization
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            var basePath = AppContext.BaseDirectory;

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json");

            var config = configBuilder.Build();

            ConfigurationProvider.Configuration = config;

            var dbContext = CreateTestDataContext();

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            var dbContext = CreateTestDataContext();
            dbContext.Database.EnsureDeleted();
        }

        private static DatabaseAccess.DataContext CreateTestDataContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DatabaseAccess.DataContext>()
                .UseNpgsql(ConfigurationProvider.ConnectionString)
                .Options;

            var dbContext = new DatabaseAccess.DataContext(dbContextOptions);
            return dbContext;
        }
    }
}
