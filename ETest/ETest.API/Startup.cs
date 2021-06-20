using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using ETest.API.Extensions;
using ETest.Business.DependencyResolvers.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ETest.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

 
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDb();
            services.AddJwtAuthenticationService(Configuration);
            services.AddMapperProfiles();
            services.AddRepositories();
            services.AddCorsService();
            services.AddCustomCoreModule(new ICoreModule[] {new CoreModule(),});
       
        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ETest"));
            }
            app.UseExtendException();
            app.UseStaticFiles();
            app.UseCors("AllowOrigin");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
