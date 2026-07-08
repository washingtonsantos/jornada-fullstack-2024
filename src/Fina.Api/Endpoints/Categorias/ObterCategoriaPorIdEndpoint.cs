using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categorias;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Categorias;

public class ObterCategoriaPorIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Categorias: Obter por Id")
            .WithSummary("Retorna Categoria por Id")
            .WithDescription("Retorna Categoria por Id")
            .WithOrder(4)
            .Produces<Response<Categoria?>>();

    public static async Task<IResult> HandleAsync(
        ICategoriaHandler handler,
        Guid id)
    {
        var request = new ObterCategoriaPorIdRequest
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
