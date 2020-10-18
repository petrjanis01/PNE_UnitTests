using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseAccess.Entities
{
    [Table("User")]
    public class User : EntityBase
    {
        public User()
        {
            Posts = new List<Post>();
            Comment = new List<Comment>();
        }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public DateTime Birthdate { get; set; }

        public string Email { get; set; }

        public virtual List<Post> Posts { get; set; }

        public virtual List<Comment> Comment { get; set; }
    }
}
