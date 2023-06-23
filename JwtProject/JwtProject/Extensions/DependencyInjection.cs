using JwtProject.Context;

namespace JwtProject.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<JwtDbContext>(options=>options.UseSqlServer)
        }
    }
}
