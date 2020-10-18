using System;
using System.Linq;
using DatabaseAccess.Entities;

namespace DatabaseAccess
{
    public static class RandomDataEntityGenerator
    {
        private static Random _rnd = new Random();

        public static User CreateUserWithRandomData()
        {
            var user = new User();
            user.FirstName = $"NAME_{_rnd.Next(100)}";
            user.Surname = $"SURNAME_{_rnd.Next(100)}";
            user.Email = $"{user.FirstName}_{user.Surname}@gmail.com";
            user.Birthdate = new DateTime(_rnd.Next(1970, 2000), 5, 5);
            return user;
        }

        public static Post CreatePostWithRandomData(bool withRelatedEntities = true)
        {
            var post = new Post();
            post.User = withRelatedEntities ? CreateUserWithRandomData() : null;
            post.Text = RandomString(_rnd.Next(100, 1000));
            return post;
        }

        public static Comment CreateCommentWithRandomData(bool withRelatedEntities = true)
        {
            var comment = new Comment();
            comment.User = withRelatedEntities ? CreateUserWithRandomData() : null;
            comment.Post = withRelatedEntities ? CreatePostWithRandomData() : null;
            comment.Text = RandomString(_rnd.Next(20, 200));
            return comment;
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_rnd.Next(s.Length)]).ToArray());
        }
    }
}
