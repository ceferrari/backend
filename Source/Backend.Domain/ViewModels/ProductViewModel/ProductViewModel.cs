namespace Backend.Domain.ViewModels.ProductViewModel
{
    public class ProductViewModel
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageUrl { get; set; }
        public ProductPriceViewModel Price { get; set; }
    }
}
