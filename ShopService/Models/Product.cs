using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopService.Models
{
    public class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
        }
        [Key]
        public int Id { get; set; }
        public virtual DeviceType? DeviceType { get; set; }
        public string? Name { get; set; }
        public virtual Producer? Producer { get; set; }
        public float Cost { get; set; }
        public float? Size { get; set; }
        public string? ScreenResolution { get; set; }
        public float? CamMp { get; set; }
        public float? AccumCapacity { get; set; }
        public float? RAM { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj is Product product)
                return Equals(product);
            else
                return false;
        }
        public override int GetHashCode()
        {
            return this.Id;
        }
        public bool Equals(Product? product)
        {
            if (product == null)
                return false;
            if (Id != product.Id)
                return false;
            return true;
        }
    }
}
