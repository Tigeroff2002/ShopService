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
        [Column("Id")]
        public int Id { get; set; }
        public virtual DeviceType? DeviceType { get; set; }
        [Column("Name")]
        [MaxLength(40)]
        public string? Name { get; set; }
        public virtual Producer? Producer { get; set; }
        [Column("Cost")]
        public float Cost { get; set; }
        [Column("Size")]
        public float? Size { get; set; }
        [Column("ScreenResolution")]
        [MaxLength(40)]
        public string? ScreenResolution { get; set; }
        [Column("Cammp")]
        public float? CamMp { get; set; }
        [Column("AccumCapacity")]
        public float? AccumCapacity { get; set; }
        [Column("RAM")]
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
