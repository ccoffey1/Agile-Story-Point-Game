using AppServiceDemo.Data.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppServiceDemo.Data.Entities
{
    [Index(nameof(JoinCode))]
    public class GameSession : TimeStampedEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        public string JoinCode { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}