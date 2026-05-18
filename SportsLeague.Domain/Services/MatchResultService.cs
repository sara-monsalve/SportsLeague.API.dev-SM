using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services
{
    public class MatchResultService : IMatchResultService
    {
        private readonly IMatchResultRepository _matchResultRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly ILogger<MatchResultService> _logger;

        public MatchResultService(
            IMatchResultRepository matchResultRepository,
            IMatchRepository matchRepository,
            ILogger<MatchResultService> logger)
        {
            _matchResultRepository = matchResultRepository;
            _matchRepository = matchRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<MatchResult>> GetAllAsync()
        {
            return await _matchResultRepository.GetAllAsync();
        }

        public async Task<MatchResult?> GetByIdAsync(int id)
        {
            return await _matchResultRepository.GetWithMatchAsync(id);
        }

        public async Task<MatchResult?> GetByMatchIdAsync(int matchId)
        {
            return await _matchResultRepository.GetByMatchIdAsync(matchId);
        }

        public async Task<MatchResult> CreateAsync(MatchResult matchResult)
        {
            if (!await _matchRepository.ExistsAsync(matchResult.MatchId))
                throw new KeyNotFoundException($"No se encontró el partido con ID {matchResult.MatchId}");

            var existing = await _matchResultRepository.GetByMatchIdAsync(matchResult.MatchId);

            if (existing != null)
                throw new InvalidOperationException("Ese partido ya tiene resultado registrado");

            return await _matchResultRepository.CreateAsync(matchResult);
        }

        public async Task UpdateAsync(int id, MatchResult matchResult)
        {
            var existing = await _matchResultRepository.GetByIdAsync(id);

            if (existing == null)
                throw new KeyNotFoundException($"No se encontró el resultado con ID {id}");

            existing.HomeScore = matchResult.HomeScore;
            existing.AwayScore = matchResult.AwayScore;

            await _matchResultRepository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            if (!await _matchResultRepository.ExistsAsync(id))
                throw new KeyNotFoundException($"No se encontró el resultado con ID {id}");

            await _matchResultRepository.DeleteAsync(id);
        }
    }
}