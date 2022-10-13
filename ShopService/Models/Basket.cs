namespace ShopService.Models
{
    public class Basket
    {
        public int Id { get; set; } 
        public string? Status { get; set; }
        public virtual ICollection<SummUpProduct>? SummUpProducts { get; set; }
        public virtual ICollection<(Product? product, int quantity)>? ProductsQuantities { get; set; }
        public int ClientId { get; set; }
        public virtual Client? Client { get; set; }
    }
}
