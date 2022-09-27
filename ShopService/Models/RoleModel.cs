namespace ShopService.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public ICollection<ProfileModel>? Profiles { get; set; }
        public ICollection<RolesActions>? Actions { get; set; }
    }
}
