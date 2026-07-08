using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categorias;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Categorias;

public class AtualizarCategoriaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Categorias: Atualizar")
            .WithSummary("Atualiza uma Categoria")
            .WithDescription("Atualiza uma Categoria")
            .WithOrder(2)
            .Produces<Response<Categoria?>>();
    
    private static async Task<IResult> HandleAsync(
        ICategoriaHandler handler,
        AtualizarCategoriaRequest request,
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
