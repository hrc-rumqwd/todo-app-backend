using TodoApp.Application.Commons;

namespace TodoApp.Application.Users.Commands.CreateUser
{
    internal class CreateUserCommand : ICommand<CreateUserCommandResult>
    {
    }

    internal class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserCommandResult>
    {
        public Task<CreateUserCommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Implement the logic to create a user here
            // For demonstration purposes, we'll just return a new user ID
            var result = new CreateUserCommandResult
            {
                UserId = Guid.NewGuid()
            };
            return Task.FromResult(result);
        }
    }   

    internal class CreateUserCommandResult
    {
        public Guid UserId { get; set; }
    }
}
