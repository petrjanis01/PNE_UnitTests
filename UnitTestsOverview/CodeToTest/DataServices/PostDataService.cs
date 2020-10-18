using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseAccess;
using DatabaseAccess.Entities;

namespace CodeToTest.DataServices
{
    public class PostDataService : IPostDataService
    {
        private readonly ICommentDataService _commentService;
        private readonly DataContext _dbContext;

        public PostDataService(DataContext dataContext, ICommentDataService commentDataService)
        {
            _dbContext = dataContext;
            _commentService = commentDataService;
        }

        public List<Post> GetPostsWithCommentsCreatedInRange(DateTime from, DateTime until)
        {
            var comments = _commentService.GetCommentsCreatedInRange(from, until);
            var posts = comments.Select(c => c.Post).Distinct().ToList();
            return posts;
        }

        public List<Post> GetPostsByUserId(Guid id)
        {
            var posts = _dbContext.Posts.Where(p => p.UserId == id).ToList();
            return posts;
        }
    }
}
