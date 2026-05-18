using Microsoft.AspNetCore.Mvc;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private readonly IGoalService _goalService;

        public GoalController(IGoalService goalService)
        {
            _goalService = goalService;
        }

        [HttpGet("match/{matchId}")]
        public async Task<ActionResult<IEnumerable<Goal>>> GetByMatch(int matchId)
        {
            var goals = await _goalService.GetByMatchAsync(matchId);
            return Ok(goals);
        }

        [HttpPost]
        public async Task<ActionResult<Goal>> Create(Goal goal)
        {
            var created = await _goalService.CreateAsync(goal);
            return Ok(created);
        }
    }
}