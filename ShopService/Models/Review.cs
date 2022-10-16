namespace ShopService.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public virtual Client? Client { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public float? Rate { get; set; }
    }
}
