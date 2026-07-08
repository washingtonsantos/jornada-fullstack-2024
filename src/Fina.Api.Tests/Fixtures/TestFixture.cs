namespace Fina.Api.Tests.Fixtures;

/// <summary>
/// Fixture base para testes da aplicação.
/// Fornece configuração comum para todos os testes.
/// </summary>
public class TestFixture : IDisposable
{
    public TestFixture()
    {
        // Inicialização comum para testes
    }

    public void Dispose()
    {
        // Limpeza após testes
    }
}

/// <summary>
/// Coleção de testes que compartilham o mesmo fixture.
/// Use [Collection("Test Collection")] nos testes que precisam deste fixture.
/// </summary>
[CollectionDefinition("Test Collection")]
public class TestCollection : ICollectionFixture<TestFixture>
{
    // Esta classe não tem código, é apenas usada para agrupar fixtures
}
