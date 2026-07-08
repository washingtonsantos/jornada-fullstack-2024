using Fina.Api.Common.Api;
using Fina.Core;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categorias;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Categorias;

public class ObterTodasCategoriasEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
                .WithName("Categorias: Obter Todas")
                .WithSummary("Retorna todas as Categorias")
                .WithDescription("Retorna todas as Categorias")
                .WithOrder(5)
                .Produces<PagedResponse<List<Categoria?>>>();

    private static async Task<IResult> HandleAsync(
        ICategoriaHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize) 
    {
        var request = new ObterTodasCategoriasRequest
        {
            UsuarioId = ApiConfiguration.UsuarioId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await handler.ObterTodosAsync(request);
        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
