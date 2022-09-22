using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopService.Models
{
    public partial class Device
    {
        public Device()
        {
            Tradings = new HashSet<Trading>();
        }
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [ForeignKey("DeviceTypeId")]
        public int DeviceTypeId { get; set; }
        public virtual DeviceType? DeviceType { get; set; }
        [Column("Name")]
        [MaxLength(40)]
        public string? Name { get; set; }
        [ForeignKey("ProducerId")]
        public int ProducerId { get; set; }
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
        public virtual ICollection<Trading>? Tradings { get; set; }
    }
}
