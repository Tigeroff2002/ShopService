namespace ShopService.Models
{
    public class SummUpProduct
    {
        public int Id { get; set; }
        public virtual Product? Product { get; set; }
        public int Quantity { get; set;}
        public float TotalPrice { get; set; }
    }
}
