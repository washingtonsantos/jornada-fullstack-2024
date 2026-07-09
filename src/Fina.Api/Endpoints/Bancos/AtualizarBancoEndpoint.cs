using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Bancos;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Bancos;

public class AtualizarBancoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Bancos: Atualizar")
            .WithSummary("Atualiza um Banco")
            .WithDescription("Atualiza um Banco")
            .WithOrder(2)
            .Produces<Response<Categoria?>>();
    
    private static async Task<IResult> HandleAsync(
        IBancoHandler handler,
        AtualizarBancoRequest request,
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
