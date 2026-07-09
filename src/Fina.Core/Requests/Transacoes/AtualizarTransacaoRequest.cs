using Fina.Core.Enums;

namespace Fina.Core.Requests.Transacoes;

public class AtualizarTransacaoRequest : Request
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; } = string.Empty;
    public TipoTransacao TipoTransacao { get; set; }
    public decimal Valor { get; set; }
    public Guid CategoriaId { get; set; }
    public Guid? SubCategoriaId { get; set; }
    public DateTime PagoOuRecebidoEm { get; set; }
    public TipoPagamentoRecebimento TipoPagamentoRecebimento { get; set; }
    public Guid ContaId { get; set; }
    public bool Efetivado { get; set; }
    public DateTime? DataEfetivado { get; set; }
    public bool Recorrente { get; set; }
}
