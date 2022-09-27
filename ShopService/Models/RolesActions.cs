namespace ShopService.Models
{
    public class RolesActions
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<int>? RolesId { get; set; } = new List<int> { 0 };
    }
}
