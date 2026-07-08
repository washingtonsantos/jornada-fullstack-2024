using Microsoft.Extensions.Logging;

namespace Fina.Api.Tests.Mocks;

/// <summary>
/// Factory para criar mocks comumente usados nos testes.
/// Centraliza a criação de mocks para facilitar manutenção.
/// </summary>
public static class MockFactory
{
    /// <summary>
    /// Cria um mock de ILogger para testes.
    /// </summary>
    public static Mock<ILogger<T>> CreateLoggerMock<T>()
    {
        return new Mock<ILogger<T>>();
    }

    /// <summary>
    /// Cria um mock que sempre retorna sucesso.
    /// </summary>
    public static Mock<IServiceProvider> CreateServiceProviderMock()
    {
        var mock = new Mock<IServiceProvider>();
        return mock;
    }

    /// <summary>
    /// Cria um MockRepository para melhor gerenciamento de múltiplos mocks.
    /// </summary>
    public static MockRepository CreateRepository()
    {
        return new MockRepository(MockBehavior.Strict);
    }
}
