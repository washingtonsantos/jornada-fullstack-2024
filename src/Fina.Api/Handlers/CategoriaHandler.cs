using Fina.Api.Data;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Bancos;
using Fina.Core.Requests.Categorias;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers;

public class CategoriaHandler(AppDbContext context, ILogger<CategoriaHandler> logger) : ICategoriaHandler
{
    public async Task<Response<Categoria?>> CriarAsync(CriarCategoriaRequest request)
    {
        var categoria = new Categoria
        {
            UsuarioId = request.UsuarioId,
            Nome = request.Titulo,
            Descricao = request.Descricao,
        };

        try
        {
            await context.Categorias.AddAsync(categoria);
            await context.SaveChangesAsync();

            return new Response<Categoria?>(categoria, 201, message: "Categoria criada com sucesso.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao criar categoria");
            return new Response<Categoria?>(null, 500, "Não foi possível cadastrar a Categoria.");
        }
    }

    public async Task<Response<Categoria?>> AtualizarAsync(AtualizarCategoriaRequest request)
    {            
        try
        {
            var categoria = await context.Categorias
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UsuarioId == request.UsuarioId);

            if (categoria is null)
                return new Response<Categoria?>(null, 404, "Categoria não encontrada.");

            categoria.Nome = request.Nome;
            categoria.Descricao = request.Descricao;

            context.Categorias.Update(categoria);
            await context.SaveChangesAsync();

            return new Response<Categoria?>(categoria, message: "Categoria atualizada com sucesso.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao atualizar categoria");
            return new Response<Categoria?>(null, 500, "Não foi possível atualizar a Categoria.");
        }
    }

    public async Task<Response<Categoria?>> RemoverAsync(RemoverCategoriaRequest request)
    {
        try
        {
            var categoria = await context.Categorias
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UsuarioId == request.UsuarioId);

            if (categoria is null)
                return new Response<Categoria?>(null, 404, "Categoria não encontrada.");

            context.Categorias.Remove(categoria);
            await context.SaveChangesAsync();

            return new Response<Categoria?>(categoria, message: "Categoria excluída com sucesso.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao remover categoria");
            return new Response<Categoria?>(null, 500, "Não foi possível excluir a Categoria.");
        }
    }

    public async Task<Response<Categoria?>> ObterPorIdAsync(ObterCategoriaPorIdRequest request)
    {
        try
        {
            var categoria = await context
                .Categorias
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UsuarioId == request.UsuarioId);

            return categoria is null
                ? new Response<Categoria?>(null, 404, "Categoria não encontrada.")
                : new Response<Categoria?>(categoria);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao consultar Categoria por Id");
            return new Response<Categoria?>(null, 500, "Não foi possível consultar a Categoria.");
        }
    }

    public async Task<Response<Categoria?>> ObterSubCategoriasDaCategoriaIdAsync(ObterSubCategoriasDaCategoriaIdRequest request)
    {
        try
        {
            var categoria = await context
                .Categorias
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.CategoriaId && x.UsuarioId == request.UsuarioId);

            if (categoria is null) return new Response<Categoria?>(null, 404, "Categoria não encontrada.");

            var subCategorias = await context
                .SubCategorias
                .AsNoTracking()
                .Where(x => x.CategoriaId == categoria.Id && x.UsuarioId == request.UsuarioId)
                .ToListAsync();

            if (subCategorias.Count == 0)
                categoria.SubCategorias = subCategorias;

            return new Response<Categoria?>(categoria);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao consultar Categoria por Id");
            return new Response<Categoria?>(null, 500, "Não foi possível consultar a Categoria.");
        }
    }

    public async Task<PagedResponse<List<Categoria?>>> ObterTodosAsync(ObterTodasCategoriasRequest request)
    {
        var query = context.Categorias
            .AsNoTracking()
            .Where(x => x.UsuarioId == request.UsuarioId)
            .OrderBy(x => x.Nome);

         var categories = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        var count = await query.CountAsync();

        return new PagedResponse<List<Categoria?>>(
            categories, 
            count, 
            request.PageNumber, 
            request.PageSize);
    }
}
