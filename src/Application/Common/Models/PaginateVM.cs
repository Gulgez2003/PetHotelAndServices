namespace FinalProjectApp.Application.Common.Models
{
    public class PaginateVM<T>
    {
        public List<T> Models { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public bool Next { get; set; }
        public bool Previous { get; set; }
    }
}
