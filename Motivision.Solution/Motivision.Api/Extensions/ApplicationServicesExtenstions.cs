using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Motivision.API.Errors;
using Motivision.Application;
using Motivision.Application.Features.FocusSessions.Validators;
using Motivision.Application.Interfaces;
using Motivision.Application.Services;
using Motivision.Core.Contracts.Services.Contracts;
using Motivision.Infrastructure.Persistence;
using Motivision.Infrastructure.Persistence.Identity;
using SnapShop.API.Helpers;
using System.Linq;
using System.Text.Json.Serialization;
using ApplicationAssembly = Motivision.Application.AssemblyReference;


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
            services.AddMediatorServices();
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
                    .AddFluentValidation(fv =>
                      fv.RegisterValidatorsFromAssemblyContaining<CreateFocusSessionValidator>())
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

        private static IServiceCollection AddMediatorServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(ApplicationAssembly).Assembly));
            return services;
        }

        private static IServiceCollection AddUserDefinedServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IFocusSessionService, FocusSessionService>();
            return services;
        }
    }
}
