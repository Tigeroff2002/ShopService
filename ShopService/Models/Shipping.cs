using System.ComponentModel.DataAnnotations;

namespace ShopService.Models
{
    public enum ShippingType : byte
    {
        Casual = 1,
        Rapid = 2,
        Prioritet = 3
    }
    public class Shipping
    {
        [Key]
        public int Id { get; set; }
        public ShippingType ShipType { get; set; }
        public float ShippingPrice { get; set; }
        public virtual Warehouse? Warehouse { get; set; }
        public virtual Shop? Shop { get; set; }

        public Shipping(ShippingType shipType)
        {
            ShipType = shipType;
        }

        public void CalculateShippingCost()
        {
            ShippingPrice = (int) ShipType * COST_BY_TYPE;
        }

        private const int COST_BY_TYPE = 1000;
    }
}
