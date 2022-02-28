using System;
using System.ComponentModel.DataAnnotations;

namespace Insurance.Api.Domain.Core.Entities
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
