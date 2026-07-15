using Fina.Core.Enums;
using Fina.Core.Models;

namespace Fina.Core.Dtos.Transacoes;

public class TransacaoPorPeriodoDto
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public DateTime? PagoRecebidoEm { get; set; }
    public TipoTransacao TipoTransacao { get; set; }
    public decimal Valor { get; set; }
    public StatusTransacao StatusTransacao { get; set; }
    public Categoria Categoria { get; set; } = null!;
    public SubCategoria? SubCategoria { get; set; } = null!;
}
