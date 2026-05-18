using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface ICardService
    {
        Task<IEnumerable<Card>> GetByMatchAsync(int matchId);
        Task<Card> CreateAsync(Card card);
    }
}