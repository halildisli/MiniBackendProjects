using JwtProject.Context;
using JwtProject.Models;
using JwtProject.Options;
using JwtProject.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JwtProject.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<JwtDbContext>(options => options.UseSqlite(configuration.GetConnectionString("default")));

            services.ConfigureOptions<JwtOptionsSetup>();

            services.ConfigureOptions<JwtBearerOptionsSetup>(); //Tüm konfigusrasyonları farklı noktalardan yönetebilirim bu da Options pattern'i oluyor...

            //services.Configure<JwtOptions>(configuration.GetSection("Jwt"));  //Class ile appsettings.json dosyasında yazdığımız Jwt nesnesini birbiri ile eşleştirmiş olduk. JwtOptions'u çağırdığımızda appsettings.json daki Jwt altındaki değerleri getirmek için kullanıyoruz.

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<JwtDbContext>().AddDefaultTokenProviders(); //Identity için kurallarımı belirlediğim ve sisteme enjekte ettiğim yer.

            services.AddScoped<IJwtTokenProvider, JwtTokenProvider>(); //Dependency injection yaptığım yer.

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
