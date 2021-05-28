using System;

namespace AppServiceDemo.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid GameSessionId { get; set; }
        public string FirstName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
