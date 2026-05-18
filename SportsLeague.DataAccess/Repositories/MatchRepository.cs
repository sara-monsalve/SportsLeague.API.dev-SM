using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories
{
    public class MatchRepository : GenericRepository<Match>, IMatchRepository
    {
        private readonly LeagueDbContext _context;

        public MatchRepository(LeagueDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Match>> GetByTournamentAsync(int tournamentId)
        {
            return await _context.Set<Match>()
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Referee)
                .Where(m => m.TournamentId == tournamentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Match>> GetByTeamAsync(int teamId)
        {
            return await _context.Set<Match>()
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Referee)
                .Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId)
                .ToListAsync();
        }

        public async Task<bool> ExistsMatchAsync(
            int homeTeamId,
            int awayTeamId,
            DateTime matchDate)
        {
            return await _context.Set<Match>()
                .AnyAsync(m =>
                    m.HomeTeamId == homeTeamId &&
                    m.AwayTeamId == awayTeamId &&
                    m.MatchDate == matchDate);
        }
    }
}