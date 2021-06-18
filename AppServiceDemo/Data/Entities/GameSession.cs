using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppServiceDemo.Data.Entities
{
    public class GameSession
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string TeamName { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
