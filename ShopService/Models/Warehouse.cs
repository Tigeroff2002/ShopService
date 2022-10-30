using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopService.Models
{
    public class Warehouse
    {
        public class SummUpGood : SummUpProduct
        {
            public int Id;
            public SummUpGood()
            {
                Id = Product!.Id;
                Quantity = 0;
            }
        }
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        [MaxLength(40)]
        public string? Name { get; set; }
        [Column("Adress")]
        [MaxLength(40)]
        public string? Address { get; set; }
        [Column("WorkingTime")]
        [MaxLength(40)]
        public string? WorkingTime { get; set; }
        public virtual ICollection<Shipping>? Shippings { get; set; }
        public virtual ICollection<SummUpGood>? ProductQuantities { get; set; }
        
        public Warehouse()
        {
            Shippings = new List<Shipping>();
            ProductQuantities = new List<SummUpGood>();
        }

        public bool CheckExistenseGoodOnWarehouse(int _goodId)
        {
            var quantity = ProductQuantities!.First(x => x.Id == _goodId).Quantity;
            if (quantity > 0)
                return true;
            return false;
        }
    }
}
