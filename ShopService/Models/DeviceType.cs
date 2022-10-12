using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopService.Models
{
    public partial class DeviceType
    {
        public DeviceType()
        {
            Devices = new HashSet<Product>();
        }
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        [MaxLength(40)]
        public string? Name { get; set; }
        [Column("Description")]
        public string? Description { get; set; }
        public virtual ICollection<Product>? Devices { get; set; }
    }
}
