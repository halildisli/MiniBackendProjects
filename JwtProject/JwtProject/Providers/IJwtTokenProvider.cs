using JwtProject.Models;

namespace JwtProject.Providers
{
    public interface IJwtTokenProvider
    {
        string GenerateToken(AppUser user);
    }
}
