using Fina.Core.Enums;

namespace Fina.Core.Models;

public class Categoria
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string UsuarioId { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    public bool Ativo { get; set; }
    public DateTime CriadaEm { get; set; }
    public TipoTransacao TipoCategoria { get; set; }
    public virtual ICollection<SubCategoria>? SubCategorias { get; set; }
}
