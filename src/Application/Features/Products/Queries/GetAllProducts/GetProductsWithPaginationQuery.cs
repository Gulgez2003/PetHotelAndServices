using FinalProjectApp.Application.Common.Models;

namespace FinalProjectApp.Application.Features.Products.Queries.GetAllProducts
{
    public class GetProductsWithPaginationQuery : IRequest<List<ProductGetDTO>>
    {
        public int CurrentPage { get; set; }
        public int Take { get; set; }
    }
}
