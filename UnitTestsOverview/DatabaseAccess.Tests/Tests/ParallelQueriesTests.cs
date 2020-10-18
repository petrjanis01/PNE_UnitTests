using System;
using System.Collections.Generic;
using System.Text;
using DatabaseAccess.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;

namespace DatabaseAccess.Tests.Tests
{
    [TestClass]
    public class ParallelQueriesTests : TestDatabaseAccessBase
    {
        [TestMethod]
        public void ParallelQueries_Always_Crash()
        {
            // Arrange

            var user = RandomDataEntityGenerator.CreateUserWithRandomData();
            for (int postIndex = 0; postIndex < 10; postIndex++)
            {
                var post = RandomDataEntityGenerator.CreatePostWithRandomData(false);

                user.Posts.Add(post);
            }

            TestDataContext.Add(user);
            TestDataContext.SaveChanges();

            // Act / Assert
            using (var dbContext = CreateTestDataContext())
            {
                var postsQuery = dbContext.Posts;

                foreach (var post in postsQuery)
                {
                    // throws an error because same transaction is used = command is already in progress
                    Assert.ThrowsException<NpgsqlOperationInProgressException>(() =>
                    {
                        var firstName = post.User.FirstName;
                    });
                    var posText = post.Text;
                }
            }
        }

        [TestMethod]
        public void ParallelQueries_OtherContext_ShouldBeOK()
        {
            // Arrange
            var user = RandomDataEntityGenerator.CreateUserWithRandomData();
            for (int postIndex = 0; postIndex < 10; postIndex++)
            {
                var post = RandomDataEntityGenerator.CreatePostWithRandomData(false);

                user.Posts.Add(post);
            }

            TestDataContext.Add(user);
            TestDataContext.SaveChanges();

            // Act / Assert
            using (DataContext context = CreateTestDataContext()
                , context2 = CreateTestDataContext())
            {
                var postsQuery = context.Posts;

                foreach (var post in postsQuery)
                {
                    context2.Attach(post).Reference(x => x.User).Load();
                    var firstName = post.User.FirstName;
                    var postText = post.Text;
                }
            }
        }
    }
}
