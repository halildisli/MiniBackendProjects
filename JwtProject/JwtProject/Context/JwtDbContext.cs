using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JwtProject.Context
{
    public class JwtDbContext:IdentityDbContext
    {
        public JwtDbContext(DbContextOptions<JwtDbContext> options):base(options)
        {
            
        }
    }
}
