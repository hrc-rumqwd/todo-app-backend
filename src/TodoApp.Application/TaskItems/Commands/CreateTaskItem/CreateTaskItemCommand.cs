using Mapster;
using TodoApp.Application.Commons;
using TodoApp.Application.Contracts;
using TodoApp.Domain.Entities;
using TodoApp.Shared.Commons;
using TodoApp.Shared.Domains.Dtos;
using TodoApp.Shared.Enums;

namespace TodoApp.Application.TaskItems.Commands.CreateTaskItem
{
    public class CreateTaskItemCommand : TaskItemDto, ICommand<Result<TaskItemDto>>
    {
    }

    public class CreateTaskItemCommandHandler : ICommandHandler<CreateTaskItemCommand, Result<TaskItemDto>>
    {
        private readonly IApplicationDbContext _context;
        public CreateTaskItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<TaskItemDto>> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
        {
            var taskItem = new Domain.Entities.TaskItem
            {
                Title = request.Title,
                Priority = request.Priority,
                Status = TaskStatuses.New.ToString(),
                IsActive = true
            };

            await _context.Set<TaskItem>().AddAsync(taskItem, cancellationToken);
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
