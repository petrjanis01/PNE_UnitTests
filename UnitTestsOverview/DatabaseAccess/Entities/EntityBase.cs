using System;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccess.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; private set; }
    }
}
