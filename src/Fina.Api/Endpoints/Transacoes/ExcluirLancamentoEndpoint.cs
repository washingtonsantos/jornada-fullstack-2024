using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transacoes;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transacoes;

public class ExcluirTransacaoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Transacoes: Excluir")
            .WithSummary("Exclui um Transação")
            .WithDescription("Exclui um Transação")
            .WithOrder(3)
            .Produces<Response<Transacao?>>();

    private static async Task<IResult> HandleAsync(
        ITransacaoHandler handler,
        Guid id) 
    {
        var request = new ExcluirLancamentoRequest
        {
            UsuarioId = ApiConfiguration.UsuarioId,
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
