using Fina.Core.Models;
using Fina.Core.Requests.Bancos;
using Fina.Core.Responses;

namespace Fina.Core.Handlers;

public interface IBancoHandler
{
    Task<Response<Banco?>> CriarAsync(CriarBancoRequest request);
    Task<Response<Banco?>> AtualizarAsync(AtualizarBancoRequest request);
    Task<Response<Banco?>> RemoverAsync(RemoverBancoRequest request);
    Task<Response<Banco?>> ObterPorIdAsync(ObterBancoPorIdRequest request);
    Task<Response<List<Banco?>>> ObterTodosAsync(ObterTodosBancosRequest request);
}
