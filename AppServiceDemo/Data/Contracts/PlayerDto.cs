using System;
using System.ComponentModel.DataAnnotations;

namespace AppServiceDemo.Data.Contracts
{
    public class PlayerDto
    {
        public int Id { get; set; }
        [Required]
        public string PlayerName { get; set; }
        [Required]
        public int GameSessionId { get; set; }
    }
}
