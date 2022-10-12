using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopService.Models
{
    public class Client : User
    {
        public Client()
        {
            Orders = new HashSet<Order>();
        }
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        [MaxLength(40)]
        public string? Name { get; set; }
        [Column("Surname")]
        [MaxLength(40)]
        public string? Surname { get; set; }
        [Column("TelephoneNumber")]
        [MaxLength(40)]
        public string? TNumber { get; set; }
        [Column("RegistrationLK")]
        public bool IsRegistredLk { get; set; }
        [Column("TotalPurchase")]
        public float TotalSum { get; set; }
        [Column("IndividDiscount")]
        public int? Discount { get; set; }
        public int BasketId { get; set; }
        public virtual Basket? Basket { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
