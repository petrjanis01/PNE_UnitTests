using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseAccess.Tests.Tests
{
    [TestClass]
    public class LazyLoadingTest : TestDatabaseAccessBase
    {
        [TestMethod]
        public void LazyLoad_ForeignKeyNotNullable_FetchesProperly()
        {
            // Arrange
            var comment = RandomDataEntityGenerator.CreateCommentWithRandomData();
            using (var dbContext = CreateTestDataContext())
            {
                dbContext.Comments.Add(comment);
                dbContext.SaveChanges();
            }
            // Act
            var dbComment = TestDataContext.Comments.Find(comment.Id);
            // lazy loading
            var post = dbComment.Post;
            // second level
            var userOfPost = post.User;

            // Assert
            Assert.IsNotNull(post);
            Assert.IsNotNull(userOfPost);
        }
    }
}
