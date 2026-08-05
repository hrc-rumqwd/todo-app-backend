using System.Security.Claims;
using TodoApp.Domain.Entities;
using TodoApp.Shared.Constants;

namespace TodoApp.Domain.Extensions
{
    public static class IdentityExtensions
    {
        public static IEnumerable<Claim> GetClaims(this AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(IdentityClaims.Name, user.UserName),
                new Claim(IdentityClaims.Email, user.Email),
                new Claim(IdentityClaims.PhoneNumber, user.PhoneNumber ?? ""),
                new Claim(IdentityClaims.UserId, user.Id.ToString())
            };

            return claims;
        }
    }
}
