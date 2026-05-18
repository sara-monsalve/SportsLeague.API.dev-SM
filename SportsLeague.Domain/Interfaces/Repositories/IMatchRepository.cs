using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface IMatchRepository : IGenericRepository<Match>
    {
        Task<IEnumerable<Match>> GetByTournamentAsync(int tournamentId);

        Task<IEnumerable<Match>> GetByTeamAsync(int teamId);

        Task<bool> ExistsMatchAsync(
            int homeTeamId,
            int awayTeamId,
            DateTime matchDate);
    }
}