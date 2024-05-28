namespace Fina.Core.Requests
{
    public abstract class PagedRequest
    {
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
}
