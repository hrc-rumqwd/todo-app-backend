using TodoApp.Application.Commons;
using TodoApp.Shared.Commons;
using TodoApp.Shared.Domains.Dtos;

namespace TodoApp.Application.TaskItems.Commands.UpdateTaskItem
{
    public class UpdateTaskItemCommand : ICommand<Result<TaskItemDto>>
    {
        public UpdateTaskItemCommand(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}
