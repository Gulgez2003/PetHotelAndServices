namespace FinalProjectApp.Application.Features.Products.Queries.GetSearchResults
{
    public class GetSearchResultsQuery : IRequest<List<ProductGetDTO>>
    {
        public string Name { get; set; }
        public int CurrentPage { get; set; }
        public int Take { get; set; }
    }
}
