using FinalProjectApp.Application.Common.Models;

namespace FinalProjectApp.Presentation.ViewModels
{
    public class ProductCartItemViewModel
    {
        public List<CartItemViewModel> CartItems { get; set; }
        public PaginateVM<ProductGetDTO> PaginateVM { get; set; }
    }
}
