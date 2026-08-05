using Mapster;
using Microsoft.AspNetCore.Identity;
using TodoApp.Application.Commons;
using TodoApp.Domain.Entities;
using TodoApp.Shared.Commons;
using TodoApp.Shared.Utils;

namespace TodoApp.Application.Auth.Commands.Register
{
    public class RegisterCommand : ICommand<Result<RegisterCommandResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }

    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, Result<RegisterCommandResult>>
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<RegisterCommandResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if(!RegexOperator.IsEmailValid(command.Email))
            {
                // Return an error indicating invalid email
                return Result<RegisterCommandResult>.Failure("Invalid email address.");
            }

            if (!RegexOperator.IsPasswordValid(command.Password))
            {
                // Return an error indicating invalid password
                return Result<RegisterCommandResult>.Failure("Invalid password. Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
            }

            var user = await _userManager.FindByEmailAsync(command.Email);
            if(user != null)
            {
                // Return an error indicating that the user already exists
                return Result<RegisterCommandResult>.Failure("User with this email already exists.");
            }

            user = command.Adapt<AppUser>();

            var result = await _userManager.CreateAsync(user, command.Password);
            if (!result.Succeeded)
            {
                // Return an error indicating that the user could not be created
                return Result<RegisterCommandResult>.Failure("Failed to create user.");
            }

            return Result<RegisterCommandResult>.Success(new()
            {
                Id = user.Id
            });
        }
    }

    public class RegisterCommandResult
    {
        public Guid Id { get; set; }
    }
}
