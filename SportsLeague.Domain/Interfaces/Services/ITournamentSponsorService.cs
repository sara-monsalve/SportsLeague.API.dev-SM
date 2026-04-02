using System;
using System.Collections.Generic;
using System.Text;

using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services;

public interface ITournamentSponsorService
{
    Task<TournamentSponsor> CreateAsync(TournamentSponsor entity);
}