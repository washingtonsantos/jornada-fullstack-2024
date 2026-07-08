# xUnit Test Project Structure para Fina.Api

Guia de boas práticas para organizar e escrever testes com xUnit.

## 📁 Estrutura de Diretórios

```
src/Fina.Api.Tests/
├── Fixtures/          # Configurações compartilhadas entre testes
├── Mocks/            # Factory e configuração de mocks
├── Builders/         # Builders para criar dados de teste
├── Unit/             # Testes unitários
├── Integration/      # Testes de integração
├── Common/           # Utilitários comuns
└── Configuração.cs   # Configuração global
```

## 🧪 Tipos de Testes

### 1. Unit Tests (`Unit/`)
Testes de uma única função ou método em isolamento.

```csharp
[Fact]
public void Method_Condition_ExpectedResult()
{
	// Arrange
	var input = "value";

	// Act
	var result = MyClass.MyMethod(input);

	// Assert
	result.Should().Be(expected);
}
```

**Convenção de Nomenclatura**: `{ClassUnderTest}_{Scenario}_{ExpectedResult}`

### 2. Theory Tests (Dados Parametrizados)
Testes que executam com múltiplos valores.

```csharp
[Theory]
[InlineData(2, 4)]
[InlineData(3, 9)]
public void Square_WithNumbers_ReturnsCorrectResult(int input, int expected)
{
	var result = Math.Pow(input, 2);
	result.Should().Be(expected);
}
```

### 3. Integration Tests (`Integration/`)
Testes que verificam múltiplos componentes funcionando juntos.

```csharp
public class MyIntegrationTests : IAsyncLifetime
{
	private WebApplicationFactory<Program>? _factory;
	private HttpClient? _client;

	public async Task InitializeAsync()
	{
		_factory = new WebApplicationFactory<Program>();
		_client = _factory.CreateClient();
		await Task.CompletedTask;
	}

	public async Task DisposeAsync()
	{
		_client?.Dispose();
		await (_factory?.DisposeAsync() ?? ValueTask.CompletedTask);
	}

	[Fact]
	public async Task GetEndpoint_ReturnsOk()
	{
		var response = await _client!.GetAsync("/api/endpoint");
		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}
}
```

## 🔧 Mocks e Fixtures

### Usar MockFactory para criar mocks consistentes

```csharp
var loggerMock = MockFactory.CreateLoggerMock<MyClass>();
var serviceProviderMock = MockFactory.CreateServiceProviderMock();
```

### Fixtures compartilhadas entre testes

```csharp
[Collection("Test Collection")]
public class MyTests
{
	private readonly TestFixture _fixture;

	public MyTests(TestFixture fixture)
	{
		_fixture = fixture;
	}
}
```

## 🏗️ Test Builders

Use builders para criar dados de teste complexos:

```csharp
var data = TestDataBuilder.CreateDefault()
	.WithName("John Doe")
	.WithEmail("john@example.com")
	.BuildDictionary();
```

## 📊 Assertions com FluentAssertions

```csharp
// Strings
result.Should().NotBeNullOrEmpty();
result.Should().StartWith("prefix");
result.Should().Contain("substring");

// Números
value.Should().BeGreaterThan(0);
value.Should().BeLessThanOrEqualTo(100);

// Coleções
list.Should().HaveCount(5);
list.Should().Contain("item");
list.Should().AllSatisfy(x => x > 0);

// Exceções
action.Should().Throw<ArgumentException>();

// Objetos
obj.Should().BeOfType<MyType>();
obj.Should().HaveProperty("Name").WithValue("John");
```

## 🚀 Executar Testes

```powershell
# Executar todos os testes
dotnet test

# Executar com verbosidade
dotnet test -v normal

# Executar testes específicos
dotnet test --filter "NameOfTest"

# Gerar relatório de cobertura
dotnet test /p:CollectCoverage=true /p:CoverageFormat=opencover
```

## 📋 Checklist para Novos Testes

- [ ] Nomear teste seguindo padrão: `{Method}_{Condition}_{Result}`
- [ ] Separar em seções: Arrange, Act, Assert
- [ ] Usar builders para dados complexos
- [ ] Usar mocks via MockFactory
- [ ] Usar FluentAssertions para assertivas
- [ ] Testar casos positivos E negativos
- [ ] Colocar em pasta apropriada (Unit, Integration, etc)
- [ ] Adicionar XML comments documentando o teste
- [ ] Executar localmente antes de commitar

## 🔗 Recursos Úteis

- [xUnit Documentation](https://xunit.net/)
- [Moq Documentation](https://github.com/Moq/moq4/wiki/Quickstart)
- [FluentAssertions Documentation](https://fluentassertions.com/)
- [WebApplicationFactory](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/test-asp-net-core-mvc-apps)

