using Fina.Api.Common.Api;
using Fina.Api.Endpoints.Bancos;
using Fina.Api.Endpoints.Categorias;
using Fina.Api.Endpoints.Dashboard;
using Fina.Api.Endpoints.Transacoes;

namespace Fina.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("/")
           .WithTags("Healt Check")
           .MapGet("/", () => new { message = "OK" });

        endpoints.MapGroup("api/v1/dashboard")
            .WithTags("Dashboard")
            //.RequireAuthorization()
            .MapEndpoint<ObterDashboardPorPeriodoEndpoint>();

        endpoints.MapGroup("api/v1/bancos")
            .WithTags("Bancos")
            //.RequireAuthorization()
            .MapEndpoint<CriarBancoEndpoint>()
            .MapEndpoint<AtualizarBancoEndpoint>()
            .MapEndpoint<ExcluirBancoEndpoint>()
            .MapEndpoint<ObterBancoPorIdEndpoint>()
            .MapEndpoint<ObterTodosBancosEndpoint>();

        endpoints.MapGroup("api/v1/categorias")
            .WithTags("Categorias")
            //.RequireAuthorization()
            .MapEndpoint<CriarCategoriaEndpoint>()
            .MapEndpoint<AtualizarCategoriaEndpoint>()
            .MapEndpoint<ExcluirCategoriaEndpoint>()
            .MapEndpoint<ObterCategoriaPorIdEndpoint>()
            .MapEndpoint<ObterTodasSubCategoriasEndpoint>()
            .MapEndpoint<ObterTodasCategoriasEndpoint>();

        endpoints.MapGroup("api/v1/transacoes")
            .WithTags("Transacoes")
            //.RequireAuthorization()
            .MapEndpoint<CriarTransacaoEndpoint>()
            .MapEndpoint<AtualizarTransacaoEndpoint>()
            .MapEndpoint<ExcluirTransacaoEndpoint>()
            .MapEndpoint<ObterTransacaoPorIdEndpoint>()
            .MapEndpoint<ObterTransacaoPorPeriodoEndpoint>()
            .MapEndpoint<ObterUltimasTransacoesEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder endpoint)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(endpoint);
        return endpoint;
    }
}
