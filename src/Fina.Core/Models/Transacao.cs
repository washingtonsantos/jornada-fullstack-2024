using Fina.Core.Enums;

namespace Fina.Core.Models;

public class Transacao
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime? PagoRecebidoEm { get; set; }
    public TipoTransacao TipoTransacao { get; set; }
    public decimal Valor { get; set; }
    public StatusTransacao StatusTransacao { get; set; }
    public Guid CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;
    public Guid? SubCategoriaId { get; set; }
    public SubCategoria? SubCategoria { get; set; } = null!;
    public TipoPagamentoRecebimento? FormaPagamentoRecebimento { get; set; }
    public Guid? ContaId { get; set; }
    public Guid UsuarioId { get; set; }
    public bool Efetivado { get; set; }
    public DateTime? DataEfetivado { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    public void AtualizarStatus()
    {
        if (Efetivado)
        {
            StatusTransacao = StatusTransacao.Efetivado;
        }
        else if (PagoRecebidoEm.HasValue && (PagoRecebidoEm.Value > DateTime.UtcNow.Date))
        {
            StatusTransacao = StatusTransacao.Agendado;
        }
        else
        {
            StatusTransacao = StatusTransacao.Pendente;
        }
        
    }
}
