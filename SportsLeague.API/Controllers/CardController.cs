using Microsoft.AspNetCore.Mvc;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet("match/{matchId}")]
        public async Task<ActionResult<IEnumerable<Card>>> GetByMatch(int matchId)
        {
            var cards = await _cardService.GetByMatchAsync(matchId);
            return Ok(cards);
        }

        [HttpPost]
        public async Task<ActionResult<Card>> Create(Card card)
        {
            var created = await _cardService.CreateAsync(card);
            return Ok(created);
        }
    }
}