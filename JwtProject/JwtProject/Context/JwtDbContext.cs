using JwtProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JwtProject.Context
{
    public class JwtDbContext:IdentityDbContext<AppUser>
    {
        public JwtDbContext(DbContextOptions<JwtDbContext> options):base(options)
        {
            
        }
    }
}
