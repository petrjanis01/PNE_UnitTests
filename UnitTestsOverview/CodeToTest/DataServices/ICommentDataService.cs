using System;
using System.Collections.Generic;
using DatabaseAccess.Entities;

namespace CodeToTest.DataServices
{
    public interface ICommentDataService
    {
        List<Comment> GetAllCommentsCreatedInTimeRange(DateTime from, DateTime until);
    }
}
