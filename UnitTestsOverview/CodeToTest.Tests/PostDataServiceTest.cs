using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeToTest.DataServices;
using DatabaseAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CodeToTest.Tests
{
    [TestClass]
    public class PostDataServiceTest : TestDatabaseAccessBase
    {

        private IPostDataService _SUT;
        private Mock<ICommentDataService> _commentServiceMock;

        [TestInitialize]
        public void TestInitialize()
        {
            _commentServiceMock = new Mock<ICommentDataService>();
            _SUT = new PostDataService(TestDataContext, _commentServiceMock.Object);
        }

        [TestMethod]
        public void GetPostsWithCommentsCreatedInRange_WhenAllInExclusiveRange_CorrectDataReturned()
        {
            // Arrange
            _commentServiceMock
                .Setup(s => s.GetAllCommentsCreatedInTimeRange(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns((DateTime from, DateTime until) => TestDataContext.Comments.ToList());

            var until = DateTime.Now;
            var from = until.AddDays(-5);

            var comment1 = RandomDataEntityGenerator.CreateCommentWithRandomData();
            comment1.Post.CreatedTime = from.AddDays(1);
            var comment2 = RandomDataEntityGenerator.CreateCommentWithRandomData();
            comment2.Post.CreatedTime = from.AddDays(2);
            var comment3 = RandomDataEntityGenerator.CreateCommentWithRandomData();
            comment3.Post.CreatedTime = from.AddDays(4);

            TestDataContext.Comments.AddRange(new[] { comment1, comment2, comment3 });
            TestDataContext.SaveChanges();

            // Act
            var result = _SUT.GetPostsWithCommentsCreatedInRange(from, until);

            // Assert
            Assert.IsNotNull(result.FirstOrDefault(c => c.Id == comment1.PostId));
            Assert.IsNotNull(result.FirstOrDefault(c => c.Id == comment2.PostId));
            Assert.IsNotNull(result.FirstOrDefault(c => c.Id == comment3.PostId));
        }

        [TestMethod]
        public void GetPostsWithCommentsCreatedInRange_WhenMultipleCommentsPerPost_CorrectAmountReturned()
        {
            // Arrange
            _commentServiceMock
                .Setup(s => s.GetAllCommentsCreatedInTimeRange(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns((DateTime from, DateTime until) => TestDataContext.Comments.ToList());

            var until = DateTime.Now;
            var from = until.AddDays(-5);

            var user = RandomDataEntityGenerator.CreateUserWithRandomData();
            var post = RandomDataEntityGenerator.CreatePostWithRandomData(false);
            post.User = user;
            post.CreatedTime = from.AddDays(1);

            var comment1 = RandomDataEntityGenerator.CreateCommentWithRandomData(false);
            comment1.User = user;
            comment1.Post = post;

            var comment2 = RandomDataEntityGenerator.CreateCommentWithRandomData(false);
            comment2.User = user;
            comment2.Post = post;

            TestDataContext.Comments.AddRange(new[] { comment2, comment2 });
            TestDataContext.SaveChanges();

            // Act
            var result = _SUT.GetPostsWithCommentsCreatedInRange(from, until);

            // Assert
            Assert.AreEqual(1, result.Count(p => p.Id == post.Id));
        }
    }
}
