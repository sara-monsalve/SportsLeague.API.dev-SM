using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories
{
    public class MatchResultRepository : GenericRepository<MatchResult>, IMatchResultRepository
    {
        private readonly LeagueDbContext _context;

        public MatchResultRepository(LeagueDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<MatchResult?> GetByMatchIdAsync(int matchId)
        {
            return await _context.Set<MatchResult>()
                .Include(mr => mr.Match)
                .FirstOrDefaultAsync(mr => mr.MatchId == matchId);
        }

        public async Task<bool> ExistsForMatchAsync(int matchId)
        {
            return await _context.Set<MatchResult>()
                .AnyAsync(mr => mr.MatchId == matchId);
        }
    }
}