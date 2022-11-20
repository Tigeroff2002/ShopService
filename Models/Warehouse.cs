using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Shipping>? Shippings { get; set; }
        public virtual ICollection<SummUpProduct>? ProductQuantities { get; set; }

        public Warehouse()
        {
            Shippings = new List<Shipping>();
            ProductQuantities = new List<SummUpProduct>();
        }

        public bool CheckExistenseGoodOnWarehouse(int _goodId)
        {
            ArgumentNullException.ThrowIfNull(ProductQuantities);

            var quantity = ProductQuantities!.First(x => x.Id == _goodId).Quantity;
            if (quantity > 0)
                return true;
            return false;
        }
    }
}
