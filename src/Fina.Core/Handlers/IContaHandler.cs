using Fina.Core.Models;
using Fina.Core.Requests.Contas;
using Fina.Core.Responses;

namespace Fina.Core.Handlers;

public interface IContaHandler
{
    Task<Response<Conta?>> CriarAsync(CriarContaRequest request);
    Task<Response<Conta?>> AtualizarAsync(AtualizarContaRequest request);
    Task<Response<Conta?>> RemoverAsync(RemoverContaRequest request);
    Task<Response<Conta?>> ObterPorIdAsync(ObterContaPorIdRequest request);
    Task<Response<List<Conta?>>> ObterTodosAsync(ObterTodasContasRequest request);
}
