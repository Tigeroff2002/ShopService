namespace ShopService.Models
{
    public class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
            Notifications = new HashSet<Notification>();
        }
        public int Id { get; set; }
        public string? NickName { get; set; }
        public string? Password { get; set; }
        public string? ContactNumber { get; set; }
        public string? EmailAdress { get; set; }
        public float TotalPurchase { get; set; }
        public int? Discount { get; set; }
        public virtual Basket? Basket { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual Role? Role { get; set; }
    }
}
