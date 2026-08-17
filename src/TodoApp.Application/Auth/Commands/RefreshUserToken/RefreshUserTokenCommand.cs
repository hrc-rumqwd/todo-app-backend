using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Commons;
using TodoApp.Application.Contracts;
using TodoApp.Application.Contracts.Generator;
using TodoApp.Application.Extensions;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Extensions;
using TodoApp.Shared.Commons;
using TodoApp.Shared.Domains.Auth;

namespace TodoApp.Api.Controllers.v1.RefreshUserToken
{
    public class RefreshUserTokenCommand : ICommand<Result<TokenResult>>
    {
        public string RefreshToken { get; set; }
    }

    public class RefreshUserTokenCommandHandler : ICommandHandler<RefreshUserTokenCommand, Result<TokenResult>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IJwtService _jwtService;

        public RefreshUserTokenCommandHandler(
            IApplicationDbContext dbContext,
            IJwtService jwtService)
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
        }

        public async Task<Result<TokenResult>> Handle(RefreshUserTokenCommand command, CancellationToken cancellationToken)
        {
            var refreshToken = await _dbContext.Query<RefreshToken>()
                .Where(x => 
                    x.IsActive && 
                    x.ExpiresAt > DateTime.UtcNow)
                .FirstOrDefaultAsync(cancellationToken);

            if(refreshToken is null)
            {
                return Result<TokenResult>.Failure("Invalid refresh token.");
            }

            // Search user
            var user = await _dbContext.Query<AppUser>()
                .Where(x => x.Id == refreshToken.UserId)
                .FirstOrDefaultAsync(cancellationToken);

            if(user is null)
                return Result<TokenResult>.Failure("User not found.");

            // Generate new token
            var tokenResult = _jwtService.GenerateToken(user.GetClaims());

            // Saving new refresh token
            var newRefreshToken = new RefreshToken
            {
                UserId = user.Id,
                TokenHash = tokenResult.RefreshToken,
                ExpiresAt = tokenResult.RefreshTokenExpiry,
                IsActive = true
            };

            _dbContext.Set<RefreshToken>().Add(newRefreshToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result<TokenResult>.Success(tokenResult);
        }
    }
}
