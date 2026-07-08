using Fina.Core.Requests.Transacoes;
using FluentValidation;

namespace Fina.Core.Validators.Transacoes;

public class AtualizarTransacaoValidator : AbstractValidator<AtualizarTransacaoRequest>
{
    public AtualizarTransacaoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ID é obrigatório");

        RuleFor(x => x.Titulo)
            .NotEmpty()
            .WithMessage("Título é obrigatório")
            .MinimumLength(3)
            .WithMessage("Título deve ter pelo menos 3 caracteres");

        RuleFor(x => x.Valor)
            .NotEmpty()
            .WithMessage("Valor é obrigatório")
            .GreaterThan(0)
            .WithMessage("Valor deve ser maior que 0");
    }
}
