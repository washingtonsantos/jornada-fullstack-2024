using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categorias;
using Fina.Core.Responses;
using System.Net.Http.Json;

namespace Fina.App.Handlers;

public class CategoriaHandler(IHttpClientFactory httpClientFactory) : ICategoriaHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);
    private readonly string endpointCategorias = "api/v1/categorias";

    public async Task<PagedResponse<List<Categoria?>>> ObterTodosAsync(ObterTodasCategoriasRequest request) =>
             await _httpClient.GetFromJsonAsync<PagedResponse<List<Categoria?>>>(endpointCategorias + $"?pageNumber={request.PageNumber}&pageSize={request.PageSize}") ??
                new PagedResponse<List<Categoria?>>(null, 400, "");

    public async Task<Response<Categoria?>> ObterPorIdAsync(ObterCategoriaPorIdRequest request)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Response<Categoria?>>(endpointCategorias + $"/{request.Id}") ??
                new Response<Categoria?>(null, 400, "Ocorreu um erro ao consultar categoria por Id");
        }
        catch (Exception ex)
        {
            //logger.Error(ex, "Ocorreu um erro ao consultar categoria por Id");
            return new Response<Categoria?>(null, 400, "Ocorreu um erro ao consultar categoria por Id");
        }
    }

    public async Task<Response<Categoria?>> CriarAsync(CriarCategoriaRequest request)
    {
            var response = await _httpClient.PostAsJsonAsync(endpointCategorias, request);

            return await response.Content.ReadFromJsonAsync<Response<Categoria?>>() ??
                new Response<Categoria?>(null, 400, "Ocorreu um erro ao criar categoria");
    }

    public async Task<Response<Categoria?>> AtualizarAsync(AtualizarCategoriaRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(endpointCategorias + "/" + request.Id, request);

            return await response.Content.ReadFromJsonAsync<Response<Categoria?>>() ??
                new Response<Categoria?>(null, 400, "Ocorreu um erro ao atualizar a categoria");
        }
        catch (Exception ex)
        {
            //logger.Error(ex, "Ocorreu um erro ao atualizar a categoria");
            return new Response<Categoria?>(null, 400, "Ocorreu um erro ao atualizar a categoria");
        }
    }

    public async Task<Response<Categoria?>> RemoverAsync(RemoverCategoriaRequest request)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(endpointCategorias + $"/{request.Id}");

            return await response.Content.ReadFromJsonAsync<Response<Categoria?>>() ??
                new Response<Categoria?>(null, 400, "Ocorreu um erro ao excluir a categoria");
        }
        catch (Exception ex)
        {
            //logger.Error(ex, "Ocorreu um erro ao excluir a categoria");
            return new Response<Categoria?>(null, 400, "Ocorreu um erro ao excluir a categoria");
        }
    }

    public Task<Response<Categoria?>> ObterSubCategoriasDaCategoriaIdAsync(ObterSubCategoriasDaCategoriaIdRequest request)
    {
        throw new NotImplementedException();
    }
}
