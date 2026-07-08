using Fina.Api.Data;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Bancos;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers;

public class BancoHandler(AppDbContext context, ILogger<BancoHandler> logger) : IBancoHandler
{
    public async Task<Response<Banco?>> CriarAsync(CriarBancoRequest request)
    {
        var banco = new Banco
        {
            UsuarioId = request.UsuarioId,
            Nome = request.Nome,
            Descricao = request.Descricao,
        };

        try
        {
            await context.Bancos.AddAsync(banco);
            await context.SaveChangesAsync();

            return new Response<Banco?>(banco, 201, message: "Banco criado com sucesso.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao criar banco");
            return new Response<Banco?>(null, 500, "Não foi possível cadastrar o banco.");
        }
    }

    public async Task<Response<Banco?>> AtualizarAsync(AtualizarBancoRequest request)
    {            
        try
        {
            var banco = await context.Bancos
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UsuarioId == request.UsuarioId);

            if (banco is null)
                return new Response<Banco?>(null, 404, "Banco não encontrado.");

            banco.Nome = request.Nome;
            banco.Descricao = request.Descricao;

            context.Bancos.Update(banco);
            await context.SaveChangesAsync();

            return new Response<Banco?>(banco, message: "Banco atualizado com sucesso.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao atualizar o Banco");
            return new Response<Banco?>(null, 500, "Não foi possível atualizar o Banco.");
        }
    }

    public async Task<Response<Banco?>> RemoverAsync(RemoverBancoRequest request)
    {
        try
        {
            var banco = await context.Bancos
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UsuarioId == request.UsuarioId);

            if (banco is null)
                return new Response<Banco?>(null, 404, "Banco não encontrado.");

            context.Bancos.Remove(banco);
            await context.SaveChangesAsync();

            return new Response<Banco?>(banco, message: "Banco excluído com sucesso.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao remover banco");
            return new Response<Banco?>(null, 500, "Não foi possível excluir o banco.");
        }
    }

    public async Task<Response<Banco?>> ObterPorIdAsync(ObterBancoPorIdRequest request)
    {
        try
        {
            var banco = await context
                .Bancos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UsuarioId == request.UsuarioId);

            return banco is null
                ? new Response<Banco?>(null, 404, "Banco não encontrado.")
                : new Response<Banco?>(banco);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao consultar Banco por Id");
            return new Response<Banco?>(null, 500, "Não foi possível consultar o Banco.");
        }
    }

    public async Task<Response<List<Banco?>>> ObterTodosAsync(ObterTodosBancosRequest request)
    {
        var query = context.Bancos
            .AsNoTracking()
            .Where(x => x.UsuarioId == request.UsuarioId)
            .OrderBy(x => x.Nome);

         var bancos = await query
            .ToListAsync();

        var count = await query.CountAsync();

        return new Response<List<Banco?>>(bancos!);
    }
}
