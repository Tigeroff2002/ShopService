using System.ComponentModel.DataAnnotations;

namespace ShopService.Models
{
    public class Option
    {
        [Key]
        public int Id { get; set; }
        public string? Caption { get; set; }
        public virtual Role? Role { get; set; }

        public Option()
        {
            Role = new Role((RoleType) 1);
        }
    }
}
