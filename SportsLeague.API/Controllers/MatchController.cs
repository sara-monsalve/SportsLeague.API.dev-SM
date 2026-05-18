using Microsoft.AspNetCore.Mvc;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _service;

        public MatchController(IMatchService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);

            if (item == null)
                return NotFound(new
                {
                    message = $"No se encontró el partido con ID {id}"
                });

            return Ok(item);
        }

        [HttpGet("tournament/{tournamentId}")]
        public async Task<IActionResult> GetByTournament(int tournamentId)
        {
            var data = await _service.GetByTournamentAsync(tournamentId);
            return Ok(data);
        }

        [HttpGet("team/{teamId}")]
        public async Task<IActionResult> GetByTeam(int teamId)
        {
            var data = await _service.GetByTeamAsync(teamId);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Match match)
        {
            try
            {
                var created = await _service.CreateAsync(match);
                return Ok(created);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Match match)
        {
            try
            {
                await _service.UpdateAsync(id, match);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
        }
    }
}