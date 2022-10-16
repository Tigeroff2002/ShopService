namespace ShopService.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? NickName { get; set; }
        public string? EmailAdress { get; set; }
        public int? RoleId { get; set; }
        public virtual Role? Role { get; set; }
    }
}
