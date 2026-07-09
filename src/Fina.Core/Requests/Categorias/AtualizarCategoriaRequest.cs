using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Categorias;

public class AtualizarCategoriaRequest : Request
{
    public Guid Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;
}
