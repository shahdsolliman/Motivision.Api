using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Motivision.API.Errors;
using Motivision.Application;
using Motivision.Core.Contracts.Services.Contracts;
using Motivision.Infrastructure.Persistence;
using Motivision.Infrastructure.Persistence.Identity;
using Motivision.API.Helpers;
using System.Linq;
using System.Text.Json.Serialization;
using Motivision.Core.Contracts.Services;
using Motivision.Application.Services;
using Motivision.Core.Contracts;
using Motivision.Infrastructure.UnitOfWork;
using Motivision.Infrastructure.Repositories;


namespace Motivision.Api.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuiltInServices();
            services.AddSwaggerServices();
            services.AddIdentityDbContextServices(configuration);
            services.AddDbContextServices(configuration);
            services.AddAutoMapperServices();
            services.AddUserDefinedServices();

            return services;
        }

        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;

                options.InvalidModelStateResponseFactory = (ActionContext context) =>
                {
                    var errors = context.ModelState
                        .Where(p => p.Value.Errors.Count > 0)
                        .SelectMany(p => p.Value.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToArray();

                    var response = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumMemberConverter());
                    });

            return services;
        }



        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        private static IServiceCollection AddIdentityDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

            return services;
        }

        private static IServiceCollection AddDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppBusinessDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        private static IServiceCollection AddAutoMapperServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));
            return services;
        }


        private static IServiceCollection AddUserDefinedServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IFocusSessionService, FocusSessionService>();
            services.AddScoped<IGoalService, GoalService>();
            services.AddScoped<IGoalStepService, GoalStepService>();


            return services;
        }
    }
}
