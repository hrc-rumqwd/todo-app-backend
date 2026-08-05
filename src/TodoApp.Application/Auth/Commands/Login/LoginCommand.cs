using Microsoft.AspNetCore.Identity;
using TodoApp.Application.Commons;
using TodoApp.Application.Contracts.Generator;
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

        public LoginCommandHandler(
            IJwtService jwtService,
            UserManager<AppUser> userManager
        )
        {
            _jwtService = jwtService;
            _userManager = userManager;
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

            // Implement your login logic here
            // For example, validate user credentials and generate tokens
            // This is just a placeholder implementation
            var result = new LoginCommandResult
            {
                AccessToken = tokenResult.AccessToken,
                RefreshToken = tokenResult.RefreshToken,
                Expiration = tokenResult.Expiry
            };
            return await Task.FromResult(Result<LoginCommandResult>.Success(result));
        }
    }

    public class LoginCommandResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }   
}
