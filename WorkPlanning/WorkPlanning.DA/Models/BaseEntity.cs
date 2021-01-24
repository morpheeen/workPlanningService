using System;
using System.ComponentModel.DataAnnotations;

namespace WorkPlanning.DA.Models
{
    public abstract class BaseEntity
    {
        [Key, Required]
        public Guid Guid { get; set; }

        protected BaseEntity()
        {
            DateCreated = DateTime.UtcNow;
        }

        public virtual DateTime DateCreated { get; set; }
    }
}
