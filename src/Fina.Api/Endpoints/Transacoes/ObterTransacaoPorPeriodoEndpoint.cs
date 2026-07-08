using Fina.Api.Common.Api;
using Fina.Core;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transacoes;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Transacoes;

public class ObterTransacaoPorPeriodoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
        .WithName("Transacoes: Obter Transação por Período")
        .WithSummary("Retorna Transações em um período")
        .WithDescription("Retorna Transações em um período")
        .WithOrder(5)
        .Produces<PagedResponse<List<Transacao?>>>();

    private static async Task<IResult> HandleAsync(
        ITransacaoHandler handler,
        [FromQuery] DateTime? dataInicio = null,
        [FromQuery] DateTime? dataFim = null,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new ObterTransacaoPorPeriodoRequest
        {
            UsuarioId = ApiConfiguration.UsuarioId,
            DataInicio = dataInicio,
            DataFim = dataFim,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await handler.ObterTransacaoPorPeriodoAsync(request);
        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
