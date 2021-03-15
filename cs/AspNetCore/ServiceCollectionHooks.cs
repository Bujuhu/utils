using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Utils
{
    public static class ServiceCollectionHooks
    {
        const string CORS_POLICY_ALLOW_ANY = "_allowAny";
        public static void AddCorsAllowAny(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CORS_POLICY_ALLOW_ANY, builder => {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });
        }
        public static void UseCorsAllowAny(this IApplicationBuilder app)
        {
            app.UseCors(CORS_POLICY_ALLOW_ANY);
        }
    }
}
