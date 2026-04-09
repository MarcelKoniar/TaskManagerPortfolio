using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.EntityModels
{
    // Index attribute is defined in EF Core and applied in the DbContext configuration
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public virtual DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual string? CreatedBy { get; set; }
        public virtual string? UpdatedBy { get; set; }
        public virtual bool Deleted { get; set; }
    }
}
