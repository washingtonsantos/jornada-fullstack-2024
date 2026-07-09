namespace Fina.Core.Requests.Bancos;

public class CriarBancoRequest : Request
{
    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;
}
