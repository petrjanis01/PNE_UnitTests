using DatabaseAccess;
using Microsoft.EntityFrameworkCore;

namespace CodeToTest.Tests
{
    public abstract class TestDatabaseAccessBase
    {
        protected DataContext TestDataContext { get; private set; }

        protected TestDatabaseAccessBase()
        {
            TestDataContext = CreateInMemoryDataContext();
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
