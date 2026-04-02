using System;
using System.Collections.Generic;
using System.Text;

using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories;

public interface ITournamentSponsorRepository : IGenericRepository<TournamentSponsor>
{
    Task<bool> ExistsRelationAsync(int tournamentId, int sponsorId);
}