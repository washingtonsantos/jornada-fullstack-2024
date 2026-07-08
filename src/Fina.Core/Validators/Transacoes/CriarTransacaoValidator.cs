using Fina.Core.Requests.Transacoes;
using FluentValidation;

namespace Fina.Core.Validators.Transacoes;

public class CriarTransacaoValidator : AbstractValidator<CriarTransacaoRequest>
{
    public CriarTransacaoValidator()
    {
        // Titulo
        RuleFor(x => x.Titulo)
            .NotEmpty()
            .WithMessage("Título é obrigatório")
            .MinimumLength(3)
            .WithMessage("Título deve ter pelo menos 3 caracteres")
            .MaximumLength(80)
            .WithMessage("Título pode ter no máximo 80 caracteres");

        // Valor
        RuleFor(x => x.Valor)
            .NotEmpty()
            .WithMessage("Valor é obrigatório")
            .GreaterThan(0)
            .WithMessage("Valor deve ser maior que 0")
            .LessThanOrEqualTo(999999.99m)
            .WithMessage("Valor deve ser menor que 999.999,99");

        // Tipo Transacao
        RuleFor(x => x.TipoTransacao)
            .NotEmpty()
            .WithMessage("Tipo de transação é obrigatório")
            .IsInEnum()
            .WithMessage("Tipo de transação inválido");

        // Categoria
        RuleFor(x => x.CategoriaId)
            .NotEmpty()
            .WithMessage("Categoria é obrigatória");

        // Data Pagamento/Recebimento (opcional, mas se fornecida, validar)
        RuleFor(x => x.PagoOuRecebidoEm)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Data não pode ser no futuro")
            .When(x => x.PagoOuRecebidoEm.HasValue);

        // Forma Pagamento (validar comprimento se fornecido)
        RuleFor(x => x.FormaPagamentoRecebimento)
            .MaximumLength(50)
            .WithMessage("Forma de pagamento deve ter no máximo 50 caracteres")
            .When(x => !string.IsNullOrEmpty(x.FormaPagamentoRecebimento));
    }
}
