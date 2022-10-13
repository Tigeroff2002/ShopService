using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopService.Models
{
    public class Client : User
    {
        public Client()
        {
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
        }
        [Key]
        [Column("Id")]
        public int ClientId { get; set; }
        [Column("Name")]
        [MaxLength(40)]
        public string? ClientName { get; set; }
        [Column("TelephoneNumber")]
        [MaxLength(40)]
        public string? TNumber { get; set; }
        //public int? CardId { get; set; }
        [Column("TotalPurchase")]
        public float TotalPurchase { get; set; }
        [Column("IndividDiscount")]
        public int? Discount { get; set; }
        public int BasketId { get; set; }
        public virtual Basket? Basket { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
