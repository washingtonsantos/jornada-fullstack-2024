using Fina.Core.Enums;

namespace Fina.Core.Models;

public class Conta
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public TipoConta TipoConta { get; set; }
    public decimal Saldo { get; set; }
    public decimal? Limite { get; set; }
    public bool Ativo { get; set; }
    public DateTime CriadoEm { get; set; }
    public Guid UsuarioId { get; set; }
}
