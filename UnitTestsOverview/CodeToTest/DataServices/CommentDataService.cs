using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseAccess;
using DatabaseAccess.Entities;

namespace CodeToTest.DataServices
{
    public class CommentDataService : ICommentDataService
    {
        private readonly DataContext _dbContext;

        public CommentDataService(DataContext dataContext)
        {
            _dbContext = dataContext;
        }

        public List<Comment> GetAllCommentsCreatedInTimeRange(DateTime from, DateTime until)
        {
            var comments = _dbContext.Comments
                .Where(c => c.CreatedDateTime >= from && c.CreatedDateTime <= until);
            return comments.ToList();
        }
    }
}
