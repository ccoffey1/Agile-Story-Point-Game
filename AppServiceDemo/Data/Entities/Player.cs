using AppServiceDemo.Data.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AppServiceDemo.Data.Entities
{
    [Index(nameof(Name))]
    public class Player : TimeStampedEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        public int GameSessionId { get; set; }
        public GameSession GameSession { get; set; }
    }
}