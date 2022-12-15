using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Drawing;
using System;

namespace Models;

public class Product 
    : IEquatable<Product>
{
    public Product()
    {

    }

    public Product(int id, DeviceType deviceType, Producer producer)
    {
        if (id <= 0)
            throw new ArgumentException(nameof(id));

        Id = id;

        DeviceType = deviceType ?? throw new ArgumentNullException(nameof(deviceType));
        Producer = producer ?? throw new ArgumentNullException(nameof(producer));

        Baskets = new HashSet<Basket>();
        Orders = new HashSet<Order>();
        SummUpProducts = new HashSet<SummUpProduct>();
    }

    [Key]
    public int Id { get; set; }
    public virtual DeviceType? DeviceType { get; set; }
    public string? Name { get; set; }
    public virtual Producer? Producer { get; set; }
    public float Cost { get; set; }
    public float? Size { get; set; }
    public string? ScreenResolution { get; set; }
    public float? CamMp { get; set; }
    public float? AccumCapacity { get; set; }
    public float? RAM { get; set; }
    public float? Rating { get; set; } = 5f;
    public string? ImageLink { get; set; } = "image/product1.jpg";
    public virtual ICollection<Order>? Orders { get; set; }
    public virtual ICollection<Basket>? Baskets { get; set; }
    public virtual ICollection<SummUpProduct>? SummUpProducts { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (obj is Product product)
        {
            return Equals(product);
        }
        else
        {
            return false;
        }
    }
    public override int GetHashCode()
    {
        return Id;
    }

    public bool Equals(Product? product)
    {
        if (product == null)
        {
            return false;
        }
        
        return product.Id == Id;
    }
}
