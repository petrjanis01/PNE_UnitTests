using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseAccess.Tests
{
    public abstract class TestDatabaseAccessBase
    {
        protected DataContext TestDataContext { get; private set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            // This method is called before every test.
            // Here can be for example started db transaction and TestDataContext can be created inside context of that transaction.
            // All operations in test can be done inside that particular transaction.

            TestDataContext = CreateTestDataContext();
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            // This method is called after every test.
            // If TestDataContext was created within transaction context all changes done in test can be undone by transaction rollback.
        }

        public static DataContext CreateTestDataContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DatabaseAccess.DataContext>()
                .UseNpgsql(ConfigurationProvider.ConnectionString)
                .Options;

            var dbContext = new DatabaseAccess.DataContext(dbContextOptions);
            return dbContext;
        }
    }
}
