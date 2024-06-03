using FinalProjectApp.Application.Common.Models;
using FinalProjectApp.Application.Features.Products.Queries.GetSearchResults;
using SendGrid.Helpers.Errors.Model;

namespace Presentation.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ISender _sender;
        public const string COOKIES_BASKET = "cart";
        public ProductsController(ISender sender)
        {
            _sender = sender;
        }

        public async Task<IActionResult> Index(int currentPage = 1, int take = 9)
        {
            var products = await _sender.Send(new GetProductsWithPaginationQuery()
            {
                CurrentPage = currentPage,
                Take = take
            });

            int pageCount = await GetPageCount(take);
            PaginateVM<ProductGetDTO> paginateVM = new PaginateVM<ProductGetDTO>
            {
                Models = products,
                CurrentPage = currentPage,
                PageCount = pageCount,
                Previous = currentPage > 1,
                Next = currentPage < pageCount
            };

            List<CartViewModel> cartViewModels = GetCartViewModels();
            List<CartItemViewModel> cartItemViewModels = new List<CartItemViewModel>();
            foreach (var item in cartViewModels)
            {
                var product = await _sender.Send(new GetProductByIdQuery { Id = item.ProductId });
                CartItemViewModel cartItemViewModel = new CartItemViewModel()
                {
                    Id = item.ProductId,
                    ProductCount = item.Count,
                    Title = product.Title,
                    Price = product.Price,
                    Colours = product.Colours,
                    ProductImages = product.ProductImages,
                    InStock = product.InStock
                };
                cartItemViewModels.Add(cartItemViewModel);
            }

            ProductCartItemViewModel productCartItemViewModel = new ProductCartItemViewModel()
            {
                PaginateVM = paginateVM,
                CartItems = cartItemViewModels
            };

            return View(productCartItemViewModel);
        }

        private List<CartViewModel> GetCartViewModels()
        {
            List<CartViewModel> cartViewModels;

            if (Request.Cookies[COOKIES_BASKET] != null)
            {
                cartViewModels = JsonConvert.DeserializeObject<List<CartViewModel>>(Request.Cookies[COOKIES_BASKET]);
            }
            else
            {
                cartViewModels = new List<CartViewModel>();
            }

            return cartViewModels;
        }

        public async Task<int> GetPageCount(int take)
        {
            var products = await _sender.Send(new GetProductsQuery());
            int productCount = products.Count();
            return (int)Math.Ceiling((decimal)productCount / take);
        }

        public async Task<IActionResult> Search(string name, int currentPage = 1, int take = 9)
        {
            var results = await _sender.Send(new GetSearchResultsQuery()
            { 
                Name = name,
                CurrentPage = currentPage,
                Take = take
            });

            int pageCount = await GetPageCount(take);

            PaginateVM<ProductGetDTO> paginateVM = new PaginateVM<ProductGetDTO>
            {
                Models = results,
                CurrentPage = currentPage,
                PageCount = pageCount,
                Previous = currentPage > 1,
                Next = currentPage < pageCount
            };

            List<CartViewModel> cartViewModels = GetCartViewModels();
            List<CartItemViewModel> cartItemViewModels = new List<CartItemViewModel>();
            foreach (var item in cartViewModels)
            {
                var product = await _sender.Send(new GetProductByIdQuery { Id = item.ProductId });
                CartItemViewModel cartItemViewModel = new CartItemViewModel()
                {
                    Id = item.ProductId,
                    ProductCount = item.Count,
                    Title = product.Title,
                    Price = product.Price,
                    Colours = product.Colours,
                    ProductImages = product.ProductImages,
                    InStock = product.InStock
                };
                cartItemViewModels.Add(cartItemViewModel);
            }

            ProductCartItemViewModel productCartItemViewModel = new ProductCartItemViewModel()
            {
                PaginateVM = paginateVM,
                CartItems = cartItemViewModels
            };

            return View(productCartItemViewModel);
        }
    }
}