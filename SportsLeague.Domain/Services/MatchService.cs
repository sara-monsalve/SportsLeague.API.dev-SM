using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ILogger<MatchService> _logger;

        public MatchService(
            IMatchRepository matchRepository,
            ILogger<MatchService> logger)
        {
            _matchRepository = matchRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Match>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all matches");
            return await _matchRepository.GetAllAsync();
        }

        public async Task<Match?> GetByIdAsync(int id)
        {
            var match = await _matchRepository.GetByIdAsync(id);

            if (match == null)
            {
                _logger.LogWarning("Match with ID {MatchId} not found", id);
            }

            return match;
        }

        public async Task<IEnumerable<Match>> GetByTournamentAsync(int tournamentId)
        {
            return await _matchRepository.GetByTournamentAsync(tournamentId);
        }

        public async Task<IEnumerable<Match>> GetByTeamAsync(int teamId)
        {
            return await _matchRepository.GetByTeamAsync(teamId);
        }

        public async Task<Match> CreateAsync(Match match)
        {
            if (match.HomeTeamId == match.AwayTeamId)
            {
                throw new InvalidOperationException(
                    "El equipo local y visitante no pueden ser iguales.");
            }

            var exists = await _matchRepository.ExistsMatchAsync(
                match.HomeTeamId,
                match.AwayTeamId,
                match.MatchDate);

            if (exists)
            {
                throw new InvalidOperationException(
                    "Ya existe un partido con esos equipos en esa fecha.");
            }

            _logger.LogInformation(
                "Creating match between {HomeTeam} and {AwayTeam}",
                match.HomeTeamId,
                match.AwayTeamId);

            return await _matchRepository.CreateAsync(match);
        }

        public async Task UpdateAsync(int id, Match match)
        {
            var existing = await _matchRepository.GetByIdAsync(id);

            if (existing == null)
            {
                throw new KeyNotFoundException(
                    $"No se encontró el partido con ID {id}");
            }

            existing.MatchDate = match.MatchDate;
            existing.Venue = match.Venue;
            existing.Status = match.Status;
            existing.HomeTeamId = match.HomeTeamId;
            existing.AwayTeamId = match.AwayTeamId;
            existing.TournamentId = match.TournamentId;
            existing.RefereeId = match.RefereeId;

            await _matchRepository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var exists = await _matchRepository.ExistsAsync(id);

            if (!exists)
            {
                throw new KeyNotFoundException(
                    $"No se encontró el partido con ID {id}");
            }

            await _matchRepository.DeleteAsync(id);
        }
    }
}