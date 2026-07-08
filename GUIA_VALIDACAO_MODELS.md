# 🔍 Guia: Melhor Forma de Validar Models no Fina

## 📊 Análise da Estrutura Atual

Seu projeto usa:
- ✅ **Fina.Core**: Models, Requests, Enums
- ✅ **Fina.Api**: Handlers, Endpoints, Mappings
- ✅ **Data Annotations**: `[Required]`, `[StringLength]` nas Requests
- ✅ **Handlers**: Aplicam lógica de negócio

**Situação Atual**:
```
Request (com ValidationAttributes)
  ↓
Handler (valida lógica de negócio)
  ↓
Model (persistência)
```

---

## 🎯 3 Abordagens Recomendadas para .NET 10

### Opção 1: **FluentValidation** (⭐ RECOMENDADO)

**Quando usar**: Validações complexas, regras de negócio, reutilização

**Vantagens**:
- ✅ Separação clara de responsabilidades
- ✅ Reutilizável entre endpoints diferentes
- ✅ Testável isoladamente
- ✅ Mensagens de erro padronizadas
- ✅ Integração nativa com ASP.NET Core
- ✅ Suporta validação assíncrona
- ✅ Cascata de validação

**Estrutura**:
```
Fina.Core/
├── Validators/
│   ├── Categorias/
│   │   ├── CriarCategoriaValidator.cs
│   │   ├── AtualizarCategoriaValidator.cs
│   │   └── RemoverCategoriaValidator.cs
│   └── Transacoes/
│       ├── CriarTransacaoValidator.cs
│       ├── AtualizarTransacaoValidator.cs
│       └── CriarTransacaoValidator.cs
└── Requests/
	├── Categorias/
	│   └── CriarCategoriaRequest.cs (SEM DataAnnotations)
	└── Transacoes/
		└── CriarTransacaoRequest.cs (SEM DataAnnotations)
```

---

### Opção 2: **Data Annotations (Built-in)** 

**Quando usar**: Validações simples, prototipos, projetos menores

**Vantagens**:
- ✅ Nenhuma dependência extra
- ✅ Rápido de implementar
- ✅ Integração automática no ASP.NET Core
- ✅ Já está sendo usado

**Desvantagens**:
- ❌ Limitado a validações "dumb"
- ❌ Difícil testar isoladamente
- ❌ Validações assíncronas complicadas
- ❌ Sem suporte a regras de negócio complexas

**Estrutura**: A que você já tem (simplifique)

---

### Opção 3: **Validadores Customizados com Herança**

**Quando usar**: Validações específicas do domínio, máximo controle

**Vantagens**:
- ✅ Controle total
- ✅ Sem dependências externas
- ✅ Alinhado com seu domínio (Handlers)

**Desvantagens**:
- ❌ Mais verboso
- ❌ Menos reutilizável
- ❌ Não integra automaticamente no pipeline

---

## 🏆 Recomendação Final: FluentValidation

Para seu projeto, recomendo **FluentValidation** porque:

1. ✅ Você já tem estrutura profissional (Handlers, Requests, Models separadas)
2. ✅ Suas validações vão crescer além de `[Required]` e `[StringLength]`
3. ✅ Precisa validar regras de negócio (ex: valor > 0, categoria exist, etc)
4. ✅ .NET 10 suporta nativamente
5. ✅ Extremamente testável
6. ✅ Comunidade grande e bem documentada

---

## 📝 Plano de Implementação

### Fase 1: Preparação (hoje)
1. Instalar FluentValidation
2. Criar estrutura de Validators
3. Migrar validações de Requests

### Fase 2: Implementação (esta semana)
1. Criar validators para Transacao
2. Criar validators para Categoria
3. Testar com seu teste xUnit

### Fase 3: Refinamento (próximas semanas)
1. Adicionar validações assíncronas
2. Integrar com sua lógica de negócio
3. Cobertura de testes

---

## 🚀 Como Implementar com FluentValidation

### 1. Instalar Pacote
```bash
dotnet add package FluentValidation -v 11.10.0
```

### 2. Criar Validators em Fina.Core

**Exemplo: CategoriaValidator.cs**
```csharp
using FluentValidation;
using Fina.Core.Requests.Categorias;

namespace Fina.Core.Validators.Categorias;

public class CriarCategoriaValidator : AbstractValidator<CriarCategoriaRequest>
{
	public CriarCategoriaValidator()
	{
		// Titulo
		RuleFor(x => x.Titulo)
			.NotEmpty()
			.WithMessage("Título é obrigatório")
			.MinimumLength(3)
			.WithMessage("Título deve ter pelo menos 3 caracteres")
			.MaximumLength(80)
			.WithMessage("Título deve ter no máximo 80 caracteres")
			.Matches(@"^[a-zA-Z0-9\s\-áéíóúãõç]+$")
			.WithMessage("Título contém caracteres inválidos");

		// Descricao (opcional, mas se fornecida, validar)
		RuleFor(x => x.Descricao)
			.MaximumLength(500)
			.WithMessage("Descrição deve ter no máximo 500 caracteres")
			.When(x => !string.IsNullOrEmpty(x.Descricao));
	}
}

public class AtualizarCategoriaValidator : AbstractValidator<AtualizarCategoriaRequest>
{
	public AtualizarCategoriaValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty()
			.WithMessage("ID é obrigatório");

		RuleFor(x => x.Titulo)
			.NotEmpty()
			.WithMessage("Título é obrigatório")
			.MinimumLength(3)
			.WithMessage("Título deve ter pelo menos 3 caracteres");
	}
}
```

**Exemplo: TransacaoValidator.cs**
```csharp
using FluentValidation;
using Fina.Core.Enums;
using Fina.Core.Requests.Transacoes;

namespace Fina.Core.Validators.Transacoes;

public class CriarTransacaoValidator : AbstractValidator<CriarTransacaoRequest>
{
	public CriarTransacaoValidator()
	{
		// Titulo
		RuleFor(x => x.Titulo)
			.NotEmpty()
			.WithMessage("Título é obrigatório")
			.MinimumLength(3)
			.WithMessage("Título deve ter pelo menos 3 caracteres")
			.MaximumLength(80)
			.WithMessage("Título pode ter no máximo 80 caracteres");

		// Valor
		RuleFor(x => x.Valor)
			.NotEmpty()
			.WithMessage("Valor é obrigatório")
			.GreaterThan(0)
			.WithMessage("Valor deve ser maior que 0")
			.LessThanOrEqualTo(999999.99m)
			.WithMessage("Valor deve ser menor que 999.999,99");

		// Tipo Transacao
		RuleFor(x => x.TipoTransacao)
			.NotEmpty()
			.WithMessage("Tipo de transação é obrigatório")
			.IsInEnum()
			.WithMessage("Tipo de transação inválido");

		// Categoria
		RuleFor(x => x.CategoriaId)
			.NotEmpty()
			.WithMessage("Categoria é obrigatória");

		// Data Pagamento/Recebimento (opcional, mas se fornecida, validar)
		RuleFor(x => x.PagoOuRecebidoEm)
			.LessThanOrEqualTo(DateTime.UtcNow)
			.WithMessage("Data não pode ser no futuro")
			.When(x => x.PagoOuRecebidoEm.HasValue);

		// Forma Pagamento (validar comprimento se fornecido)
		RuleFor(x => x.FormaPagamentoRecebimento)
			.MaximumLength(50)
			.WithMessage("Forma de pagamento deve ter no máximo 50 caracteres")
			.When(x => !string.IsNullOrEmpty(x.FormaPagamentoRecebimento));
	}
}

public class AtualizarTransacaoValidator : AbstractValidator<AtualizarTransacaoRequest>
{
	public AtualizarTransacaoValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty()
			.WithMessage("ID é obrigatório");

		RuleFor(x => x.Titulo)
			.NotEmpty()
			.WithMessage("Título é obrigatório")
			.MinimumLength(3)
			.WithMessage("Título deve ter pelo menos 3 caracteres");

		RuleFor(x => x.Valor)
			.NotEmpty()
			.WithMessage("Valor é obrigatório")
			.GreaterThan(0)
			.WithMessage("Valor deve ser maior que 0");
	}
}
```

### 3. Registrar no Program.cs

**Em Fina.Api/Program.cs**:
```csharp
// Adicionar FluentValidation
builder.Services
	.AddFluentValidationAutoValidation()
	.AddFluentValidationClientsideAdapters()
	.AddValidatorsFromAssemblyContaining<CriarCategoriaValidator>();

// Se estiver em outro assembly:
builder.Services
	.AddValidatorsFromAssembly(typeof(Fina.Core.Models.Categoria).Assembly);
```

### 4. Remover Data Annotations dos Requests

**Antes**:
```csharp
public class CriarCategoriaRequest : Request
{
	[Required(ErrorMessage = "Título inválido")]
	[StringLength(80, ...)]
	public string Titulo { get; set; } = string.Empty;
}
```

**Depois**:
```csharp
public class CriarCategoriaRequest : Request
{
	public string Titulo { get; set; } = string.Empty;
	public string Descricao { get; set; } = string.Empty;
}
```

---

## 🧪 Como Testar Validators

**Exemplo de teste em seu projeto xUnit**:

```csharp
// src/Fina.Api.Tests/Validators/Categorias/CriarCategoriaValidatorTests.cs

using FluentValidation.TestHelper;
using Fina.Core.Requests.Categorias;
using Fina.Core.Validators.Categorias;

namespace Fina.Api.Tests.Validators.Categorias;

public class CriarCategoriaValidatorTests
{
	private readonly CriarCategoriaValidator _validator;

	public CriarCategoriaValidatorTests()
	{
		_validator = new CriarCategoriaValidator();
	}

	[Fact]
	public void Validate_WithValidData_ReturnsSuccess()
	{
		// Arrange
		var request = new CriarCategoriaRequest
		{
			Titulo = "Alimentação",
			Descricao = "Despesas com alimentação"
		};

		// Act
		var result = _validator.TestValidate(request);

		// Assert
		result.ShouldNotHaveAnyValidationErrors();
	}

	[Fact]
	public void Validate_WithEmptyTitulo_ReturnsError()
	{
		// Arrange
		var request = new CriarCategoriaRequest
		{
			Titulo = "",
			Descricao = "Valid description"
		};

		// Act
		var result = _validator.TestValidate(request);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Titulo);
	}

	[Fact]
	public void Validate_WithTituloTooShort_ReturnsError()
	{
		// Arrange
		var request = new CriarCategoriaRequest
		{
			Titulo = "AB",  // Menos de 3
			Descricao = ""
		};

		// Act
		var result = _validator.TestValidate(request);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Titulo)
			.WithErrorMessage("Título deve ter pelo menos 3 caracteres");
	}

	[Theory]
	[InlineData("")]
	[InlineData("AB")]
	[InlineData("A" + new string('B', 80))]  // Muito longo
	public void Validate_WithInvalidTitulo_ReturnsError(string invalidTitulo)
	{
		// Arrange
		var request = new CriarCategoriaRequest { Titulo = invalidTitulo };

		// Act
		var result = _validator.TestValidate(request);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Titulo);
	}

	[Fact]
	public void Validate_WithValidTitulo_ReturnsSuccess()
	{
		// Arrange
		var request = new CriarCategoriaRequest { Titulo = "Alimentação" };

		// Act
		var result = _validator.TestValidate(request);

		// Assert
		result.ShouldNotHaveValidationErrorFor(x => x.Titulo);
	}
}
```

---

## 🔗 Integrando com Handlers

Seus Handlers **não precisam mudar**! FluentValidation se integra automaticamente:

```csharp
public class CategoriaHandler : ICategoriaHandler
{
	public async Task<Response<Guid>> Create(CreateCategoriaRequest request)
	{
		// Request já foi validado automaticamente
		// pelo FluentValidation middleware do ASP.NET Core

		try
		{
			var categoria = new Categoria
			{
				Id = Guid.NewGuid(),
				Nome = request.Titulo,
				// ... resto da lógica
			};

			// Validações de negócio aqui
			if (await _context.Categorias.AnyAsync(c => 
				c.Nome.ToLower() == categoria.Nome.ToLower() &&
				c.UsuarioId == request.UsuarioId))
			{
				return new Response<Guid>(
					errorCode: "CATEGORIA_JA_EXISTE",
					message: "Categoria com este nome já existe");
			}

			// ... resto da lógica
		}
		catch (Exception ex)
		{
			return new Response<Guid>(exception: ex);
		}
	}
}
```

---

## 📂 Estrutura Proposta Final

```
Fina.Core/
├── Enums/
├── Models/
├── Requests/
│   ├── Categorias/
│   │   ├── CriarCategoriaRequest.cs       (SEM Data Annotations)
│   │   ├── AtualizarCategoriaRequest.cs  (SEM Data Annotations)
│   │   └── RemoverCategoriaRequest.cs    (SEM Data Annotations)
│   └── Transacoes/
│       ├── CriarTransacaoRequest.cs      (SEM Data Annotations)
│       ├── AtualizarTransacaoRequest.cs  (SEM Data Annotations)
│       └── RemoverTransacaoRequest.cs    (SEM Data Annotations)
│
└── Validators/                            ✨ NOVO
	├── Categorias/
	│   ├── CriarCategoriaValidator.cs
	│   ├── AtualizarCategoriaValidator.cs
	│   └── RemoverCategoriaValidator.cs
	└── Transacoes/
		├── CriarTransacaoValidator.cs
		├── AtualizarTransacaoValidator.cs
		└── RemoverTransacaoValidator.cs
```

---

## ✅ Checklist de Implementação

- [ ] Instalar `FluentValidation` via NuGet
- [ ] Criar estrutura `Validators/` em Fina.Core
- [ ] Criar `CriarCategoriaValidator`
- [ ] Criar `AtualizarCategoriaValidator`
- [ ] Criar `CriarTransacaoValidator`
- [ ] Criar `AtualizarTransacaoValidator`
- [ ] Registrar validadores em `Program.cs`
- [ ] Remover `[Required]` e `[StringLength]` dos Requests
- [ ] Criar testes para validators
- [ ] Testar endpoints com dados inválidos
- [ ] Documentar padrão de validação

---

## 💡 Dicas Extras

### 1. Validators Customizados (Assíncrono)
```csharp
RuleFor(x => x.CategoriaId)
	.NotEmpty()
	.WithMessage("Categoria obrigatória")
	.MustAsync(async (categoriaId, ct) =>
		await _context.Categorias.AnyAsync(c => c.Id == categoriaId, ct))
	.WithMessage("Categoria não encontrada");
```

### 2. Composição de Validators
```csharp
public class CriarTransacaoComCategoriaValidator : 
	AbstractValidator<CriarTransacaoRequest>
{
	public CriarTransacaoComCategoriaValidator(
		IValidator<CriarCategoriaRequest> categoriaValidator)
	{
		RuleFor(x => x)
			.SetValidator(categoriaValidator);
	}
}
```

### 3. Suporte a Múltiplos Idiomas
```csharp
RuleFor(x => x.Titulo)
	.NotEmpty()
	.WithName("Título");  // Para tradução automática
```

---

## 🎯 Resumo

| Aspecto | FluentValidation | Data Annotations |
|---------|-----------------|------------------|
| **Complexidade** | Alta ✅ | Baixa |
| **Testabildade** | Excelente ✅ | Pobre |
| **Validação Assíncrona** | Sim ✅ | Complicado |
| **Regras de Negócio** | Sim ✅ | Não |
| **Reutilização** | Sim ✅ | Não |
| **Curva de Aprendizado** | Média | Baixa |
| **Para seu projeto** | ⭐⭐⭐⭐⭐ | ⭐⭐ |

---

**Recomendação**: Implemente FluentValidation e seu projeto ficará muito mais robusto e testável! 🚀
