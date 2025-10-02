using Microsoft.EntityFrameworkCore;
using SevSU.HabitsTracker.Identity.Api.DbContexts;
using SevSU.HabitsTracker.Identity.Api.Models.Entities;

namespace SevSU.HabitsTracker.Identity.Api.Repositories;

public class RefreshTokensRepository : IRefreshTokensRepository
{
    private readonly AppDbContext _context;

    public RefreshTokensRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token, cancellationToken);
    }

    public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}