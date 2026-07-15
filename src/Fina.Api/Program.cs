using Fina.Api;
using Fina.Api.Common.Api;
using Fina.Api.Endpoints;
using Fina.Core.Validators.Transacoes;
using FluentValidation;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Iniciando o servidor web...");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSerilog((services, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(builder.Configuration)
        .ReadFrom.Services(services));

    builder.Services.AddValidatorsFromAssemblyContaining<CriarTransacaoValidator>();

    builder.AddConfiguration();
    builder.AddDataContext();
    builder.AddCrossOrigin();
    builder.AddDocumentation();
    builder.AddDependencyInjection();
    builder.Services.AddControllers();
    var app = builder.Build();

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
        app.ConfigureDevEnvironment();

    app.UseCors(ApiConfiguration.CorsPolicyName);
    //app.UseHttpsRedirection();
    app.MapEndpoints();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "O aplicativo falhou inesperadamente durante a inicialização.");
}
finally
{
    Log.CloseAndFlush();
}
