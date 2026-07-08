using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categorias;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Categorias;

public class CriarCategoriaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Categorias: Criar")
            .WithSummary("Cria uma nova Categoria")
            .WithDescription("Cria uma nova Categoria")
            .WithOrder(1)
            .Produces<Response<Categoria?>>();

    private static async Task<IResult> HandleAsync(
        ICategoriaHandler handler,
        CriarCategoriaRequest request) 
    {
        request.UsuarioId = ApiConfiguration.UsuarioId;

        var result = await handler.CriarAsync(request);
        return result.Sucesso
            ? TypedResults.Created($"v1/categorias/{result.Data?.Id}", result.Data)
            : TypedResults.BadRequest(result);
    }
}
