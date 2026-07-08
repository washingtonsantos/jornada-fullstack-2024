using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categorias;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Categorias;

public class ObterTodasSubCategoriasEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id:long}/sub-categorias", HandleAsync)
                .WithName("SubCategorias: Obter Todas SubCategorias da Categoria")
                .WithSummary("Retorna todas as SubCategorias da Categoria")
                .WithDescription("Retorna todas as SubCategorias da Categoria")
                .WithOrder(5)
                .Produces<List<SubCategoria?>>();

    private static async Task<IResult> HandleAsync(
        ICategoriaHandler handler,
        [FromRoute] Guid id) 
    {
        var request = new ObterSubCategoriasDaCategoriaIdRequest
        {
            UsuarioId = ApiConfiguration.UsuarioId,
            CategoriaId = id
        };

        var result = await handler.ObterSubCategoriasDaCategoriaIdAsync(request);
        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
