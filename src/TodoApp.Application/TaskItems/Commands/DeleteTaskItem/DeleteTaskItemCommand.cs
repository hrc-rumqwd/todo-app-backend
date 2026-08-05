using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Commons;
using TodoApp.Application.Contracts;
using TodoApp.Domain.Entities;
using TodoApp.Shared.Commons;

namespace TodoApp.Application.TaskItems.Commands.DeleteTaskItem
{
    public record DeleteTaskItemCommand(long Id) : ICommand<Result<bool>>;

    public class DeleteTaskItemCommandHandler : ICommandHandler<DeleteTaskItemCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteTaskItemCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result<bool>> Handle(DeleteTaskItemCommand request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<TaskItem>()
                .AsQueryable()
                .Where(t => t.Id == request.Id);

            var item = await query
                .FirstOrDefaultAsync(cancellationToken);
            if(item != null)
            {
                item.IsRemove = true;
                item.IsActive = false;
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Result<bool>.Success(true);
        }
    }
}
