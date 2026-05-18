using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface IMatchService
    {
        Task<IEnumerable<Match>> GetAllAsync();

        Task<Match?> GetByIdAsync(int id);

        Task<IEnumerable<Match>> GetByTournamentAsync(int tournamentId);

        Task<IEnumerable<Match>> GetByTeamAsync(int teamId);

        Task<Match> CreateAsync(Match match);

        Task UpdateAsync(int id, Match match);

        Task DeleteAsync(int id);
    }
}