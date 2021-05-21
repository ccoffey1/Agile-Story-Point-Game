using AppServiceDemo.Data.Models;
using AppServiceDemo.Data.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppServiceDemo.Service
{
	public interface IVoteService
	{
		Task<IEnumerable<Vote>> GetByGameSessionIdAsync(Guid gameSessionId);
		Task<Vote> CreateAsync(Guid gameSessionId, int points);
	}

	public class VoteService : IVoteService
	{
		private readonly ILogger<VoteService> _logger;
		private readonly IVoteRepository _voteRepository;

		public VoteService(
			ILogger<VoteService> logger,
			IVoteRepository voteRepository)
		{
			_logger = logger;
			_voteRepository = voteRepository;
		}

		public async Task<Vote> CreateAsync(Guid gameSessionId, int points)
		{
			return await _voteRepository.CreateAsync(gameSessionId, points);
		}

		public async Task<IEnumerable<Vote>> GetByGameSessionIdAsync(Guid gameSessionId)
		{
			return await _voteRepository.GetByGameSessionIdAsync(gameSessionId);
		}
	}
}
