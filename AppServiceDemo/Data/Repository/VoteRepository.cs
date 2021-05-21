using AppServiceDemo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppServiceDemo.Data.Repository
{
	public interface IVoteRepository
	{
		Task<Vote> CreateAsync(Guid gameSessionId, int points);
		Task<IEnumerable<Vote>> GetByGameSessionIdAsync(Guid gameSessionId);
	}

	public class VoteRepository : IVoteRepository
	{
		private readonly CosmosDbContext _dbContext;

		public VoteRepository(CosmosDbContext dbContext)
		{
			_dbContext = dbContext;
			_dbContext.Database.EnsureCreated();
		}

		public async Task<Vote> CreateAsync(Guid gameSessionId, int points)
		{
			var entity = _dbContext.Votes.Add(new Vote()
			{
				Points = points,
				GameSessionId = gameSessionId,
				CreatedAt = DateTime.Now
			}).Entity;

			await _dbContext.SaveChangesAsync();

			return entity;
		}

		public async Task<IEnumerable<Vote>> GetByGameSessionIdAsync(Guid gameSessionId)
		{
			return await _dbContext.Votes
				.Where(x => x.GameSessionId == gameSessionId)
				.ToArrayAsync();
		}
	}
}
