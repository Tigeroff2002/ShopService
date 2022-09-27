namespace ShopService.Models
{
    public class ProfileModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? EmailAdress { get; set; }
        public int? RoleId { get; set; }
        public virtual RoleModel? Role { get; set; }
    }
}
