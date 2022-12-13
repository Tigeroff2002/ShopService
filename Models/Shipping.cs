using System.ComponentModel.DataAnnotations;

namespace Models;

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

    public Shipping()
    {

    }

    public Shipping(int shipType)
    {
        if (shipType < 1 || shipType > 3)
        {
            throw new ArgumentException(nameof(shipType));
        }

        ShipType = (ShippingType)shipType;

        ShippingPrice = CalculateShippingCost();
    }

    public float CalculateShippingCost()
    {
        return (int)ShipType * COST_BY_TYPE;
    }

    public const int COST_BY_TYPE  = 1000;
}
