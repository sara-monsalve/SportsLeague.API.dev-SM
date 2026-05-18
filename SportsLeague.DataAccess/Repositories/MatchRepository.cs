using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories
{
    public class MatchRepository : GenericRepository<Match>, IMatchRepository
    {
        private readonly LeagueDbContext _context;

        public MatchRepository(LeagueDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Match>> GetAllWithRelationsAsync()
        {
            return await _context.Set<Match>()
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Referee)
                .Include(m => m.Tournament)
                .ToListAsync();
        }

        public async Task<Match?> GetByIdWithRelationsAsync(int id)
        {
            return await _context.Set<Match>()
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Referee)
                .Include(m => m.Tournament)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Match>> GetByTournamentAsync(int tournamentId)
        {
            return await _context.Set<Match>()
                .Where(m => m.TournamentId == tournamentId)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .ToListAsync();
        }

        public async Task<IEnumerable<Match>> GetByTeamAsync(int teamId)
        {
            return await _context.Set<Match>()
                .Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .ToListAsync();
        }
    }
}