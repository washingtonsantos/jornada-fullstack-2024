using Fina.Api.Data;
using Fina.Api.Handlers;
using Fina.Core;
using Fina.Core.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Common.Api
{
    public static class BuilderExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            ApiConfiguration.ConnectionString = builder.Configuration.GetConnectionString("PostgresConnection") ?? string.Empty;
            Configuration.BackendURL = builder.Configuration.GetValue<string>("BackendURL") ?? string.Empty;
            Configuration.FrontendURL = builder.Configuration.GetValue<string>("FrontendURL") ?? string.Empty;
        }

        public static void AddDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.CustomSchemaIds(n => n.FullName);
            });
        }

        public static void AddDataContext(this WebApplicationBuilder builder) =>
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(ApiConfiguration.ConnectionString)
            .UseSnakeCaseNamingConvention());

        public static void AddCrossOrigin(this WebApplicationBuilder builder) =>
            builder.Services.AddCors(
                options => options.AddPolicy(
                    ApiConfiguration.CorsPolicyName,
                    policy => policy
                    .WithOrigins([
                        Configuration.BackendURL,
                        Configuration.FrontendURL
                        ])
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    ));

        public static void AddDependencyInjection(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IBancoHandler, BancoHandler>();
            builder.Services.AddTransient<ICategoriaHandler, CategoriaHandler>();
            builder.Services.AddTransient<ITransacaoHandler, TransacaoHandler>();
        }
    }
}
