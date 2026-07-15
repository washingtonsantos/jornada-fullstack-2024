using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Contas;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Contas;

public class ObterContaPorIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Contas: Obter por Id")
            .WithSummary("Retorna Conta por Id")
            .WithDescription("Retorna Conta por Id")
            .WithOrder(4)
            .Produces<Response<Conta?>>();

    public static async Task<IResult> HandleAsync(
        IContaHandler handler,
        Guid id)
    {
        var request = new ObterContaPorIdRequest
        {
            UsuarioId = ApiConfiguration.UsuarioId,
            Id = id
        };

        var result = await handler.ObterPorIdAsync(request);
        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
