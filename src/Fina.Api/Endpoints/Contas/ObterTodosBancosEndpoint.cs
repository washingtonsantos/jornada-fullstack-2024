using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Contas;

namespace Fina.Api.Endpoints.Contas;

public class ObterTodosBancosEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
                .WithName("Contas: Obter Todos")
                .WithSummary("Retorna todas as Contas")
                .WithDescription("Retorna todas as Contas")
                .WithOrder(5)
                .Produces<List<Conta?>>();

    private static async Task<IResult> HandleAsync(
        IContaHandler handler) 
    {
        var request = new ObterTodasContasRequest
        {
            UsuarioId = ApiConfiguration.UsuarioId,
        };

        var result = await handler.ObterTodosAsync(request);
        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
