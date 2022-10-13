using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopService.Models
{
    public enum DeviceTypeEnum : byte
    {
        Smartphone = 1,
        TabletPad = 2,
        Notebook = 3,
        Laptop = 4,
        SystemBlock = 5,
        MonoBlock = 6,
        Monitor = 7,
        Mouse = 8,
        KeyBoard = 9,
        HeadPhones = 10,
        Microphone = 11,
        Speakers = 12,
        MotherBoard = 13,
        CPU = 14,
        VideoCard = 15,
        DriveDisk = 16,
        RAM_Card = 17,
        PowerSupply = 18,
        Printer = 19,
        WebCam = 20,
        Scanner = 21
    }
    public partial class DeviceType
    {
        public DeviceType()
        {
            Devices = new HashSet<Product>();
        }
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        public DeviceTypeEnum DeviceTypeEntity { get; set; } 
        [Column("Name")]
        [MaxLength(40)]
        public string? DeviceTypeName { get; set; }
        public virtual ICollection<Product>? Devices { get; set; }
    }
}
