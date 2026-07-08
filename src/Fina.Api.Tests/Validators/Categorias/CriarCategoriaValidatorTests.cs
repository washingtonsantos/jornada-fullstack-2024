using Fina.Core.Requests.Categorias;
using Fina.Core.Validators.Categorias;
using FluentValidation.TestHelper;

namespace Fina.Api.Tests.Validators.Categorias;

public class CriarCategoriaValidatorTests
{
    private readonly CriarCategoriaValidator _validator = new();

    [Fact]
    public void Validate_WithValidData_ReturnsSuccess()
    {
        var request = new CriarCategoriaRequest
        {
            Titulo = "Alimentação",
            Descricao = "Despesas"
        };

        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WithEmptyTitulo_ReturnsError()
    {
        var request = new CriarCategoriaRequest { Titulo = "" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Titulo);
    }

    [Fact]
    public void Validate_WithTituloTooShort_ReturnsError()
    {
        var request = new CriarCategoriaRequest { Titulo = "AB" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Titulo);
    }
}
