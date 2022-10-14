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
        public virtual User? Client { get; set; }
        [ForeignKey("DeviceId")]
        public virtual ICollection<SummUpProduct>? SummUpProducts { get; set; }
        [Column("ResultCost")]
        public float ResultCost { get; set; }
        public virtual Basket? Basket { get; set; }
        public int BasketStatusIdWhenOrderFormed { get; set; }
        public virtual Shipping? Shipping { get; set; }
        [Column("TradingDate")]
        public DateTime OrderDate { get; set; }
    }
}
