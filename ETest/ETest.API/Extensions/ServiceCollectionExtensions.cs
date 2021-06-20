using Microsoft.Extensions.DependencyInjection;

namespace ETest.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCorsService(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            });
        }
    }
}