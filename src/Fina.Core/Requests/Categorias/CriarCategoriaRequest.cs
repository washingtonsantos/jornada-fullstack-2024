namespace Fina.Core.Requests.Categorias;

public class CriarCategoriaRequest : Request
{
    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;
}
