namespace ShopService.Models
{
    public class Basket
    {
        public int Id { get; set; } 
        public string? Status { get; set; }
        public virtual Client? Client { get; set; }
    }
}
