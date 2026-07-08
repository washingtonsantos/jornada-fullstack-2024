using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Bancos;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Bancos;

public class ObterBancoPorIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Bancos: Obter por Id")
            .WithSummary("Retorna Banco por Id")
            .WithDescription("Retorna Banco por Id")
            .WithOrder(4)
            .Produces<Response<Banco?>>();

    public static async Task<IResult> HandleAsync(
        IBancoHandler handler,
        long id)
    {
        var request = new ObterBancoPorIdRequest
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
