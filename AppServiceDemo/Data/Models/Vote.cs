using System;
using System.ComponentModel.DataAnnotations;

namespace AppServiceDemo.Data.Models
{
	public class Vote
	{
		public Guid Id { get; set; }
		public Guid GameSessionId { get; set; }
		public int Points { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
