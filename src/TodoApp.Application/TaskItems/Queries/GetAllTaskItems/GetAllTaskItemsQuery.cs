using TodoApp.Application.Commons;
using TodoApp.Shared.Commons;
using TodoApp.Shared.Domains.Dtos;

namespace TodoApp.Application.TaskItems.Queries.GetAllTaskItems
{
    public class GetAllTaskItemsQuery : IQuery<Result<PaginationResult<TaskItemDto>>>
    {
    }

    public class GetAllTaskItemsQueryHandler : IQueryHandler<GetAllTaskItemsQuery, Result<PaginationResult<TaskItemDto>>>
    {
        public Task<Result<PaginationResult<TaskItemDto>>> Handle(GetAllTaskItemsQuery request, CancellationToken cancellationToken)
        {
            // Implement the logic to retrieve all task items here
            // For example, you can query the database and return the result
            var taskItems = new List<TaskItemDto>
            {
                new TaskItemDto { Id = 1, Title = "Task 1", Priority = "High", Status = "Open", AuthorName = 123 },
                new TaskItemDto { Id = 2, Title = "Task 2", Priority = "Medium", Status = "In Progress", AuthorName = 456 }
            };
            var paginationResult = new PaginationResult<TaskItemDto>
            {
                Items = taskItems,
                PageSize = taskItems.Count,
                PageIndex = 1,
                TotalRows = taskItems.Count
            };
            return Task.FromResult(Result<PaginationResult<TaskItemDto>>.Success(paginationResult));
        }
    }
}
