using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Motivision.API.Errors;
using Motivision.Application;
using Motivision.Core.Contracts.Services.Contracts;
using Motivision.Infrastructure.Persistence;
using Motivision.Infrastructure.Persistence.Identity;
using SnapShop.API.Helpers;

namespace Motivision.Api.Extensions
{
    public static class ApplicationServicesExtenstions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuiltInServices();
            services.AddSwaggerServices();
            services.AddIdentityDbContextServices(configuration);
            services.AddDbContextServices(configuration);
            services.ConfigureInValidResponseServices();
            services.AddAutoMapperServices();

            return services;
        }
        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }

        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddUserDefinedServices();

            return services;
        }

        private static IServiceCollection AddIdentityDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

            return services;
        }
        private static IServiceCollection AddDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppBusinessDbContext>(options => options/*.UseLazyLoadingProxies()*/.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
        private static IServiceCollection AddUserDefinedServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(ITokenService), typeof(TokenService));
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddScoped<IBasketRepository, BasketRepository>();
            //services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            //services.AddScoped(typeof(IOrderService), typeof(OrderService));
            //services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
            //services.AddSingleton<IResponseCacheService, ResponseCacheService>();
            return services;
        }

        private static IServiceCollection ConfigureInValidResponseServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
               options.InvalidModelStateResponseFactory = (ActionContext) =>
               {
                   var errors = ActionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                        .SelectMany(E => E.Value.Errors)
                                                        .Select(E => E.ErrorMessage)
                                                        .ToArray();
                   var response = new ApiValidationErrorResponse
                   {
                       Errors = errors
                   };

                   return new BadRequestObjectResult(response);

               }
            );

            return services;
        }

        private static IServiceCollection AddAutoMapperServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));

            return services;
        }


    }
}
