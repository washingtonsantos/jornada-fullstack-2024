using Fina.Core.Models;
using Fina.Core.Requests.Transacoes;
using Fina.Core.Responses;

namespace Fina.Core.Handlers;

public interface ITransacaoHandler
{
    Task<Response<Transacao?>> CriarAsync(CriarTransacaoRequest request);
    Task<Response<Transacao?>> UpdateAsync(AtualizarTransacaoRequest request);
    Task<Response<Transacao?>> DeleteAsync(ExcluirLancamentoRequest request);
    Task<Response<Transacao?>> ObterTransacaoPorIdAsync(ObterTransacaoPorIdRequest request);
    //Task<PagedResponse<List<Transacao?>>> ObterTransacaoPorPeriodoAsync(ObterTransacaoPorPeriodoRequest request);
    Task<PagedResponse<List<Dtos.Transacoes.TransacaoPorPeriodoDto?>>> ObterTransacaoPorPeriodoAsync(ObterTransacaoPorPeriodoRequest request);
    Task<Response<List<Transacao?>>> ObterUltimasTransacoesAsync(ObterUltimasTransacoesRequest request);

}
