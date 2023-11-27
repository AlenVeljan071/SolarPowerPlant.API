namespace SolarPowerPlant.Api.Extensions
{
    public static class CorsPolicyExtension
    {
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)

          => services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:4200", "http://localhost:4200");
                });
            });


    }
}
