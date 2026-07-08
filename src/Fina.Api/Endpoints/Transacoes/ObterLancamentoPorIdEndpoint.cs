using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transacoes;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transacoes;

public class ObterTransacaoPorIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id:int}", HandleAsync)
            .WithName("Transacoes: Obter por Id")
            .WithSummary("Retorna Transação por Id")
            .WithDescription("Retorna Transação por Id")
            .WithOrder(4)
            .Produces<Response<Transacao?>>();

    private static async Task<IResult> HandleAsync(
        ITransacaoHandler handler,
        Guid id) 
    {
        var request = new ObterTransacaoPorIdRequest
        {
            UsuarioId = ApiConfiguration.UsuarioId,
            Id = id
        };

        var result = await handler.ObterTransacaoPorIdAsync(request);
        return result.Sucesso 
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
