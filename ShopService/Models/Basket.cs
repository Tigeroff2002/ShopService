namespace ShopService.Models
{
    public class Basket
    {
        public virtual User? Client { get; set; }
        public virtual ICollection<SummUpProduct>? SummUpProducts { get; set; }
        public int BasketStatusId { get; set; } = 0;
    }
}
