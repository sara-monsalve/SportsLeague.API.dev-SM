using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services;

public class TournamentSponsorService : ITournamentSponsorService
{
    private readonly ITournamentSponsorRepository _repository;
    private readonly ILogger<TournamentSponsorService> _logger;

    public TournamentSponsorService(
        ITournamentSponsorRepository repository,
        ILogger<TournamentSponsorService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<TournamentSponsor> CreateAsync(TournamentSponsor entity)
    {
        // Validación: evitar duplicados
        var exists = await _repository.ExistsRelationAsync(entity.TournamentId, entity.SponsorId);

        if (exists)
        {
            _logger.LogWarning("La relación ya existe entre Tournament {TournamentId} y Sponsor {SponsorId}",
                entity.TournamentId, entity.SponsorId);

            throw new InvalidOperationException("Esta relación ya existe");
        }

        _logger.LogInformation("Creando relación Tournament-Sponsor");

        return await _repository.CreateAsync(entity);
    }
}