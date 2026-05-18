using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories
{
    public class CardRepository : GenericRepository<Card>, ICardRepository
    {
        private readonly LeagueDbContext _context;

        public CardRepository(LeagueDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Card>> GetByMatchAsync(int matchId)
        {
            return await _context.Cards
                .Include(c => c.Player)
                .Include(c => c.Referee)
                .Where(c => c.MatchId == matchId)
                .ToListAsync();
        }
    }
}