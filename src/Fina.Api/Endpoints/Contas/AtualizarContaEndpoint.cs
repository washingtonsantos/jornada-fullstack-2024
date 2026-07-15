using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Contas;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Contas;

public class AtualizarContaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Contas: Atualizar")
            .WithSummary("Atualiza uma Conta")
            .WithDescription("Atualiza uma Conta")
            .WithOrder(2)
            .Produces<Response<Conta?>>();
    
    private static async Task<IResult> HandleAsync(
        IContaHandler handler,
        AtualizarContaRequest request,
        Guid id)
    {
        request.UsuarioId = ApiConfiguration.UsuarioId;
        request.Id = id;

        var result = await handler.AtualizarAsync(request);
        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
