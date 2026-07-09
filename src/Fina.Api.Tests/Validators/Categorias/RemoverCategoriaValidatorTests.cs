using Fina.Core.Requests.Categorias;
using Fina.Core.Validators.Categorias;
using FluentValidation.TestHelper;

namespace Fina.Api.Tests.Validators.Categorias;

public class RemoverCategoriaValidatorTests
{
    private readonly RemoverCategoriaValidator _validator = new();

    [Fact]
    public void Validate_CriaCategoria_ReturnsSuccess()
    {
        var request = new RemoverCategoriaRequest
        {
            Id = Guid.NewGuid(),
            UsuarioId = Guid.NewGuid(),
        };

        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WithEmptyId_ReturnsError()
    {
        var request = new RemoverCategoriaRequest { Id = Guid.Empty };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Validate_WithEmptyUsuarioId_ReturnsError()
    {
        var request = new RemoverCategoriaRequest { UsuarioId = Guid.Empty };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.UsuarioId);
    }
}
