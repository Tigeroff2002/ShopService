using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopService.Models
{
    [Table("Producer")]
    public partial class Producer
    {
        public Producer()
        {
            Devices = new HashSet<Device>();
        }
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        [MaxLength(40)]
        public string? Name { get; set; }
        [Column("Country")]
        [MaxLength(40)]
        public string? Country { get; set; }
        [Column("Popularity")]
        public float Popularity { get; set; }
        [Column("WebSite")]
        public string? WebSite { get; set; }
        public virtual ICollection<Device>? Devices { get; set; } 
    }
}
