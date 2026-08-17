using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TodoApp.Application.Contracts;
using TodoApp.Shared.Constants;

namespace TodoApp.Infrastructure.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly HttpContext? _httpContext;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public string GetUserId()
        {
            return _httpContext.User.FindFirstValue(IdentityClaims.UserId) ?? string.Empty;
        }
    }
}
