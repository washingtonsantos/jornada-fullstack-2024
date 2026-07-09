namespace Fina.Core.Models;

public class Banco
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public Guid UsuarioId { get; set; }
}
