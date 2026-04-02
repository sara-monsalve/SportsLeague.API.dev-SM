using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories;

public class SponsorRepository : GenericRepository<Sponsor>, ISponsorRepository
{
    private readonly LeagueDbContext _context;

    public SponsorRepository(LeagueDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Sponsor?> GetByNameAsync(string name)
    {
        return await _context.Sponsors
            .FirstOrDefaultAsync(s => s.Name == name);
    }
}