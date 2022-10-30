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
        public int Id { get; set; }
        public ShippingType ShipType { get; set; }
        public float ShippingPrice { get; set; }
        public bool IsThroughRegions { get; set; } = false;
        public int daysShipping { get; set; }
        private static readonly int COST_OF_DAY = 50;
        private static readonly int KF_SHIP_TYPE = - 1;
        public virtual Warehouse? Warehouse { get; set; }

        public Shipping(ShippingType shipType, bool isThroughRegions)
        {
            ShipType = shipType;
            IsThroughRegions = isThroughRegions;
        }

        public void CalculateShippingDays()
        {
            switch(ShipType)
            {
                case ShippingType.Casual: 
                    daysShipping = IsThroughRegions? 6 : 3;
                    break;
                case ShippingType.Rapid:
                    daysShipping = IsThroughRegions ? 4 : 2;
                    break;
                case ShippingType.Prioritet:
                    daysShipping = IsThroughRegions ? 2 : 1;
                    break;
            }
        }
        public void CalculateShippingPrice()
        {
            CalculateShippingDays();
            ShippingPrice = COST_OF_DAY * daysShipping * ((byte)ShipType) * ((byte)ShipType + KF_SHIP_TYPE);
        }
    }
}
