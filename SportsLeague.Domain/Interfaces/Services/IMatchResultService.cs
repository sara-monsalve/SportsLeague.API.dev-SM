using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface IMatchResultService
    {
        Task<IEnumerable<MatchResult>> GetAllAsync();

        Task<MatchResult?> GetByIdAsync(int id);

        Task<MatchResult?> GetByMatchIdAsync(int matchId);

        Task<MatchResult> CreateAsync(MatchResult matchResult);

        Task UpdateAsync(int id, MatchResult matchResult);

        Task DeleteAsync(int id);
    }
}