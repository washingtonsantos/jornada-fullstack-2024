namespace Fina.Core.Requests.Bancos;

public class AtualizarBancoRequest : Request
{
    public Guid Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public decimal Valor { get; set; }
    public string Descricao { get; set; } = string.Empty;

}
