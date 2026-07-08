using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categorias;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Categorias;

public class ExcluirCategoriaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapDelete("/{id}", HandleAsync)
            .WithName("Categorias: Excluir")
            .WithSummary("Exclui uma Categoria")
            .WithDescription("Exclui uma Categoria")
            .WithOrder(3)
            .Produces<Response<Categoria?>>();

    private static async Task<IResult> HandleAsync(
        ICategoriaHandler handler,
        Guid id)
    {
        var request = new RemoverCategoriaRequest
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
