using TodoApp.Application.Commons;
using TodoApp.Application.Contracts;
using TodoApp.Shared.Commons;
using TodoApp.Shared.Domains.Dtos;

namespace TodoApp.Application.TaskItems.Commands.UpdateTaskItem
{
    public class UpdateTaskItemCommand : TaskItemDto, ICommand<Result<TaskItemDto>>
    {
    }

    public class UpdateTaskItemCommandHandler : ICommandHandler<UpdateTaskItemCommand, Result<TaskItemDto>>
    {
        private readonly IApplicationDbContext _context;
        public UpdateTaskItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<TaskItemDto>> Handle(UpdateTaskItemCommand request, CancellationToken cancellationToken)
        {
            var taskItem = await _context.Set<Domain.Entities.TaskItem>().FindAsync(new object[] { request.Id }, cancellationToken);
            if (taskItem == null)
            {
                return Result<TaskItemDto>.Failure("Task item not found.");
            }
            taskItem.Title = request.Title;
            taskItem.Priority = request.Priority;
            taskItem.Status = request.Status;

            await _context.SaveChangesAsync(cancellationToken);
            var taskItemDto = new TaskItemDto
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Priority = taskItem.Priority,
                Status = taskItem.Status
            };

            return Result<TaskItemDto>.Success(taskItemDto);
        }
    }
}
