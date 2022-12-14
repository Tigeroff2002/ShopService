using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Models;

public class Basket
{
    [Key]
    public int Id { get; set; }
    public int BasketStatusId { get; set; }
    public int ClientId { get; set; }
    public virtual User? Client { get; set; }
    public virtual List<SummUpProduct> SummUpProducts { get; set; }
    public float? TotalCost { get; set; }

    public Basket()
    {

    }

    public Basket(User? client)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client));

        ClientId = Client.Id;

        TotalCost = 0;
        BasketStatusId = 1;

        SummUpProducts = new List<SummUpProduct>();
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        if (obj is Basket basket)
        {
            return Equals(basket);
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return (Client!.Id, BasketStatusId).GetHashCode();
    }

    public bool EqualsA(Basket? basket)
    {
        if (basket == null)
        {
            return false;
        }
        var equals = Enumerable.SequenceEqual(
            SummUpProducts!.OrderBy(x => x.Product!.Id),
            basket.SummUpProducts!.OrderBy(x => x.Product!.Id));

        if (GetHashCode() != basket.GetHashCode() || !equals)
        {
            return false;
        }
        return true;
    }

    public bool Equals(Basket? basket)
    {
        if (basket == null)
        {
            return false;
        }

        return basket.BasketStatusId == BasketStatusId &&
            basket.Client == Client;
    }

    public float? CalculateTotalPrice()
    {
        TotalCost = 0;

        foreach (var item in SummUpProducts!)
        {
            TotalCost += item.TotalPrice;
        }

        return TotalCost;
    }

    public void ChangeBasketStatusId(Basket? basket)
    {
        if (!Equals(this, basket))
        {
            BasketStatusId += 1;
        }
    }
    public void ResetBasket()
    {
        SummUpProducts!.Clear();
        TotalCost = 0;
        BasketStatusId = 1;
    }

    public void AddProductInBasket(Product? product)
    {
        ArgumentNullException.ThrowIfNull(product);

        var id = product.Id;
        var count = FindQuantityProductsInBasket(id);

        if (count == 0)
        {
            var summUpProduct = new SummUpProduct(RandomNumberGenerator.GetInt32(100), product, 1);
            SummUpProducts!.Add(summUpProduct);
        }
        else
        {
            SummUpProducts!.First(x => x.Product!.Id == id).Quantity++;
        }    

        TotalCost += product!.Cost;
    }

    public void RemoveProductInBasket(Product? product)
    {
        ArgumentNullException.ThrowIfNull(product);

        var id = product.Id;
        var summUpProductId = 1;

        var count = FindQuantityProductsInBasket(id);

        if (count == 1)
        {
            var summUpProduct = new SummUpProduct(summUpProductId, product, 1);
            SummUpProducts!.Remove(summUpProduct);
        }
        else if (count > 1)
        {
            SummUpProducts!.First(x => x.Product!.Id == id).Quantity--;
        }

        TotalCost -= product!.Cost;
    }

    private int FindQuantityProductsInBasket(int id)
    {
        if (SummUpProducts!.Any(x => x.Product!.Id == id))
        {
            return SummUpProducts!.FirstOrDefault(x => x.Product!.Id == id)!.Quantity;
        }

        return 0;
    }
}
