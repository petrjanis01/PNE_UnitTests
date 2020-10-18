using System;
using System.Linq;
using CodeToTest.DataServices;
using DatabaseAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeToTest.Tests
{
    [TestClass]
    public class CommentDataServiceTest : TestDatabaseAccessBase
    {
        private ICommentDataService _SUT;

        [TestInitialize]
        public void TestInitialize()
        {
            _SUT = new CommentDataService(TestDataContext);
        }

        [TestMethod]
        public void GetAllCommentsCreatedInTimeRange_WhenAllInExclusiveRange_CorrectDataReturned()
        {
            // Arrange
            var until = DateTime.Now;
            var from = until.AddDays(-5);

            var comment1 = RandomDataEntityGenerator.CreateCommentWithRandomData();
            comment1.CreatedDateTime = from.AddDays(1);
            var comment2 = RandomDataEntityGenerator.CreateCommentWithRandomData();
            comment2.CreatedDateTime = from.AddDays(2);
            var comment3 = RandomDataEntityGenerator.CreateCommentWithRandomData();
            comment3.CreatedDateTime = from.AddDays(4);

            TestDataContext.Comments.AddRange(new[] { comment1, comment2, comment3 });
            TestDataContext.SaveChanges();

            // Act
            var result = _SUT.GetAllCommentsCreatedInTimeRange(from, until);

            // Assert
            Assert.IsNotNull(result.FirstOrDefault(c => c.Id == comment1.Id));
            Assert.IsNotNull(result.FirstOrDefault(c => c.Id == comment2.Id));
            Assert.IsNotNull(result.FirstOrDefault(c => c.Id == comment3.Id));
        }

        [TestMethod]
        public void GetAllCommentsCreatedInTimeRange_WhenAllInInclusiveRange_CorrectDataReturned()
        {
            // Arrange
            var until = DateTime.Now;
            var from = until.AddDays(-5);

            var comment1 = RandomDataEntityGenerator.CreateCommentWithRandomData();
            comment1.CreatedDateTime = until;
            var comment2 = RandomDataEntityGenerator.CreateCommentWithRandomData();
            comment2.CreatedDateTime = from.AddDays(2);
            var comment3 = RandomDataEntityGenerator.CreateCommentWithRandomData();
            comment3.CreatedDateTime = from;

            TestDataContext.Comments.AddRange(new[] { comment1, comment2, comment3 });
            TestDataContext.SaveChanges();

            // Act
            var result = _SUT.GetAllCommentsCreatedInTimeRange(from, until);

            // Assert
            Assert.IsNotNull(result.FirstOrDefault(c => c.Id == comment1.Id));
            Assert.IsNotNull(result.FirstOrDefault(c => c.Id == comment2.Id));
            Assert.IsNotNull(result.FirstOrDefault(c => c.Id == comment3.Id));
        }

        [TestMethod]
        public void GetAllCommentsCreatedInTimeRange_SomeAreOutOfRange_CorrectDataReturned()
        {
            // Arrange
            var until = DateTime.Now;
            var from = until.AddDays(-5);

            var outOfRangeComment = RandomDataEntityGenerator.CreateCommentWithRandomData();
            outOfRangeComment.CreatedDateTime = until.AddMinutes(1);
            var comment2 = RandomDataEntityGenerator.CreateCommentWithRandomData();
            comment2.CreatedDateTime = from.AddDays(2);
            var outOfRangeComment1 = RandomDataEntityGenerator.CreateCommentWithRandomData();
            outOfRangeComment1.CreatedDateTime = from.AddMinutes(-1);

            TestDataContext.Comments.AddRange(new[] { outOfRangeComment, comment2, outOfRangeComment1 });
            TestDataContext.SaveChanges();

            // Act
            var result = _SUT.GetAllCommentsCreatedInTimeRange(from, until);

            // Assert
            Assert.IsNull(result.FirstOrDefault(c => c.Id == outOfRangeComment.Id));
            Assert.IsNotNull(result.FirstOrDefault(c => c.Id == comment2.Id));
            Assert.IsNull(result.FirstOrDefault(c => c.Id == outOfRangeComment1.Id));
        }

        // TODO correct amount returned
    }
}
