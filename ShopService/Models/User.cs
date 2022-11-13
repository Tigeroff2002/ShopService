using System.ComponentModel.DataAnnotations;

namespace ShopService.Models
{
    public class User
    {
        public User()
        {
            Basket = new Basket();
            TotalPurchase = 0;
            Discount = 0.00f;
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
            Notifications = new HashSet<Notification>();
            Role = new Role((RoleType) 1);
        }
        [Key]
        public int Id { get; set; }
        public string? NickName { get; set; }
        public string? Password { get; set; }
        public string? ContactNumber { get; set; }
        public string? EmailAdress { get; set; }
        public float TotalPurchase { get; set; }
        public float Discount { get; set; }
        public virtual Basket? Basket { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual Role? Role { get; set; }
    }
}
