using DatabaseAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeToTest.Tests
{
    public abstract class TestDatabaseAccessBase
    {
        protected DataContext TestDataContext { get; private set; }

        protected TestDatabaseAccessBase()
        {
            TestDataContext = CreateInMemoryDataContext();
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            TestDataContext.Database.EnsureDeleted();
        }

        public static DataContext CreateInMemoryDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var dbContext = new DataContext(options);
            return dbContext;
        }
    }
}
