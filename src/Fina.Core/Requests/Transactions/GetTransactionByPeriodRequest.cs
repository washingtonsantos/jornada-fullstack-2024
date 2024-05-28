namespace Fina.Core.Requests.Transactions
{
    public class GetTransactionByPeriodRequest : PagedRequest
    {
        public DateTime? StartdDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
