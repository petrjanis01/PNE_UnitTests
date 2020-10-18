using System;

namespace DatabaseAccess.Entities
{
    public class Comment : EntityBase
    {
        public Comment()
        {
            CreatedDateTime = DateTime.Now;
        }
        public string Text { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public Guid PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
