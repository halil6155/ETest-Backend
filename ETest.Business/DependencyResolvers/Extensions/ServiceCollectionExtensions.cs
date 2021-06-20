using System;
using System.Reflection;
using Core.Utilities.FormFiles.Abstract;
using Core.Utilities.FormFiles.SixLabors;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.Jwt;
using Core.Utilities.Security.Token.Abstract;
using Core.Utilities.Security.Token.Concrete;
using ETest.Business.Abstract;
using ETest.Business.Concrete;
using ETest.DataAccess.Abstract;
using ETest.DataAccess.Concrete.Contexts;
using ETest.DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ETest.Business.DependencyResolvers.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDb(this IServiceCollection services)
        {
            services.AddDbContext<ETestContext>(ServiceLifetime.Scoped);
          

        }
        public static void AddMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
        public static void AddJwtAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    SaveSigninToken = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = tokenOptions.Audience,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)

                };
            });
        }
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IUserDal, UserRepository>();
            services.AddScoped<IUserOperationService, UserOperationManager>();
            services.AddScoped<ITokenService, TokenManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDal, CategoryRepository>();
            services.AddScoped<IQuestionService, QuestionManager>();
            services.AddScoped<IQuestionDal, QuestionRepository>();
            services.AddScoped<IUserAnswerService, UserAnswerManager>();
            services.AddScoped<IUserAnswerDal, UserAnswerRepository>();
            services.AddScoped<IOperationClaimService, OperationClaimManager>();
            services.AddScoped<IOperationClaimDal, OperationClaimRepository>();
            services.AddScoped<IUserOperationClaimDal, UserOperationClaimRepository>();
            services.AddScoped<IUserOperationClaimService, UserOperationClaimManager>();
            services.AddTransient<ITokenHelper, JwtHelper>();
            services.AddTransient<IImageCrud, ImageCrud>();
            services.AddScoped<IMainService, MainManager>();
        }
    }
}