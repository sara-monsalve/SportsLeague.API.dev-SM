using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface IGoalService
    {
        Task<IEnumerable<Goal>> GetByMatchAsync(int matchId);
        Task<Goal> CreateAsync(Goal goal);
    }
}