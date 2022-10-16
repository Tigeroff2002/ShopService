﻿namespace ShopService.Models
{
    public class SummUpProduct : IEquatable<SummUpProduct>
    {
        public int Id { get; set; }
        public virtual Product? Product { get; set; }
        public int Quantity { get; set;}
        public float TotalPrice { get; set; }
        
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
    }
}
