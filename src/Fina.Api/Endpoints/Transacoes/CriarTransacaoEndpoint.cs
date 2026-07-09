using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transacoes;
using Fina.Core.Responses;
using FluentValidation;

namespace Fina.Api.Endpoints.Transacoes;

public class CriarTransacaoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Transacoes: Criar")
            .WithSummary("Cria uma novo Transação")
            .WithDescription("Cria uma novo Transação")
            .WithOrder(1)
            .Produces<Response<Transacao?>>();

    private static async Task<IResult> HandleAsync(
        IValidator<CriarTransacaoRequest> _validator,
        ITransacaoHandler handler,
        CriarTransacaoRequest request) 
    {        
        request.UsuarioId = ApiConfiguration.UsuarioId;

        var result = await handler.CreateAsync(request);
        return result.Sucesso
            ? TypedResults.Created($"v1/transacoes/{result.Data?.Id}", result) 
            : TypedResults.BadRequest(result);
    }
}
