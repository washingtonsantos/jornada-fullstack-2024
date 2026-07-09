using Fina.Core.Requests.Categorias;
using FluentValidation;

namespace Fina.Core.Validators.Categorias;

public class RemoverCategoriaValidator : AbstractValidator<RemoverCategoriaRequest>
{
    public RemoverCategoriaValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ID é obrigatório");

        RuleFor(x => x.UsuarioId)
           .NotEmpty()
           .WithMessage("Usuário é obrigatório");
    }
}