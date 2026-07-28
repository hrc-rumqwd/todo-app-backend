using TodoApp.Application.Commons;
using TodoApp.Shared.Commons;
using TodoApp.Shared.Domains.Dtos;

namespace TodoApp.Application.TaskItems.Queries.GetTaskItemById
{
    public record GetTaskItemByIdQuery(long id) : IQuery<Result<TaskItemDto>>;
}
