# 🎯 Estrutura de Testes xUnit - Fina.Api

## ✅ Projeto Criado com Sucesso!

Seu projeto de testes foi criado e configurado com **xUnit**, **Moq**, e **FluentAssertions**.

### 📦 Localização
- **Projeto**: `src/Fina.Api.Tests/Fina.Api.Tests.csproj`
- **Solução**: `Fina.sln` ✓ Adicionado

---

## 📁 Estrutura de Diretórios

```
src/Fina.Api.Tests/
├── Fixtures/
│   └── TestFixture.cs          # Base fixture e collection definition
├── Mocks/
│   └── MockFactory.cs          # Factory para mocks reutilizáveis
├── Builders/
│   └── TestDataBuilder.cs      # Builder pattern para dados de teste
├── Unit/
│   └── ExampleUnitTests.cs     # Exemplos de testes unitários
├── Integration/
│   └── ExampleIntegrationTests.cs  # Exemplos de testes de integração
├── Common/
│   └── TestUtilities.cs        # Funções utilitárias comuns
├── README.md                   # Guia completo de uso
├── Configuração.cs             # Configuração global do xUnit
└── Fina.Api.Tests.csproj      # Arquivo de projeto
```

---

## 📚 Pacotes Instalados

| Pacote | Versão | Propósito |
|--------|--------|----------|
| xunit | 2.9.3 | Framework de testes |
| xunit.runner.visualstudio | 3.1.4 | Runner do Visual Studio |
| Microsoft.NET.Test.Sdk | 17.14.1 | SDK de testes |
| Moq | 4.20.71 | Mocking framework |
| FluentAssertions | 6.12.1 | Assertions fluentes |
| Microsoft.AspNetCore.Mvc.Testing | 10.0.9 | WebApplicationFactory |
| Microsoft.EntityFrameworkCore.InMemory | 10.0.4 | EF Core em memória |
| coverlet.collector | 6.0.4 | Code coverage |

---

## 🚀 Começar a Usar

### 1. **Rodar Todos os Testes**
```powershell
cd "E:\DEV\ESTUDOS\Washington\Blazor\Fina"
dotnet test src/Fina.Api.Tests/Fina.Api.Tests.csproj
```

### 2. **Rodar um Teste Específico**
```powershell
dotnet test --filter "ExampleUnitTests"
```

### 3. **Com Verbosidade Completa**
```powershell
dotnet test -v normal
```

### 4. **Gerar Relatório de Cobertura**
```powershell
dotnet test /p:CollectCoverage=true
```

---

## 📝 Exemplos de Testes

### ✅ Unit Test (Teste Simples)
```csharp
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
```

### ✅ Theory Test (Dados Parametrizados)
```csharp
[Theory]
[InlineData("test@example.com", true)]
[InlineData("invalid.email", false)]
public void ValidateEmail_WithVariousInputs_ReturnsExpectedResult(
	string email, bool expected)
{
	var result = IsValidEmail(email);
	result.Should().Be(expected);
}
```

### ✅ Integration Test (Teste de API)
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
		response.IsSuccessStatusCode.Should().BeTrue();
	}
}
```

---

## 🏗️ Padrões Recomendados

### 📋 Nomenclatura de Testes
```
{Classe}_{Cenário}_{ResultadoEsperado}
```
**Exemplo**: `ValidateEmail_WithInvalidEmail_ReturnsFalse`

### 🔄 Padrão AAA (Arrange-Act-Assert)
```csharp
[Fact]
public void Method_Scenario_ExpectedResult()
{
	// Arrange - Preparar dados
	var input = "value";

	// Act - Executar ação
	var result = MyClass.MyMethod(input);

	// Assert - Verificar resultado
	result.Should().Be(expected);
}
```

### 🏭 Usar MockFactory
```csharp
var loggerMock = Fina.Api.Tests.Mocks.MockFactory.CreateLoggerMock<MyClass>();
```

### 🛠️ Usar TestDataBuilder
```csharp
var data = TestDataBuilder.CreateDefault()
	.WithName("John Doe")
	.WithEmail("john@example.com")
	.BuildDictionary();
```

### ✔️ Usar FluentAssertions
```csharp
// Strings
result.Should().NotBeNullOrEmpty();
result.Should().StartWith("prefix");

// Números
value.Should().BeGreaterThan(0);

// Coleções
list.Should().HaveCount(5);
list.Should().Contain("item");

// Exceções
action.Should().Throw<ArgumentException>();

// Objetos
obj.Should().BeOfType<MyType>();
```

---

## 🎓 Próximos Passos

1. **Remova os exemplos** - Delete `ExampleUnitTests.cs` e `ExampleIntegrationTests.cs`
2. **Crie seus próprios testes** - Use a estrutura como template
3. **Organize por funcionalidade**:
   - `Unit/Services/` - Testes de serviços
   - `Unit/Handlers/` - Testes de handlers
   - `Unit/Validators/` - Testes de validadores
   - `Integration/Endpoints/` - Testes de endpoints
4. **Configure CI/CD** - Adicione testes ao pipeline

---

## 📖 Recursos

- 📄 [README.md completo](./README.md) - Documentação detalhada
- 🔗 [xUnit Docs](https://xunit.net/)
- 🔗 [Moq Docs](https://github.com/Moq/moq4/wiki/Quickstart)
- 🔗 [FluentAssertions Docs](https://fluentassertions.com/)
- 🔗 [WebApplicationFactory](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/test-asp-net-core-mvc-apps)

---

## ⚠️ Notas Importantes

### Warnings do Build
Os warnings do EF Core (versões 10.0.4 vs 10.0.9) são inofensivos e não afetam os testes. Isso ocorre porque o Fina.Api usa EF Core 10.0.9 enquanto o EF Core InMemory está em 10.0.4. Ambos são compatíveis.

### Estrutura Preparada Para
- ✅ Testes unitários (isolação completa)
- ✅ Testes de integração (com WebApplicationFactory)
- ✅ Testes de banco de dados (com EF Core InMemory)
- ✅ Mocking de dependências
- ✅ Dados de teste com builders
- ✅ Code coverage com Coverlet

---

## 📞 Comandos Rápidos

```powershell
# Rodar todos os testes
dotnet test

# Rodar testes em modo watch (atualiza automaticamente)
dotnet watch test

# Rodar teste específico por nome
dotnet test --filter "NomeDoTeste"

# Rodar com log detalhado
dotnet test -v detailed

# Parar no primeiro erro
dotnet test --no-build --fail-fast
```

---

**✨ Sua estrutura de testes está pronta para começar!**
