namespace Shopping.Web.Models.Ordering
{
    public class PaginatedResult<TEntity>(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
        where TEntity : class // TEntity is a class constraint that ensures TEntity is a reference type
    {
        public int PageIndex { get; } = pageIndex;
        public int PageSize { get; } = pageSize;
        public long Count { get; } = count;
        public IEnumerable<TEntity> Data { get; } = data;
    }
}
