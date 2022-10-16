namespace ShopService.Models
{
    public class Option
    {
        public int Id { get; set; }
        public string? Caption { get; set; }
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }
    }
}
