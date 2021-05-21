using AppServiceDemo.Data.Models;
using AppServiceDemo.Data.Repository;
using AppServiceDemo.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppServiceDemo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class VoteController : ControllerBase
	{
		private readonly ILogger<VoteController> _logger;
		private readonly IVoteService _voteService;

		public VoteController(
			ILogger<VoteController> logger,
			IVoteService voteService)
		{
			_logger = logger;
			_voteService = voteService;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Vote>))]
		[Route("gamesession/{gameSessionId:guid}")]
		public async Task<ActionResult<IEnumerable<Vote>>> Get(Guid gameSessionId)
		{
			try
			{
				return Ok(await _voteService.GetByGameSessionIdAsync(gameSessionId));
			}
			catch (Exception e)
			{
				_logger.LogError($"An error occurred fetching votes for game session {gameSessionId}", e);
				return StatusCode(500);
			}
		}

		[HttpPost]
		public async Task<ActionResult<Vote>> Create(Guid gameSessionId, int points)
		{
			try
			{
				var vote = await _voteService.CreateAsync(gameSessionId, points);
				return CreatedAtAction(nameof(Create), new { id = vote.Id }, vote);
			}
			catch (Exception e)
			{
				_logger.LogError($"An error occurred creating vote for game session {gameSessionId}", e);
				return StatusCode(500);
			}
		}
	}
}
