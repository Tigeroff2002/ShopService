namespace ShopService.Models
{
    public class Shipping
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public float ShippingPrice { get; set; }
        public bool? IsThroughRegions { get; set; } = false;
        public virtual Warehouse? Warehouse { get; set; }
    }
}
