using TodoApp.Application.Commons;
using TodoApp.Shared.Commons;
using TodoApp.Shared.Domains.Dtos;

namespace TodoApp.Application.TaskItems.Commands.DeleteTaskItem
{
    public record DeleteTaskItemCommand(long id) : ICommand<Result<bool>>;
}
