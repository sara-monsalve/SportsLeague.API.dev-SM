using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories
{
    public class GoalRepository : GenericRepository<Goal>, IGoalRepository
    {
        private readonly LeagueDbContext _context;

        public GoalRepository(LeagueDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Goal>> GetByMatchAsync(int matchId)
        {
            return await _context.Goals
                .Include(g => g.Player)
                .Include(g => g.Team)
                .Where(g => g.MatchId == matchId)
                .ToListAsync();
        }
    }
}