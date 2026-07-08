namespace Fina.Api.Tests.Builders;

/// <summary>
/// Exemplo de builder para criar objetos de teste.
/// Use o padrão Builder para criar dados de teste complexos de forma legível.
/// </summary>
public class TestDataBuilder
{
    private string _name = "Test Name";
    private string _email = "test@example.com";
    private DateTime _createdAt = DateTime.UtcNow;

    /// <summary>
    /// Define o nome para o objeto de teste.
    /// </summary>
    public TestDataBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    /// <summary>
    /// Define o email para o objeto de teste.
    /// </summary>
    public TestDataBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    /// <summary>
    /// Define a data de criação para o objeto de teste.
    /// </summary>
    public TestDataBuilder WithCreatedAt(DateTime createdAt)
    {
        _createdAt = createdAt;
        return this;
    }

    /// <summary>
    /// Constrói um dicionário com os dados definidos.
    /// </summary>
    public Dictionary<string, string> BuildDictionary()
    {
        return new Dictionary<string, string>
        {
            { "Name", _name },
            { "Email", _email },
            { "CreatedAt", _createdAt.ToString("O") }
        };
    }

    /// <summary>
    /// Cria um novo builder com valores padrão.
    /// </summary>
    public static TestDataBuilder CreateDefault()
    {
        return new TestDataBuilder();
    }
}
