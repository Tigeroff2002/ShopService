using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopService.Models
{
    public class Warehouse
    {
        public Warehouse()
        {
            Tradings = new HashSet<Trading>();
        }
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        [MaxLength(40)]
        public string? Name { get; set; }
        [Column("Adress")]
        [MaxLength(40)]
        public string? Address { get; set; }
        [Column("WorkingTime")]
        [MaxLength(40)]
        public string? WorkingTime { get; set; }
        public virtual ICollection<Trading>? Tradings { get; set; }
    }
}
