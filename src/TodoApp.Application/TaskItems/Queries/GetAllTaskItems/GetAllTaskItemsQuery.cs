using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Commons;
using TodoApp.Application.Contracts;
using TodoApp.Application.Extensions;
using TodoApp.Domain.Entities;
using TodoApp.Shared.Commons;
using TodoApp.Shared.Commons.Abstractions;
using TodoApp.Shared.Domains.Dtos;
using TodoApp.Shared.Extensions;

namespace TodoApp.Application.TaskItems.Queries.GetAllTaskItems
{
    public class GetAllTaskItemsQuery : IPaginationBase, IQuery<Result<PaginationResult<TaskItemDto>>>
    {
        [FromQuery(Name = "pageIndex")]
        public int PageIndex { get; set; } = PaginationDefaults.DefaultPageIndex;

        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; } = PaginationDefaults.DefaultPageSize;
        public string[]? Status { get; set; }
    }

    public class GetAllTaskItemsQueryHandler : IQueryHandler<GetAllTaskItemsQuery, Result<PaginationResult<TaskItemDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;

        public GetAllTaskItemsQueryHandler(
            IApplicationDbContext context,
            IUserService userService
            )
        {
            _context = context;
            _userService = userService;
        }

        public async Task<Result<PaginationResult<TaskItemDto>>> Handle(GetAllTaskItemsQuery request, CancellationToken cancellationToken)
        {
            // Search for user
            var requestUid = _userService.GetUserId();
            var author = await _context.GetEntityQuery<AppUser>()
                .Where(u => u.Id.Equals(Guid.Parse(requestUid)))
                .FirstOrDefaultAsync(cancellationToken);

            if(author == null)
                return Result<PaginationResult<TaskItemDto>>.Failure("User not found");

            // Get all task items for the current user 
            var queryable = _context.GetEntityQuery<TaskItem>();
            queryable = queryable.Where(t => requestUid.Equals(t.CreatedBy));

            if (request.Status != null && request.Status.Any())
            {
                queryable = queryable.Where(t => request.Status.Contains(t.Status));
            }

            queryable = queryable
                .Where(t => !t.IsRemove && t.IsActive)
                .ToPaginationQuery(request.PageIndex, request.PageSize);

            var taskItems = await queryable
                .OrderByDescending(t => t.UpdatedAt)
                .ToListAsync(cancellationToken);

            var result = new PaginationResult<TaskItemDto>
            {
                Items = taskItems.Adapt<List<TaskItemDto>>(),
                PageSize = taskItems.Count,
                PageIndex = request.PageIndex,
                TotalRows = await _context.Set<TaskItem>().CountAsync()
            };

            // Match the result with the user information
            foreach (var item in result.Items)
            {
                item.AuthorName = author.FullName;
            }

            return Result<PaginationResult<TaskItemDto>>.Success(result);
        }
    }
}
