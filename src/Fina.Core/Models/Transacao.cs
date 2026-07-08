using Fina.Core.Enums;

namespace Fina.Core.Models;

public class Transacao
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime? PagoRecebidoEm { get; set; }
    public TipoTransacao TipoTransacao { get; set; }
    public decimal Valor { get; set; }
    public StatusTransacao StatusTransacao { get; set; }
    public Guid CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;
    public Guid? SubCategoriaId { get; set; }
    public SubCategoria? SubCategoria { get; set; } = null!;
    public string? FormaPagamentoRecebimento { get; set; }
    public string? OrigemPagamentoRecebimento { get; set; }
    public string UsuarioId { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}
