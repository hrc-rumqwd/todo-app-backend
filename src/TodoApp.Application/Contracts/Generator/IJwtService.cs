using System.Security.Claims;
using TodoApp.Shared.Domains.Auth;

namespace TodoApp.Application.Contracts.Generator
{
    public interface IJwtService
    {
        public TokenResult GenerateToken(IEnumerable<Claim> claims);
    }
}
