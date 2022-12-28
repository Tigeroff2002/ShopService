using Azure.Core;
using System.ComponentModel.DataAnnotations;

namespace Models;

public class Order
{
    [Key]
    public int Id { get; set; }
    public virtual User? Client { get; set; }
    public virtual IList<SummUpProduct> SummUpProducts { get; set; }
    public float? ResultCost { get; set; }
    public virtual Shipping? Shipping { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShippedDate { get; set; }
    public bool isReadyForConfirmation { get; set; } = false;
    public bool isReadyForPayment { get; set; } = false;
    public bool isPayd { get; set; } = false;

    public bool isGot { get; set; } = false;

    public bool isDeleted { get; set; } = false;

    public string? OrderDescription { get; set; }

    public Order()
    {

    }

    public Order(User? client)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client));

        isReadyForConfirmation = default;
        isReadyForPayment = default;
        isPayd = default;

        SummUpProducts = new List<SummUpProduct>();

        OrderDescription = CreateDescription();
    }

    public bool Equals1(Order? order)
    {
        if (order == null)
        {
            return false;
        }    
        return GetHashCode() == order.GetHashCode() &&
            Client!.Basket!.Equals(order!.Client!.Basket);
    }

    public bool Equals(Order? order)
    {
        if (order == null)
        {
            return false;
        }

        return order.Id == Id;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Order);
    }

    public override int GetHashCode()
    {
        return (Client!.Id,
            Client!.Basket!.BasketStatusId).GetHashCode();
    }

    public void CreateOrderWithCurrentBasket()
    {
        var currentDate = new DateTime();

        OrderDate = currentDate;
        ResultCost = Client!.Basket!.TotalCost * (1 - Client!.Discount);
        Client!.Basket!.ResetBasket();

        isReadyForConfirmation = true;
    }

    public void ConfirmCreatedOrder()
    {
        // Actions for Confrimation Order
        isReadyForPayment = true;
    }

    public void PayConfirmedOrder()
    {
        // Actions for Paying Order
        isPayd = true;
    }

    public float? CalculateResultCost()
    {
        ResultCost = 0;

        foreach(var group in SummUpProducts)
        {
            ResultCost += group.TotalPrice;
        }

        return ResultCost;
    }
    public string CreateDescription()
    {
        var s = "";

        foreach (var group in SummUpProducts)
        {
            s += $" Тип устройства :" +
                $" {group.Product!.DeviceType!.Name}," +
                $" Производитель: {group.Product!.Producer!.Name}," +
                $" Название: {group.Product.Name}," +
                $" Количество {group.Quantity} {Environment.NewLine}";
        }

        return s;
    }
}
