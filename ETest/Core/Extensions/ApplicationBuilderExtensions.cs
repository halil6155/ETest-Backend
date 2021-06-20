using Core.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Core.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseExtendException(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}