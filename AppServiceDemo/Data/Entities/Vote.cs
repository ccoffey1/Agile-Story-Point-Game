using System;

namespace AppServiceDemo.Data.Entities
{
    public class Vote
	{
		public Guid Id { get; set; }
		public Guid GameSessionId { get; set; }
		public int Points { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
