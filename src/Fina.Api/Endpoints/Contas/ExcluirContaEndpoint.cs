using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Contas;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Contas;

public class ExcluirContaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapDelete("/{id}", HandleAsync)
            .WithName("Contas: Excluir")
            .WithSummary("Exclui uma Conta")
            .WithDescription("Exclui uma Conta")
            .WithOrder(3)
            .Produces<Response<Conta?>>();

    private static async Task<IResult> HandleAsync(
        IContaHandler handler,
        Guid id)
    {
        var request = new RemoverContaRequest
        {
            UsuarioId = ApiConfiguration.UsuarioId,
            Id = id
        };

        var result = await handler.RemoverAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result);
    }
}
