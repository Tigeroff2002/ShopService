namespace ShopService.Models
{
    public enum RoleType: byte
    {
        AuthUser = 1,
        Operator = 2,
        Manager = 3,
        Admin = 4
    }
    public class Role
    {
        public int Id { get; set; }
        public RoleType RoleType { get; set; }
        public string? RoleCaption { get; set; }
        public ICollection<User>? Users { get; set; }
        public ICollection<Option>? Options { get; set; }
    }
}
