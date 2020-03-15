namespace Backend.Domain.ViewModels.CartViewModel
{
    public class CartViewModel
    {
        public string CustomerId { get; set; }
        public CartItemViewModel Item { get; set; }
    }
}
