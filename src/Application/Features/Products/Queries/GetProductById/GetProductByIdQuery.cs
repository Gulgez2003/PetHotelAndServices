namespace FinalProjectApp.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductGetDTO>
    {
        public int Id { get; set; }
    }
}
