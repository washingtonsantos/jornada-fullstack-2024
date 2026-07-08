using Fina.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Transacoes;

public class AtualizarTransacaoRequest : Request
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Titulo inálido")]
    [StringLength(80, ErrorMessage = "O título deve ter entre {2} e {0} caracteres.", MinimumLength = 3)]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tipo Transação inválido.")]
    public TipoTransacao TipoTransacao { get; set; }

    [Required(ErrorMessage = "Valor inválido.")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "Categoria inválida.")]
    public Guid CategoriaId { get; set; }

    [Required(ErrorMessage = "Data inválida.")]
    public DateTime PagoOuRecebidoEm { get; set; }
}
