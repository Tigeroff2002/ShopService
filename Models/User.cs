using System.ComponentModel.DataAnnotations;

namespace Models;

public class User
{
    public User()
    {

    }

    public User(int id, int roleType)
    {
        if (roleType < 1 || roleType > 3)
        {
            throw new ArgumentException(nameof(roleType));
        }

        Id = id;
        Role = new Role((RoleType)roleType);
        TotalPurchase = 0;
        Discount = 0.00f;

        Orders = new HashSet<Order>();
        Reviews = new HashSet<Review>();
        Notifications = new HashSet<Notification>();
    }

    public int Id { get; set; }
    public string? NickName { get; set; } = default!;
    public string? Password { get; set; } = default!;
    public string? ContactNumber { get; set; } = default!;
    public string? EmailAdress { get; set; } = default!;
    public float TotalPurchase { get; set; } = default!;
    public float Discount { get; set; } = default!;
    public virtual Basket? Basket { get; set; } = default!;
    public virtual ICollection<Order> Orders { get; set; } = default!;
    public virtual ICollection<Review> Reviews { get; set; } = default!;
    public virtual ICollection<Notification> Notifications { get; set; } = default!;
    public virtual Role? Role { get; set; } = default!;

    public bool Equals(User? user)
    {
        if (user == null)
        {
            return false;
        }

        return user!.EmailAdress!.Equals(EmailAdress);
    }

    public override int GetHashCode()
    {
        return (EmailAdress!).GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (obj is User user)
        {
            return Equals(user);
        }
        else
        {
            return false;
        }
    }
}
