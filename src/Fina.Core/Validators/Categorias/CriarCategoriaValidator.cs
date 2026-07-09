using Fina.Core.Requests.Categorias;
using FluentValidation;

namespace Fina.Core.Validators.Categorias;

public class CriarCategoriaValidator : AbstractValidator<CriarCategoriaRequest>
{
    public CriarCategoriaValidator()
    {
        // Titulo
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("Título é obrigatório")
            .MinimumLength(3)
            .WithMessage("Título deve ter pelo menos 3 caracteres")
            .MaximumLength(80)
            .WithMessage("Título deve ter no máximo 80 caracteres")
            .Matches(@"^[a-zA-Z0-9\s\-áéíóúãõç]+$")
            .WithMessage("Título contém caracteres inválidos");

        // Descricao (opcional, mas se fornecida, validar)
        RuleFor(x => x.Descricao)
            .MaximumLength(500)
            .WithMessage("Descrição deve ter no máximo 500 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Descricao));

        RuleFor(x => x.UsuarioId)
            .NotEmpty()
            .WithMessage("Usuário é obrigatório");
    }
}
