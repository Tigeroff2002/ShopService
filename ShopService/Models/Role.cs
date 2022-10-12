namespace ShopService.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string? Caption { get; set; } 
        public ICollection<User>? Users { get; set; }
        public ICollection<Option>? Actions { get; set; }
    }
}
