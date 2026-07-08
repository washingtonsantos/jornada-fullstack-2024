using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Bancos;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Bancos;

public class ExcluirBancoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapDelete("/{id}", HandleAsync)
            .WithName("Bancos: Excluir")
            .WithSummary("Exclui um Banco")
            .WithDescription("Exclui um Banco")
            .WithOrder(3)
            .Produces<Response<Banco?>>();

    private static async Task<IResult> HandleAsync(
        IBancoHandler handler,
        long id)
    {
        var request = new RemoverBancoRequest
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
