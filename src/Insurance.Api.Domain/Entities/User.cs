using System.ComponentModel.DataAnnotations;
using Insurance.Api.Domain.Core.Entities;

namespace Insurance.Api.Domain.Entities
{
    public class User : Entity
    {
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}

