namespace Fina.Core.Requests.Transacoes;

public class ObterTransacaoPorIdRequest : Request
{
    public Guid Id { get; set; }
}
