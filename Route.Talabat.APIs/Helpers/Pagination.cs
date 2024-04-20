namespace Route.Talabat.APIs.Helpers
{
    public class Pagination<T>
    {
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public IReadOnlyList<T> Data { get; set; } = null!;

        public Pagination(int pageSize,int pageIndex,int count, IReadOnlyList<T> data) 
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Data = data;
            Count = count;
        }
    }
}
