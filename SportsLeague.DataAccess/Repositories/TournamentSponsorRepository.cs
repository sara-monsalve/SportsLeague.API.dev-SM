using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories;

public class TournamentSponsorRepository
    : GenericRepository<TournamentSponsor>, ITournamentSponsorRepository
{
    private readonly LeagueDbContext _context;

    public TournamentSponsorRepository(LeagueDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsRelationAsync(int tournamentId, int sponsorId)
    {
        return await _context.TournamentSponsors
            .AnyAsync(ts => ts.TournamentId == tournamentId && ts.SponsorId == sponsorId);
    }
}