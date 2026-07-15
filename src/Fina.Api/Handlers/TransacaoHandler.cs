using Fina.Api.Data;
using Fina.Core.Common;
using Fina.Core.Dtos.Transacoes;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transacoes;
using Fina.Core.Responses;
using Fina.Core.Validators.Transacoes;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers;

public class TransacaoHandler(AppDbContext context, ILogger<TransacaoHandler> logger) : ITransacaoHandler
{
    public async Task<Response<Transacao?>> CriarAsync(CriarTransacaoRequest request)
    {
        var validator = new CriarTransacaoValidator();
        ValidationResult result = validator.Validate(request);

        if (!result.IsValid) 
            return new Response<Transacao?>(null, 400, string.Join(';', result.Errors));

        ValidarCategoria(result, request.CategoriaId, request.UsuarioId);

        if (!result.IsValid)
            return new Response<Transacao?>(null, 400, string.Join(';', result.Errors));

        //caso a request seja uma saída e o valor é positivo, o valor é convertido para negativo
        if (request is { TipoTransacao: Core.Enums.TipoTransacao.Despesa, Valor: >= 0 })
            request.Valor *= -1;

        try
        {
            var transacao = new Transacao
            {
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                PagoRecebidoEm = request.PagoOuRecebidoEm,
                TipoTransacao = request.TipoTransacao,
                Valor = request.Valor,
                CategoriaId = request.CategoriaId,
                SubCategoriaId = request.SubCategoriaId,
                FormaPagamentoRecebimento = request.FormaPagamentoRecebimento,

            };

            transacao.AtualizarStatus();

            await context.Transacoes.AddAsync(transacao);
            await context.SaveChangesAsync();

            return new Response<Transacao?>(transacao, 201, message: "Transação criada com sucesso.");
        }
        catch (Exception ex)    
        {
            logger.LogError(ex, "Ocorreu um erro ao criar a Transação");
            return new Response<Transacao?>(null, 500, "Ocorreu um erro ao criar a Transação.");
        }
    }

    public async Task<Response<Transacao?>> UpdateAsync(AtualizarTransacaoRequest request)
    {
        //caso a request seja uma saída e o valor é positivo, o valor é convertido para negativo
        if (request is { TipoTransacao: Core.Enums.TipoTransacao.Despesa, Valor: >= 0 })
            request.Valor *= -1;

        try
        {
            var transacao = await context.Transacoes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UsuarioId == request.UsuarioId);

            if (transacao is null)
                return new Response<Transacao?>(null, 404, "Transação não encontrada.");

            transacao.CategoriaId = request.CategoriaId;
            transacao.Valor = request.Valor;
            transacao.Titulo = request.Titulo;
            transacao.TipoTransacao = request.TipoTransacao;
            transacao.PagoRecebidoEm = request.PagoOuRecebidoEm;

            context.Transacoes.Update(transacao);
            await context.SaveChangesAsync();

            return new Response<Transacao?>(transacao, message: "Transação alterada com sucesso.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ocorreu um erro ao atualizar a Transação");
            return new Response<Transacao?>(null, 500, "Ocorreu um erro ao alterar a Transação.");
        }
    }

    public async Task<Response<Transacao?>> DeleteAsync(ExcluirLancamentoRequest request)
    {
        try
        {
            var transacao = await context.Transacoes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UsuarioId == request.UsuarioId);

            if (transacao is null)
                return new Response<Transacao?>(null, 404, "Transação não encontrada.");

            context.Transacoes.Remove(transacao);
            await context.SaveChangesAsync();

            return new Response<Transacao?>(transacao, message: "Transação excluída com sucesso.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ocorreu um erro ao excluir a Transação");
            return new Response<Transacao?>(null, 500, "Ocorreu um erro ao excluir a Transação.");
        }
    }

    public async Task<Response<Transacao?>> ObterTransacaoPorIdAsync(ObterTransacaoPorIdRequest request)
    {
        try
        {
            var transacao = await context.Transacoes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UsuarioId == request.UsuarioId);

            return transacao is null
                ? new Response<Transacao?>(null, 404, "Transação não encontrada.")
                : new Response<Transacao?>(transacao);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ocorreu um erro ao consultar a Transação por Id");
            return new Response<Transacao?>(null, 500, "Ocorreu um erro ao consultar a Transação.");
        }
    }

    public async Task<PagedResponse<List<TransacaoPorPeriodoDto?>>> ObterTransacaoPorPeriodoAsync(ObterTransacaoPorPeriodoRequest request)
    {
        try
        {
            request.DataInicio ??= DateTime.Now.GetFirstDay();
            request.DataFim ??= DateTime.Now.GetLastDay();

            var query = context.Transacoes
                .AsNoTracking()
                .Where(x => x.PagoRecebidoEm >= request.DataInicio &&
                            x.PagoRecebidoEm <= request.DataFim &&
                            x.UsuarioId == request.UsuarioId)
                .OrderBy(x => x.PagoRecebidoEm);

            var Transacoes = await query
                .Skip((request.PageNumber -1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<TransacaoPorPeriodoDto?>>(Transacoes.Select(t => new TransacaoPorPeriodoDto 
            {
                Id = t.Id,
                Titulo = t.Titulo,
                PagoRecebidoEm = t.PagoRecebidoEm,
                TipoTransacao = t.TipoTransacao,
                Valor = t.Valor,
                StatusTransacao = t.StatusTransacao,
                Categoria = t.Categoria,
                SubCategoria = t.SubCategoria
            }).ToList(), 
                count,
                request.PageNumber, 
                request.PageSize);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ocorreu um erro ao buscar Transação por período");
            return new PagedResponse<List<TransacaoPorPeriodoDto?>>(null, 0, request.PageNumber, request.PageSize);
        }
    }

    public async Task<Response<List<Transacao?>>> ObterUltimasTransacoesAsync(ObterUltimasTransacoesRequest request)
    {
        try
        {
            var query = context.Transacoes
                .AsNoTracking()
                .OrderBy(t => t.PagoRecebidoEm);

            var transacoes = await query
                .Take(request.QuantidadeUltimasTransacoes)
                .ToListAsync();

            return transacoes is null
                ? new Response<List<Transacao?>>([])
                : new Response<List<Transacao?>>(transacoes!);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ocorreu um erro ao buscar as últimas Transações");
            return new Response<List<Transacao?>>(null, 500, "Ocorreu um erro ao buscar as últimas Transações");
        }
    }

    private ValidationResult ValidarCategoria(ValidationResult result, Guid categoriaId, Guid usuarioId)
    {
        var categoria = context.Categorias
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == categoriaId && x.UsuarioId == usuarioId);

        if (categoria is null)
            result.Errors.Add(new ValidationFailure("Categoria", "Categoria é obrigatório"));

        return result;
    }
}
