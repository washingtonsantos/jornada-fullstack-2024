using Fina.Core.Requests.Categorias;
using Fina.Core.Validators.Categorias;
using FluentValidation.TestHelper;

namespace Fina.Api.Tests.Validators.Categorias;

public class AtualizarCategoriaValidatorTests
{
    private readonly AtualizarCategoriaValidator _validator = new();

    [Fact]
    public void Validate_CriaCategoria_ReturnsSuccess()
    {
        var request = new AtualizarCategoriaRequest
        {
            Id = Guid.NewGuid(),
            Nome = "Alimentação",
            Descricao = "Despesas",
            UsuarioId = Guid.NewGuid(),
        };

        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WithEmptyId_ReturnsError()
    {
        var request = new AtualizarCategoriaRequest { Id = Guid.Empty };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Validate_WithEmptyNome_ReturnsError()
    {
        var request = new AtualizarCategoriaRequest { Nome = "" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Nome);
    }

    [Fact]
    public void Validate_WithNomeTooShort_ReturnsError()
    {
        var request = new AtualizarCategoriaRequest { Nome = "AB" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Nome);
    }

    [Fact]
    public void Validate_WithEmptyUsuarioId_ReturnsError()
    {
        var request = new AtualizarCategoriaRequest { UsuarioId = Guid.Empty };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.UsuarioId);
    }
}
