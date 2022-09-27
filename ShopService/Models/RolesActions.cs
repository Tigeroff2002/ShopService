namespace ShopService.Models
{
    public class RolesActions
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<RoleModel>? Roles { get; set; } 
    }
}
