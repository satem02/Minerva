
using System;
using System.ComponentModel.DataAnnotations;

namespace Minerva.Shared.Data
{
    public class EntityBase<T> where T : struct
    {
        [Key]
        public T Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}