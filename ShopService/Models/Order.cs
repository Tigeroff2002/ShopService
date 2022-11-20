using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopService.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public virtual User? Client { get; set; }
        public float? ResultCost { get; set; }
        public virtual Shipping? Shipping { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public bool isReadyForConfirmation { get; set; } = false;
        public bool isReadyForPayment { get; set; } = false;
        public bool isReadyForShipping { get; set; } = false;

        public Order(ShippingType type)
        {
            Shipping = new Shipping(type);
        }
        public bool Equals(Order? order)
        {
            if (order == null)
                return false;
            return GetHashCode() == order.GetHashCode() && Client!.Basket!.Equals(order!.Client!.Basket);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Order);
        }
        public override int GetHashCode()
        {
            return (Client!.Id, Client!.Basket!.BasketStatusId).GetHashCode();
        }

        public Order()
        {
            isReadyForConfirmation = default;
            isReadyForPayment = default;
            isReadyForShipping = default;
            CreateOrderWithCurrentBasket();
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
            isReadyForShipping = true;
        }
    }
}
