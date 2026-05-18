using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IRefereeRepository _refereeRepository;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ILogger<MatchService> _logger;

        public MatchService(
            IMatchRepository matchRepository,
            ITeamRepository teamRepository,
            IRefereeRepository refereeRepository,
            ITournamentRepository tournamentRepository,
            ILogger<MatchService> logger)
        {
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
            _refereeRepository = refereeRepository;
            _tournamentRepository = tournamentRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Match>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all matches");
            return await _matchRepository.GetAllWithRelationsAsync();
        }

        public async Task<Match?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving match with ID: {MatchId}", id);
            return await _matchRepository.GetByIdWithRelationsAsync(id);
        }

        public async Task<IEnumerable<Match>> GetByTournamentAsync(int tournamentId)
        {
            var exists = await _tournamentRepository.ExistsAsync(tournamentId);

            if (!exists)
                throw new KeyNotFoundException($"No se encontró el torneo con ID {tournamentId}");

            return await _matchRepository.GetByTournamentAsync(tournamentId);
        }

        public async Task<IEnumerable<Match>> GetByTeamAsync(int teamId)
        {
            var exists = await _teamRepository.ExistsAsync(teamId);

            if (!exists)
                throw new KeyNotFoundException($"No se encontró el equipo con ID {teamId}");

            return await _matchRepository.GetByTeamAsync(teamId);
        }

        public async Task<Match> CreateAsync(Match match)
        {
            if (match.HomeTeamId == match.AwayTeamId)
                throw new InvalidOperationException("Un equipo no puede jugar contra sí mismo");

            if (!await _teamRepository.ExistsAsync(match.HomeTeamId))
                throw new KeyNotFoundException($"No se encontró el equipo local con ID {match.HomeTeamId}");

            if (!await _teamRepository.ExistsAsync(match.AwayTeamId))
                throw new KeyNotFoundException($"No se encontró el equipo visitante con ID {match.AwayTeamId}");

            if (!await _refereeRepository.ExistsAsync(match.RefereeId))
                throw new KeyNotFoundException($"No se encontró el árbitro con ID {match.RefereeId}");

            if (!await _tournamentRepository.ExistsAsync(match.TournamentId))
                throw new KeyNotFoundException($"No se encontró el torneo con ID {match.TournamentId}");

            _logger.LogInformation("Creating match");
            return await _matchRepository.CreateAsync(match);
        }

        public async Task UpdateAsync(int id, Match match)
        {
            var existing = await _matchRepository.GetByIdAsync(id);

            if (existing == null)
                throw new KeyNotFoundException($"No se encontró el partido con ID {id}");

            existing.MatchDate = match.MatchDate;
            existing.Location = match.Location;
            existing.HomeTeamId = match.HomeTeamId;
            existing.AwayTeamId = match.AwayTeamId;
            existing.RefereeId = match.RefereeId;
            existing.TournamentId = match.TournamentId;

            await _matchRepository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var exists = await _matchRepository.ExistsAsync(id);

            if (!exists)
                throw new KeyNotFoundException($"No se encontró el partido con ID {id}");

            await _matchRepository.DeleteAsync(id);
        }
    }
}