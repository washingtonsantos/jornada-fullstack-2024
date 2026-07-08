using Fina.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Transacoes;

public class CriarTransacaoRequest : Request
{
    [Required(ErrorMessage = "Titulo inálido")]
    [StringLength(80, ErrorMessage = "O título deve ter entre {2} e {0} caracteres.", MinimumLength = 3)]
    public string Titulo { get; set; } = string.Empty;

    public string? Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tipo Transação inválido.")]
    public TipoTransacao TipoTransacao { get; set; }

    [Required(ErrorMessage = "Valor inválido.")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "Categoria inválida.")]
    public Guid CategoriaId { get; set; }

    public Guid? SubCategoriaId { get; set; }

    public DateTime? PagoOuRecebidoEm { get; set; }

    public string? FormaPagamentoRecebimento { get; set; }

    public string? OrigemPagamentoRecebimento { get; set; }

    public bool Efetivado { get; set; }

    public bool Recorrente { get; set; }

    //Criar outra request para estornos
    //public bool Estornado { get; set; }

    //public DateTime DataEstorno { get; set; }
}
