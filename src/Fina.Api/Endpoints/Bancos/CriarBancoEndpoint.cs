using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Bancos;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Bancos;

public class CriarBancoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Bancos: Criar")
            .WithSummary("Cria um novo Banco")
            .WithDescription("Cria um novo Banco")
            .WithOrder(1)
            .Produces<Response<Banco?>>();

    private static async Task<IResult> HandleAsync(
        IBancoHandler handler,
        CriarBancoRequest request) 
    {
        request.UsuarioId = ApiConfiguration.UsuarioId;

        var result = await handler.CriarAsync(request);
        return result.Sucesso
            ? TypedResults.Created($"v1/bancos/{result.Data?.Id}", result.Data)
            : TypedResults.BadRequest(result);
    }
}
