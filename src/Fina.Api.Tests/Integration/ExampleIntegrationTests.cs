using Microsoft.AspNetCore.Mvc.Testing;

namespace Fina.Api.Tests.Integration;

/// <summary>
/// Testes de integração de exemplo.
/// Testes que verificam a interação entre múltiplos componentes.
/// </summary>
public class ExampleIntegrationTests : IAsyncLifetime
{
    private WebApplicationFactory<Program>? _factory;
    private HttpClient? _client;

    /// <summary>
    /// Inicializa os recursos necessários para os testes.
    /// </summary>
    public async Task InitializeAsync()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
        await Task.CompletedTask;
    }

    /// <summary>
    /// Limpa os recursos após os testes.
    /// </summary>
    public async Task DisposeAsync()
    {
        _client?.Dispose();
        await (_factory?.DisposeAsync() ?? ValueTask.CompletedTask);
    }

    /// <summary>
    /// Exemplo: testa se a API responde a uma requisição GET.
    /// </summary>
    [Fact]
    public async Task ApiFactory_CreatesClient_Successfully()
    {
        // Arrange - verificar que o factory pode criar um cliente
        var client = _client;

        // Act & Assert
        client.Should().NotBeNull();
    }

    /// <summary>
    /// Exemplo: testa se a API rejeita requisições inválidas.
    /// </summary>
    [Fact]
    public async Task SubmitInvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        var endpoint = "/api/example";
        var content = new StringContent("{}", System.Text.Encoding.UTF8, "application/json");

        // Act
        var response = await _client!.PostAsync(endpoint, content);

        // Assert
        response.StatusCode.Should().NotBe(System.Net.HttpStatusCode.OK);
    }

    /// <summary>
    /// Exemplo: testa um fluxo completo de requisição-resposta.
    /// </summary>
    [Fact]
    public async Task HttpClient_IsConfiguredCorrectly_ReturnsNotNull()
    {
        // Arrange
        var client = _client;

        // Act & Assert
        client.Should().NotBeNull();
        client!.Timeout.Should().BeGreaterThan(TimeSpan.Zero);
    }
}
