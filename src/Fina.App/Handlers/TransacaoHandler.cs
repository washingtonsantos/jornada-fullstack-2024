using Fina.Core.Common;
using Fina.Core.Dtos.Transacoes;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transacoes;
using Fina.Core.Responses;
using System.Net.Http.Json;
using System.Transactions;

namespace Fina.App.Handlers;

public class TransacaoHandler(IHttpClientFactory httpClientFactory) : ITransacaoHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);
    private readonly string endpoint = "api/v1/transacoes";
    public Task<Response<Transacao?>> CriarAsync(CriarTransacaoRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Transacao?>> DeleteAsync(ExcluirLancamentoRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Transacao?>> ObterTransacaoPorIdAsync(ObterTransacaoPorIdRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedResponse<List<TransacaoPorPeriodoDto>?>> ObterTransacaoPorPeriodoAsync(ObterTransacaoPorPeriodoRequest request)
    {
        const string format = "yyyy-MM-dd";

        var starDate = request.DataInicio is not null ? request.DataInicio.Value.ToString(format) : DateTime.Now.GetFirstDay().ToString(format);
        var endDate = request.DataFim is not null ? request.DataFim.Value.ToString(format) : DateTime.Now.GetLastDay().ToString(format);

        var url = $"{endpoint}?dataInicio={starDate}&dataFim={endDate}";

        return await _client.GetFromJsonAsync<PagedResponse<List<TransacaoPorPeriodoDto>?>>(url)
            ?? new PagedResponse<List<TransacaoPorPeriodoDto>?>(null, 400, "Falha ao obter as transações");
    }

    public Task<Response<List<Transacao?>>> ObterUltimasTransacoesAsync(ObterUltimasTransacoesRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Transacao?>> UpdateAsync(AtualizarTransacaoRequest request)
    {
        throw new NotImplementedException();
    }
}
