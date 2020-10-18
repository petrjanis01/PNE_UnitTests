using System;
using System.Collections.Generic;

namespace DatabaseAccess.Entities
{
    public class Post : EntityBase
    {
        public Post()
        {
            CreatedTime = DateTime.Now;
            Comments = new List<Comment>();
        }

        public string Text { get; set; }

        public DateTime CreatedTime { get; set; }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}
