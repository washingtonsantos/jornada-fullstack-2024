using Microsoft.OpenApi;

namespace Fina.Api.Common.Api
{
    public static class AppExtension
    {
        public static void ConfigureDevEnvironment(this WebApplication app)
        {
            app.UseSwagger(cfg =>
                cfg.OpenApiVersion = OpenApiSpecVersion.OpenApi2_0);
            app.UseSwaggerUI(cfg => 
            {
                cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "Fina API v1");
                //cfg.RoutePrefix = string.Empty;
            });
            //app.MapSwagger().RequireAuthorization();
        }
    }
}
