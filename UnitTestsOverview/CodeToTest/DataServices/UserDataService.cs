using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseAccess;
using DatabaseAccess.Entities;

namespace CodeToTest.DataServices
{
    public class UserDataService
    {
        private readonly DataContext _dbContext;
        private readonly IPostDataService _postService;

        public UserDataService(DataContext dataContext, IPostDataService postDataService)
        {
            _dbContext = dataContext;
            _postService = postDataService;
        }

        public List<User> GetAllUsers()
        {
            var users = _dbContext.Users.ToList();
            return users;
        }

        public List<User> GetUsersBornAfter(DateTime date)
        {
            var users = _dbContext.Users.Where(u => u.Birthdate > date).ToList();
            return users;
        }

        public List<User> GetUsersWithMoreCommentsThanPosts()
        {
            var users = _dbContext.Users.Where(u => u.Comment.Count > u.Posts.Count).ToList();
            return users;
        }

        public Dictionary<DateTime, int> GetPostsByDateHistogram(Guid userId)
        {
            var posts = _postService.GetPostsByUserId(userId);

            var postsByDate = posts
                .GroupBy(p => p.CreatedTime.Date)
                .OrderBy(p => p.Key);

            var result = new Dictionary<DateTime, int>();

            foreach (var postByDate in postsByDate)
            {
                var countOfPosts = postByDate.Count();
                result.Add(postByDate.Key, countOfPosts);
            }

            return result;
        }
    }
}
