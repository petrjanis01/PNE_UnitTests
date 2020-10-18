using System;
using System.Collections.Generic;
using DatabaseAccess.Entities;

namespace CodeToTest.DataServices
{
    public interface IPostDataService
    {
        List<Post> GetPostsWithCommentsCreatedInRange(DateTime from, DateTime until);

        List<Post> GetPostsByUserId(Guid id);
    }
}
