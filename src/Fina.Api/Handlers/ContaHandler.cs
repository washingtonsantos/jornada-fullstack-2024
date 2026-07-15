using Fina.Api.Data;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Contas;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers;

public class ContaHandler(AppDbContext context, ILogger<ContaHandler> logger) : IContaHandler
{
    public async Task<Response<Conta?>> CriarAsync(CriarContaRequest request)
    {
        var conta = new Conta
        {
            UsuarioId = request.UsuarioId,
            Nome = request.Nome,
        };

        try
        {
            await context.Contas.AddAsync(conta);
            await context.SaveChangesAsync();

            return new Response<Conta?>(conta, 201, message: "Conta criada com sucesso.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao criar Conta");
            return new Response<Conta?>(null, 500, "Não foi possível cadastrar a Conta.");
        }
    }

    public async Task<Response<Conta?>> AtualizarAsync(AtualizarContaRequest request)
    {            
        try
        {
            var conta = await context.Contas
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UsuarioId == request.UsuarioId);

            if (conta is null)
                return new Response<Conta?>(null, 404, "Conta não encontrada.");

            conta.Nome = request.Nome;

            context.Contas.Update(conta);
            await context.SaveChangesAsync();

            return new Response<Conta?>(conta, message: "Conta atualizada com sucesso.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao atualizar a Conta");
            return new Response<Conta?>(null, 500, "Não foi possível atualizar a Conta.");
        }
    }

    public async Task<Response<Conta?>> RemoverAsync(RemoverContaRequest request)
    {
        try
        {
            var conta = await context.Contas
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UsuarioId == request.UsuarioId);

            if (conta is null)
                return new Response<Conta?>(null, 404, "Conta não encontrada.");

            context.Contas.Remove(conta);
            await context.SaveChangesAsync();

            return new Response<Conta?>(conta, message: "Conta excluída com sucesso.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao remover Conta");
            return new Response<Conta?>(null, 500, "Não foi possível excluir a Conta.");
        }
    }

    public async Task<Response<Conta?>> ObterPorIdAsync(ObterContaPorIdRequest request)
    {
        try
        {
            var conta = await context
                .Contas
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UsuarioId == request.UsuarioId);

            return conta is null
                ? new Response<Conta?>(null, 404, "Conta não encontrada.")
                : new Response<Conta?>(conta);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao consultar Conta por Id");
            return new Response<Conta?>(null, 500, "Não foi possível consultar a Conta.");
        }
    }

    public async Task<Response<List<Conta?>>> ObterTodosAsync(ObterTodasContasRequest request)
    {
        var query = context.Contas
            .AsNoTracking()
            .Where(x => x.UsuarioId == request.UsuarioId)
            .OrderBy(x => x.Nome);

         var contas = await query
            .ToListAsync();

        var count = await query.CountAsync();

        return new Response<List<Conta?>>(contas!);
    }
}
