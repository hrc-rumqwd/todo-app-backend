using Microsoft.AspNetCore.Identity;
using System.Linq;
using TodoApp.Application.Commons;
using TodoApp.Application.Contracts;
using TodoApp.Application.Contracts.Generator;
using TodoApp.Application.Extensions;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Extensions;
using TodoApp.Shared.Commons;
using TodoApp.Shared.Constants;

namespace TodoApp.Application.Auth.Commands.Login
{
    public class LoginCommand : ICommand<Result<LoginCommandResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler : ICommandHandler<LoginCommand, Result<LoginCommandResult>>
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IApplicationDbContext _dbContext;

        public LoginCommandHandler(
            IJwtService jwtService,
            UserManager<AppUser> userManager,
            IApplicationDbContext dbContext
        )
        {
            _jwtService = jwtService;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<Result<LoginCommandResult>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);

            if(user == null)
                return Result<LoginCommandResult>.Failure(ErrorCodes.InvalidCredentials);

            var validPwd = await _userManager.CheckPasswordAsync(user, command.Password);
            if (!validPwd)
                return Result<LoginCommandResult>.Failure(ErrorCodes.InvalidCredentials);

            var tokenResult = _jwtService.GenerateToken(user.GetClaims());

            // Saving the refresh token to the user entity (if needed)
            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                TokenHash = tokenResult.RefreshToken,
                ExpiresAt = tokenResult.RefreshTokenExpiry,
            };

            var refreshTokenSet = _dbContext.Set<RefreshToken>();
            refreshTokenSet.Add(refreshToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Implement your login logic here
            // For example, validate user credentials and generate tokens
            // This is just a placeholder implementation
            var result = new LoginCommandResult
            {
                AccessToken = tokenResult.AccessToken,
                RefreshToken = tokenResult.RefreshToken,
                AccessTokenExpiry = tokenResult.AccessTokenExpiry,
                RefreshTokenExpiry = tokenResult.RefreshTokenExpiry
            };
            return await Task.FromResult(Result<LoginCommandResult>.Success(result));
        }
    }

    public class LoginCommandResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpiry { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }   
}
