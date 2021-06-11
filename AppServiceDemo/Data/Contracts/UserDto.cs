using System;
using System.ComponentModel.DataAnnotations;

namespace AppServiceDemo.Data.Contracts
{
    public class UserDto
    {
        public Guid Id { get; set; }
        [Required]
        public Guid GameSessionId { get; set; }
        [Required]
        public string FirstName { get; set; }
    }
}
