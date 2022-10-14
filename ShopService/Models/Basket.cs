namespace ShopService.Models
{
    public class Basket
    {
        public int ClientId { get; set; } 
        public virtual ICollection<SummUpProduct>? SummUpProducts { get; set; }
        public int BasketStatusId { get; set; } = 0;
    }
}
