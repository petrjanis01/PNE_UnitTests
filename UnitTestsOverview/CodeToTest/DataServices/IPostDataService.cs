using System;
using System.Collections.Generic;
using System.Text;
using DatabaseAccess.Entities;

namespace CodeToTest.DataServices
{
    public interface IPostDataService
    {
        List<Post> GetPostsWithCommentsCreatedInRange(DateTime from, DateTime until);
    }
}
