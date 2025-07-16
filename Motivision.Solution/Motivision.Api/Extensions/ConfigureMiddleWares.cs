using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Motivision.Api.Middlewares;
using Motivision.Core.Identity.Entities;
using Motivision.Infrastructure.Persistence;
using Motivision.Infrastructure.Persistence.Identity;

namespace Motivision.Api.Extensions
{
    public static class ConfigureMiddleWares
    {
        public static async Task<WebApplication> ConfigureMiddleWaresAsync(this WebApplication app)
        {

            #region Update-Database Explicity

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var _dbContext = services.GetRequiredService<AppBusinessDbContext>();
            var identityContext = services.GetRequiredService<AppIdentityDbContext>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();


            try
            {
                await _dbContext.Database.MigrateAsync();
                // await StoreContextSeed.SeedAsync(_dbContext);
                await identityContext.Database.MigrateAsync();
                await AppIdentityDbContextSeed.SeedUserAsync(userManager);
            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during migration");
            }

            #endregion

            #region Configure Kestrel Middlewares

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");  // Not Found

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            #endregion

            return app;
        }
    }
}