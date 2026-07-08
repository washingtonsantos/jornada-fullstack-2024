using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Bancos;

namespace Fina.Api.Endpoints.Bancos;

public class ObterTodosBancosEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
                .WithName("Bancos: Obter Todos")
                .WithSummary("Retorna todos os Bancos")
                .WithDescription("Retorna todos os Bancos")
                .WithOrder(5)
                .Produces<List<Banco?>>();

    private static async Task<IResult> HandleAsync(
        IBancoHandler handler) 
    {
        var request = new ObterTodosBancosRequest
        {
            UsuarioId = ApiConfiguration.UsuarioId,
        };

        var result = await handler.ObterTodosAsync(request);
        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
