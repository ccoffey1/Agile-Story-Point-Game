using System;
using System.ComponentModel.DataAnnotations;

namespace AppServiceDemo.Data.Entities
{
    public class GameSession
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid OwnerUserId { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string TeamName { get; set; }
        public User Owner { get; set;  }
    }
}
