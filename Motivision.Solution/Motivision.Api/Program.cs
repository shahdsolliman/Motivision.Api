
using Motivision.Api.Extensions;

namespace Motivision.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            webApplicationBuilder.Services.AddApplicationServices(webApplicationBuilder.Configuration);
            webApplicationBuilder.Services.AddIdentityServices(webApplicationBuilder.Configuration);

            var app = webApplicationBuilder.Build();
            await app.ConfigureMiddleWaresAsync();



            app.Run();
        }
    }
}
