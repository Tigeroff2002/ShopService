using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopService.Models
{
    public class Warehouse
    {
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
        public virtual ICollection<SummUpProduct>? ProductQuantities { get; set; }
        
        public Warehouse()
        {
            Shippings = new List<Shipping>();
            ProductQuantities = new List<SummUpProduct>();
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
