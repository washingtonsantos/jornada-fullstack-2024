namespace Fina.Core.Requests.Transacoes;

public class ObterTransacaoPorPeriodoRequest : PagedRequest
{
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
}
