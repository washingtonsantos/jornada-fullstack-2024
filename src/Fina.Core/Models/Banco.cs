namespace Fina.Core.Models;

public class Banco
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string UsuarioId { get; set; } = string.Empty;
}
