using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopService.Models
{
    public class Trading
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [ForeignKey("ClientId")]
        public int ClientId { get; set; }
        public virtual Client? Client { get; set; }
        [ForeignKey("DeviceId")]
        public int DeviceId { get; set; }
        public virtual Device? Device { get; set; } 
        [ForeignKey("WarehouseId")]
        public int WareHouseId { get; set; }
        public virtual Warehouse? WareHouse { get; set; }
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
