using Fina.Core.Requests.Transacoes;
using Fina.Core.Validators.Transacoes;
using FluentValidation.TestHelper;

namespace Fina.Api.Tests.Validators.Transacoes;

public class AtualizarTransacaoValidatorTests
{
    private readonly AtualizarTransacaoValidator _validator = new();

    [Fact]
    public void AtualizarTransacao_ReturnsSuccess()
    {
        var request = new AtualizarTransacaoRequest
        {
            Id = Guid.Parse("D86AE0C3-875B-45E2-9A6E-5D93DE485FA1"),
            Titulo = "Salario",
            Descricao = "Salario recebido por empresa XYZ",
            PagoOuRecebidoEm = DateTime.UtcNow,
            TipoTransacao = Core.Enums.TipoTransacao.Receita,
            Valor = 1000.00m,
            CategoriaId = Guid.Parse("D2AFC928-38E8-4D8B-8C40-000000000001"),
            SubCategoriaId = null,
            DataEfetivado = DateTime.UtcNow,
            TipoPagamentoRecebimento = Core.Enums.TipoPagamentoRecebimento.PIX,
            ContaId = Guid.Parse("bd7c019b-aa34-4447-9366-bc0e3366312d"),
            Efetivado = true,
            Recorrente = false,
            UsuarioId = Guid.Parse("DDF53685-7DFC-4593-BDA7-2FA5305C4283")
        };

        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void ComTituloEmBranco_ReturnsError()
    {
        var request = new AtualizarTransacaoRequest { Titulo = "" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Titulo);
    }

    [Fact]
    public void ComTituloCurto_ReturnsError()
    {
        var request = new AtualizarTransacaoRequest { Titulo = "AB" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Titulo);
    }

    [Fact]
    public void ComPagoOuRecebidoEmInvalido_ReturnsError()
    {
        var request = new AtualizarTransacaoRequest { PagoOuRecebidoEm = DateTime.MinValue };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.PagoOuRecebidoEm);
    }

    [Fact]
    public void ComTipoTransacaoInvalida_ReturnsError()
    {
        var request = new AtualizarTransacaoRequest { TipoTransacao = 0 };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.TipoTransacao);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ComValorInvalido_ReturnsError(decimal valor)
    {
        var request = new AtualizarTransacaoRequest { Valor = valor };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Valor);
    }

    [Fact]
    public void ComCategoriaIdEmBranco_ReturnsError()
    {
        var request = new AtualizarTransacaoRequest { CategoriaId = Guid.Empty };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.CategoriaId);
    }

    [Fact]
    public void ComTipoPagamentoRecebimento_ReturnsError()
    {
        var request = new AtualizarTransacaoRequest { TipoPagamentoRecebimento = 0 };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.TipoPagamentoRecebimento);
    }

    [Fact]
    public void ComContaIdEmBranco_ReturnsError()
    {
        var request = new AtualizarTransacaoRequest { ContaId = Guid.Empty };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.ContaId);
    }

    [Fact]
    public void ComUsuarioIdEmBranco_ReturnsError()
    {
        var request = new AtualizarTransacaoRequest { UsuarioId = Guid.Empty };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.UsuarioId);
    }
}
