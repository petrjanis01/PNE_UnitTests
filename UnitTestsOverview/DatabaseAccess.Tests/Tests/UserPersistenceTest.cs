using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DatabaseAccess.Tests.Tests
{
    [TestClass]
    public class UserPersistenceTest : TestDatabaseAccessBase
    {
        [TestMethod]
        public void SaveAndLoad_AllDataShouldBePresent()
        {
            // Arrange
            var memoryUser = RandomDataEntityGenerator.CreateUserWithRandomData();

            // Act
            using (var dbContext = CreateTestDataContext())
            {
                dbContext.Users.Add(memoryUser);
                dbContext.SaveChanges();
            }

            var databaseUser = TestDataContext.Users.FirstOrDefault(u => u.Id == memoryUser.Id);

            // Assert
            Assert.IsNotNull(databaseUser);
            Assert.AreEqual(memoryUser.FirstName, databaseUser.FirstName);
            Assert.AreEqual(memoryUser.Surname, databaseUser.Surname);
            Assert.AreEqual(memoryUser.Birthdate, databaseUser.Birthdate);
            Assert.AreEqual(memoryUser.Email, databaseUser.Email);
        }

        [TestMethod]
        public void Delete_ResultShouldBeNull()
        {
            // Arrange
            var memoryUser = RandomDataEntityGenerator.CreateUserWithRandomData();
            using (var dbContext = CreateTestDataContext())
            {
                dbContext.Users.Add(memoryUser);
                dbContext.SaveChanges();
            }

            // Act
            using (var dbContext = CreateTestDataContext())
            {
                var user = TestDataContext.Users.FirstOrDefault(u => u.Id == memoryUser.Id);
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
            }

            var databaseUser = TestDataContext.Users.FirstOrDefault(u => u.Id == memoryUser.Id);

            // Assert
            Assert.IsNull(databaseUser);
        }
    }
}
