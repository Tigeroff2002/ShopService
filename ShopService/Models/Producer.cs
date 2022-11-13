using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopService.Models
{
    public partial class Producer
    {
        public Producer()
        {
            Products = new HashSet<Product>();
        }
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public float Popularity { get; set; }
        public string? WebSite { get; set; }
        public virtual ICollection<Product>? Products { get; set; } 
    }
}
