using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transacoes;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transacoes;

public class AtualizarTransacaoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/{id}", HandleAsync)
            .WithName("Transacoes: Atualizar")
            .WithSummary("Atualiza um Transação")
            .WithDescription("Atualiza uma Transação")
            .WithOrder(2)
            .Produces<Response<Transacao>>();

    private static async Task<IResult> HandleAsync(
        ITransacaoHandler handler,
        AtualizarTransacaoRequest request,
        Guid id) 
    {
        request.UsuarioId = ApiConfiguration.UsuarioId;  
        request.Id = id;

        var result = await handler.UpdateAsync(request);
        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
