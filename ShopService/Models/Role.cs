﻿namespace ShopService.Models
{
    public enum RoleType: byte
    {
        AuthUser = 1,
        Manager = 2,
        Admin = 3
    }
    public class Role
    {
        public int Id { get; set; }
        public RoleType RoleType { get; set; }
        public string? RoleCaption { get; set; }
        public ICollection<User>? Users { get; set; }
        public ICollection<Option>? Options { get; set; }

        public Role(RoleType roleType)
        {
            RoleType = roleType;
            Options = new List<Option>();
        }
    }
}
