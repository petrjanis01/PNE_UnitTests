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

        public UserDataService(DataContext dataContext)
        {
            _dbContext = dataContext;
        }

        public List<User> GetAllUsers()
        {
            var users = _dbContext.Users.ToList();
            return users;
        }

        public List<User> GetAllUserBornAfter(DateTime birthDate)
        {
            var users = _dbContext.Users.Where(u => u.Birthdate > birthDate).ToList();
            return users;
        }

        public List<User> GetAllUsersWithMoreCommentsThanPosts()
        {
            var users = _dbContext.Users.Where(u => u.Comment.Count > u.Posts.Count).ToList();
            return users;
        }
    }
}
