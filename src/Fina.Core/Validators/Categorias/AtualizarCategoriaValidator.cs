using Fina.Core.Requests.Categorias;
using FluentValidation;

namespace Fina.Core.Validators.Categorias;

public class AtualizarCategoriaValidator : AbstractValidator<AtualizarCategoriaRequest>
{
    public AtualizarCategoriaValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ID é obrigatório");

        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("Nome é obrigatório")
            .MinimumLength(3)
            .WithMessage("Nome deve ter pelo menos 3 caracteres");

        RuleFor(x => x.UsuarioId)
           .NotEmpty()
           .WithMessage("Usuário é obrigatório");
    }
}
