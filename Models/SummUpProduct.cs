using System.ComponentModel.DataAnnotations;

namespace Models;

public class SummUpProduct 
    : IEquatable<SummUpProduct>
{
    [Key]
    public int Id { get; set; }
    public int ProductId { get; set; }
    public virtual Product? Product { get; set; }
    public int Quantity { get; set; }
    public float? TotalPrice { get; set; }
    public int BasketId { get; set; }
    public virtual Basket? Basket { get; set; }

    public SummUpProduct()
    {

    }
    public SummUpProduct(int id, Product? product, int quantity)
    {
        if (id <= 0)
            throw new ArgumentException(nameof(id));

        Id = id;

        Product = product ?? throw new ArgumentNullException(nameof(product));

        if (quantity < 0)
            throw new ArgumentException(nameof(quantity));

        Quantity = quantity;

        TotalPrice = CalculateSummUpPrice();
    }
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        if (obj is SummUpProduct summUpProduct)
            return Equals(summUpProduct);
        else
            return false;
    }

    public bool Equals(SummUpProduct? summUpProduct)
    {
        if (summUpProduct == null)
            return false;
        if (GetHashCode() != summUpProduct.GetHashCode())
            return false;
        return true;
    }

    public override int GetHashCode()
    {
        return (Product!.Id, Quantity).GetHashCode();
    }

    public float CalculateSummUpPrice()
    {
        return Product!.Cost * Quantity;
    }
}
