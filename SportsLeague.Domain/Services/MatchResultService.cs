using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services
{
    public class MatchResultService : IMatchResultService
    {
        private readonly IMatchResultRepository _matchResultRepository;
        private readonly IMatchRepository _matchRepository;

        public MatchResultService(
            IMatchResultRepository matchResultRepository,
            IMatchRepository matchRepository)
        {
            _matchResultRepository = matchResultRepository;
            _matchRepository = matchRepository;
        }

        public async Task<IEnumerable<MatchResult>> GetAllAsync()
        {
            return await _matchResultRepository.GetAllAsync();
        }

        public async Task<MatchResult?> GetByIdAsync(int id)
        {
            return await _matchResultRepository.GetByIdAsync(id);
        }

        public async Task<MatchResult?> GetByMatchIdAsync(int matchId)
        {
            return await _matchResultRepository.GetByMatchIdAsync(matchId);
        }

        public async Task<MatchResult> CreateAsync(MatchResult matchResult)
        {
            var matchExists = await _matchRepository.ExistsAsync(matchResult.MatchId);

            if (!matchExists)
            {
                throw new KeyNotFoundException(
                    $"No existe el partido con ID {matchResult.MatchId}");
            }

            var alreadyExists = await _matchResultRepository.ExistsForMatchAsync(matchResult.MatchId);

            if (alreadyExists)
            {
                throw new InvalidOperationException(
                    "Este partido ya tiene un resultado registrado");
            }

            return await _matchResultRepository.CreateAsync(matchResult);
        }

        public async Task UpdateAsync(int id, MatchResult matchResult)
        {
            var existing = await _matchResultRepository.GetByIdAsync(id);

            if (existing == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró el resultado con ID {id}");
            }

            existing.HomeScore = matchResult.HomeScore;
            existing.AwayScore = matchResult.AwayScore;

            await _matchResultRepository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var exists = await _matchResultRepository.ExistsAsync(id);

            if (!exists)
            {
                throw new KeyNotFoundException(
                    $"No se encontró el resultado con ID {id}");
            }

            await _matchResultRepository.DeleteAsync(id);
        }
    }
}