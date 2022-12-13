using System.ComponentModel.DataAnnotations;

namespace Models;

public class Option
{
    [Key]
    public int Id { get; set; }
    public string? Caption { get; set; }
    public virtual Role? Role { get; set; }

    public Option()
    {
        Role = new Role((RoleType)1);
    }

    public Option(RoleType type)
    {
        Role = new Role(type);
    }
}
