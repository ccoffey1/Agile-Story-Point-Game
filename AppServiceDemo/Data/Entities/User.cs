using System;
using System.ComponentModel.DataAnnotations;

namespace AppServiceDemo.Data.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public Guid GameSessionId { get; set; }
        public string FirstName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
