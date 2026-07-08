using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transacoes;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transacoes;

public class ObterUltimasTransacoesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/ultimas-transacoes", HandleAsync)
        .WithName("Transacoes: Obter Últimas Transações")
        .WithSummary("Retorna as últimas transações")
        .WithDescription("Retorna as últimas transações")
        .WithOrder(6)
        .Produces<Response<List<Transacao?>>>();

    private static async Task<IResult> HandleAsync(
        ITransacaoHandler handler)
    {
        var request = new ObterUltimasTransacoesRequest
        {
            UsuarioId = ApiConfiguration.UsuarioId,
            QuantidadeUltimasTransacoes = 10
        };

        var result = await handler.ObterUltimasTransacoesAsync(request);
        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
