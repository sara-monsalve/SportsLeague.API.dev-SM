using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services
{
    public class GoalService : IGoalService
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;

        public GoalService(
            IGoalRepository goalRepository,
            IMatchRepository matchRepository,
            IPlayerRepository playerRepository)
        {
            _goalRepository = goalRepository;
            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
        }

        public async Task<IEnumerable<Goal>> GetByMatchAsync(int matchId)
        {
            return await _goalRepository.GetByMatchAsync(matchId);
        }

        public async Task<Goal> CreateAsync(Goal goal)
        {
            var match = await _matchRepository.GetByIdAsync(goal.MatchId);

            if (match == null)
                throw new KeyNotFoundException("Match not found");

            if (match.Status != MatchStatus.InProgress)
                throw new InvalidOperationException("Goals can only be registered when match is InProgress");

            var player = await _playerRepository.GetByIdAsync(goal.PlayerId);

            if (player == null)
                throw new KeyNotFoundException("Player not found");

            if (player.TeamId != goal.TeamId)
                throw new InvalidOperationException("Player does not belong to selected team");

            return await _goalRepository.CreateAsync(goal);
        }
    }
}