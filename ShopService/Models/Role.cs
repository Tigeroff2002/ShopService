namespace ShopService.Models
{
    public enum RoleType: byte
    {
        NewUser = 1,
        AuthUser = 2,
        Operator = 3,
        Manager = 4,
        Admin = 5
    }
    public class Role
    {
        public int Id { get; set; }
        public RoleType RoleType { get; set; }
        public string? RoleCaption { get; set; }
        public ICollection<User>? Users { get; set; }
        public ICollection<Option>? Actions { get; set; }
    }
}
