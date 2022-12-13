using System.ComponentModel.DataAnnotations;

namespace Models;

public enum RoleType : byte
{
    NonAuthUser = 0,
    AuthUser = 1,
    Manager = 2,
    Admin = 3
}
public class Role
{
    [Key]
    public int Id { get; set; }
    public RoleType RoleType { get; set; }
    public string? RoleCaption { get; set; }
    public virtual ICollection<User>? Users { get; set; }
    public virtual ICollection<Option>? Options { get; set; }

    public Role(RoleType roleType)
    {
        RoleType = roleType;

        switch (roleType)
        {
            case RoleType.NonAuthUser:
                RoleCaption = "New user";
                break;
            case RoleType.Admin:
                RoleCaption = "Administrator";
                break;

            case RoleType.Manager:
                RoleCaption = "Manager";
                break;

            case RoleType.AuthUser:
                RoleCaption = "User";
                break;

        }

        Options = new List<Option>();
    }
}
