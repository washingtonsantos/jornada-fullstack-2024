using Fina.Core.Requests.Transacoes;
using FluentValidation;

namespace Fina.Core.Validators.Transacoes;

public class AtualizarTransacaoValidator : AbstractValidator<AtualizarTransacaoRequest>
{
    public AtualizarTransacaoValidator()
    {
        // ID
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ID é obrigatório");

        // Titulo
        RuleFor(x => x.Titulo)
            .NotEmpty()
            .WithMessage("Título é obrigatório")
            .MinimumLength(3)
            .WithMessage("Título deve ter pelo menos 3 caracteres")
            .MaximumLength(255)
            .WithMessage("Título pode ter no máximo 255 caracteres");

        // PagoOuRecebidoEm
        RuleFor(x => x.PagoOuRecebidoEm)
            .NotEqual(DateTime.MinValue)
            .WithMessage("Data pagamento/recebimento é obrigatório");

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

        // Tipo Pagamento/Recebimento
        RuleFor(x => x.TipoPagamentoRecebimento)
            .NotEmpty()
            .WithMessage("Tipo de pagamento/recebimento é obrigatório")
            .IsInEnum()
            .WithMessage("Tipo de pagamento/recebimento inválido");

        // ContaId
        RuleFor(x => x.ContaId)
            .NotEmpty()
            .WithMessage("Conta é obrigatória");

        // Categoria
        RuleFor(x => x.CategoriaId)
            .NotEmpty()
            .WithMessage("Categoria é obrigatória");

        // UsuarioId
        RuleFor(x => x.UsuarioId)
           .NotEmpty()
           .WithMessage("Usuário é obrigatório");
    }
}
