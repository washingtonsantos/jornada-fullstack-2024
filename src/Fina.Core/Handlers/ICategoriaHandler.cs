using Fina.Core.Models;
using Fina.Core.Requests.Categorias;
using Fina.Core.Responses;

namespace Fina.Core.Handlers;

public interface ICategoriaHandler
{
    Task<Response<Categoria?>> CriarAsync(CriarCategoriaRequest request);
    Task<Response<Categoria?>> AtualizarAsync(AtualizarCategoriaRequest request);
    Task<Response<Categoria?>> RemoverAsync(RemoverCategoriaRequest request);
    Task<Response<Categoria?>> ObterPorIdAsync(ObterCategoriaPorIdRequest request);
    Task<Response<Categoria?>> ObterSubCategoriasDaCategoriaIdAsync(ObterSubCategoriasDaCategoriaIdRequest request);
    Task<PagedResponse<List<Categoria?>>> ObterTodosAsync(ObterTodasCategoriasRequest request);
}
