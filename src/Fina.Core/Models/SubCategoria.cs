namespace Fina.Core.Models;

public class SubCategoria
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public Guid CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;
    public Guid UsuarioId { get; set; }
}
