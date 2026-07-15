namespace Fina.Core.Requests.Contas;

public class CriarContaRequest : Request
{
    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;
}
