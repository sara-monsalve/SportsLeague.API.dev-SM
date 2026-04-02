using Microsoft.AspNetCore.Mvc;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TournamentSponsorController : ControllerBase
{
    private readonly ITournamentSponsorService _service;

    public TournamentSponsorController(ITournamentSponsorService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TournamentSponsor entity)
    {
        var result = await _service.CreateAsync(entity);
        return Ok(result);
    }
}