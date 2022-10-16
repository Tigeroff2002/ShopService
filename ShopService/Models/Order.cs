using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopService.Models
{
    public class Order
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [ForeignKey("ClientId")]
        public int ClientId { get; set; }
        public virtual Client? Client { get; set; }
        [ForeignKey("DeviceId")]
        public virtual ICollection<SummUpProduct>? SummUpProduct { get; set; }
        public virtual ICollection<(Product? product, int quantity)>? ProductsQuantities { get; set; }
        /*
        public int? BasketId { get; set; }
        public virtual Basket? Basket { get; set; }
        */
        public int ShippingId { get; set; }
        public virtual Shipping? Shipping { get; set; }
        [Column("TradingDate")]
        public DateTime Date { get; set; }
        [Column("ResultCost")]
        public float ResultCost { get; set; }
        [Column("ClientMark")]
        public float ClientMark { get; set; }
        [Column("isDeviceReturned")]
        public bool IsDeviceReturned { get; set; }
    }
}
