# 🎯 RESUMO: Validação de Models - Próximos Passos

## 📊 Recomendação: FluentValidation ⭐

Para seu projeto **Fina**, recomendo **FluentValidation** porque:

```
✅ Já tem estrutura profissional (Handlers, Requests, Models)
✅ Validações vão crescer (regras de negócio complexas)
✅ Separação clara de responsabilidades
✅ Extremamente testável com xUnit
✅ .NET 10 suporta nativamente
```

---

## 🚀 Plano de Ação (Esta Semana)

### Dia 1: Preparação
```powershell
# Instalar FluentValidation
cd "E:\DEV\ESTUDOS\Washington\Blazor\Fina"
dotnet add src/Fina.Core/Fina.Core.csproj package FluentValidation -v 11.10.0
dotnet add src/Fina.Api/Fina.Api.csproj package FluentValidation.AspNetCore -v 11.10.0
```

### Dia 1-2: Criar Validators

**1. Crie estrutura de pastas**:
```
src/Fina.Core/Validators/
├── Categorias/
│   ├── CriarCategoriaValidator.cs
│   └── AtualizarCategoriaValidator.cs
└── Transacoes/
	├── CriarTransacaoValidator.cs
	└── AtualizarTransacaoValidator.cs
```

**2. Implementar CriarCategoriaValidator.cs**:
```csharp
using FluentValidation;
using Fina.Core.Requests.Categorias;

namespace Fina.Core.Validators.Categorias;

public class CriarCategoriaValidator : AbstractValidator<CriarCategoriaRequest>
{
	public CriarCategoriaValidator()
	{
		RuleFor(x => x.Titulo)
			.NotEmpty().WithMessage("Título é obrigatório")
			.MinimumLength(3).WithMessage("Mínimo 3 caracteres")
			.MaximumLength(80).WithMessage("Máximo 80 caracteres");

		RuleFor(x => x.Descricao)
			.MaximumLength(500).WithMessage("Máximo 500 caracteres")
			.When(x => !string.IsNullOrEmpty(x.Descricao));
	}
}
```

**3. Implementar CriarTransacaoValidator.cs**:
```csharp
using FluentValidation;
using Fina.Core.Requests.Transacoes;

namespace Fina.Core.Validators.Transacoes;

public class CriarTransacaoValidator : AbstractValidator<CriarTransacaoRequest>
{
	public CriarTransacaoValidator()
	{
		RuleFor(x => x.Titulo)
			.NotEmpty().WithMessage("Título obrigatório")
			.MinimumLength(3).WithMessage("Mínimo 3 caracteres")
			.MaximumLength(80).WithMessage("Máximo 80 caracteres");

		RuleFor(x => x.Valor)
			.NotEmpty().WithMessage("Valor obrigatório")
			.GreaterThan(0).WithMessage("Valor > 0")
			.LessThanOrEqualTo(999999.99m).WithMessage("Máximo 999.999,99");

		RuleFor(x => x.CategoriaId)
			.NotEmpty().WithMessage("Categoria obrigatória");
	}
}
```

### Dia 3: Registrar em Program.cs

**Em `src/Fina.Api/Program.cs`**:
```csharp
// Adicionar após as outras configurações
builder.Services
	.AddFluentValidationAutoValidation()
	.AddFluentValidationClientsideAdapters()
	.AddValidatorsFromAssemblyContaining(typeof(Fina.Core.Models.Categoria).Assembly);
```

### Dia 4: Limpar Requests

**Remove** `[Required]` e `[StringLength]` dos Requests:

**De**:
```csharp
public class CriarCategoriaRequest : Request
{
	[Required(ErrorMessage = "Título inválido")]
	[StringLength(80, MinimumLength = 3)]
	public string Titulo { get; set; } = string.Empty;
}
```

**Para**:
```csharp
public class CriarCategoriaRequest : Request
{
	public string Titulo { get; set; } = string.Empty;
	public string Descricao { get; set; } = string.Empty;
}
```

### Dia 5: Criar Testes

**Arquivo: `src/Fina.Api.Tests/Validators/Categorias/CriarCategoriaValidatorTests.cs`**

```csharp
using FluentValidation.TestHelper;
using Fina.Core.Requests.Categorias;
using Fina.Core.Validators.Categorias;

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
```

---

## 📋 Verdades e Mitos

### ✅ VERDADE
> "FluentValidation substitui Data Annotations completamente"
- Sim! Você fica mais limpo e testável

### ✅ VERDADE
> "Handlers não precisam mudar"
- Certo! Validação acontece no middleware

### ✅ VERDADE
> "Posso integrar com EF Core async"
- Sim! `MustAsync(async (val, ct) => ...)`

### ❌ MITO
> "FluentValidation é pesado"
- Falso! É mais leve que manter Data Annotations + custom logic

### ❌ MITO
> "Preciso passar validador para Handler"
- Falso! ASP.NET Core integra automaticamente

---

## 📚 Recursos Rápidos

| Recurso | Link |
|---------|------|
| FluentValidation Docs | https://docs.fluentvalidation.net/ |
| Validators em ASP.NET Core | https://docs.fluentvalidation.net/dotnet-core |
| Testes com FluentValidation | https://docs.fluentvalidation.net/testing |

---

## ✅ Checklist Semanal

**Segunda-Terça**:
- [ ] Instalar FluentValidation
- [ ] Criar estrutura de pastas Validators/

**Quarta-Quinta**:
- [ ] Implementar CriarCategoriaValidator
- [ ] Implementar CriarTransacaoValidator
- [ ] Registrar em Program.cs

**Sexta**:
- [ ] Criar testes para validators
- [ ] Remover Data Annotations dos Requests
- [ ] Testar endpoints

---

## 💬 Dúvidas Comuns

**P: Vou perder as validações que tenho com Data Annotations?**
R: Não! FluentValidation é mais poderoso. Você ganha validação assíncrona e testes.

**P: Meus endpoints funcionarão do mesmo jeito?**
R: Sim! Erro HTTP 400 com as mesmas mensagens.

**P: Preciso mexer em meus Handlers?**
R: Não! ASP.NET Core cuida da validação automaticamente.

**P: E as validações no Blazor?**
R: FluentValidation gera validação JavaScript automaticamente com `WithName()`.

---

## 🎯 Resultado Esperado

**Depois de implementar**:

```
Request inválido:
POST /api/categorias
{ "titulo": "AB" }

Resposta HTTP 400:
{
  "errors": {
	"Titulo": ["Mínimo 3 caracteres"]
  }
}
```

---

## 🚀 Começar Agora

**Abra seu VS 2026**:
1. Clique em `Package Manager Console`
2. Execute:
```powershell
Install-Package FluentValidation -Version 11.10.0
```
3. Crie a pasta `src/Fina.Core/Validators/`
4. Comece com `CriarCategoriaValidator.cs`

**Tempo estimado**: 30 minutos ⏱️

---

**Para detalhes completos, abra**: `GUIA_VALIDACAO_MODELS.md`

Boa sorte! 🚀
