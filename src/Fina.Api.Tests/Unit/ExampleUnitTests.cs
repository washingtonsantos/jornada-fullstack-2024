using Microsoft.Extensions.Logging;
using Fina.Api.Tests.Mocks;

namespace Fina.Api.Tests.Unit;

/// <summary>
/// Testes unitários de exemplo.
/// Cada classe de teste testa uma única classe ou método.
/// </summary>
public class ExampleUnitTests
{
    private readonly Mock<ILogger<ExampleUnitTests>> _mockLogger;

    public ExampleUnitTests()
    {
        _mockLogger = Fina.Api.Tests.Mocks.MockFactory.CreateLoggerMock<ExampleUnitTests>();
    }

    /// <summary>
    /// Exemplo: testa se um valor é validado corretamente.
    /// </summary>
    [Fact]
    public void ValidateNonEmptyString_WithValidString_ReturnsTrue()
    {
        // Arrange
        var testData = "valid string";

        // Act
        var result = !string.IsNullOrWhiteSpace(testData);

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Exemplo: testa se um valor vazio é rejeitado.
    /// </summary>
    [Fact]
    public void ValidateNonEmptyString_WithEmptyString_ReturnsFalse()
    {
        // Arrange
        var testData = string.Empty;

        // Act
        var result = !string.IsNullOrWhiteSpace(testData);

        // Assert
        result.Should().BeFalse();
    }

    /// <summary>
    /// Exemplo: testa múltiplos cenários com Theory (dados parametrizados).
    /// </summary>
    [Theory]
    [InlineData("test@example.com", true)]
    [InlineData("invalid.email", false)]
    [InlineData("another@test.co.uk", true)]
    public void ValidateEmail_WithVariousInputs_ReturnsExpectedResult(string email, bool expected)
    {
        // Arrange & Act
        var result = IsValidEmail(email);

        // Assert
        result.Should().Be(expected);
    }

    /// <summary>
    /// Exemplo de validação de email simples (substituir por sua lógica real).
    /// </summary>
    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Exemplo: testa se exceções são lançadas corretamente.
    /// </summary>
    [Fact]
    public void DivideByZero_ThrowsException()
    {
        // Arrange & Act
        int x = 10;
        int y = 0;
        var action = () => _ = x / y;

        // Assert
        action.Should().Throw<DivideByZeroException>();
    }

    /// <summary>
    /// Exemplo: testa com Builder pattern para dados complexos.
    /// </summary>
    [Fact]
    public void TestDataBuilder_WithCustomValues_BuildsCorrectly()
    {
        // Arrange
        var builder = TestDataBuilder.CreateDefault()
            .WithName("John Doe")
            .WithEmail("john@example.com");

        // Act
        var data = builder.BuildDictionary();

        // Assert
        data.Should().ContainKey("Name");
        data["Name"].Should().Be("John Doe");
        data.Should().ContainKey("Email");
        data["Email"].Should().Be("john@example.com");
    }
}
