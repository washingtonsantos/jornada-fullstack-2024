using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Contas;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Contas;

public class CriarContaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Contas: Criar")
            .WithSummary("Cria uma nova Conta")
            .WithDescription("Cria um nova Conta")
            .WithOrder(1)
            .Produces<Response<Conta?>>();

    private static async Task<IResult> HandleAsync(
        IContaHandler handler,
        CriarContaRequest request) 
    {
        request.UsuarioId = ApiConfiguration.UsuarioId;

        var result = await handler.CriarAsync(request);
        return result.Sucesso
            ? TypedResults.Created($"v1/contas/{result.Data?.Id}", result.Data)
            : TypedResults.BadRequest(result);
    }
}
