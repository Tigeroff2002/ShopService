using Microsoft.Build.Framework;
using ShopService.Controllers;

namespace ShopService.Models
{
    public class Basket
    {
        public delegate void ChangeBasketState(object obj);
        public virtual User? Client { get; set; }
        public virtual ICollection<SummUpProduct>? SummUpProducts { get; set; }
        public int BasketStatusId { get; set; } = 0;
        public float TotalCost { get; private set;  }
        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj is Basket basket)
                return Equals(basket);
            else
                return false;
        }
        public override int GetHashCode()
        {
            return (Client!.Id, BasketStatusId).GetHashCode();
        }
        public bool Equals(Basket? basket)
        {
            if (basket == null)
                return false;
            var equals = Enumerable.SequenceEqual(SummUpProducts!.OrderBy(x => x.Id),
                basket.SummUpProducts!.OrderBy(x => x.Id));
            if (GetHashCode() != basket.GetHashCode() || !equals)
                return false;
            return true;
        }

        public event ChangeBasketState? ChangeBasket;
        public void OnChanged(object obj)
        {
            ChangeBasketState? handler = ChangeBasket;
            handler?.Invoke(obj!);
        }
        public void AddProductInBasket(Product? product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            var id = product.Id;
            var count = FindQuantityProductsInBasket(id);
            if (count == 0)
            {
                var summUpProduct = new SummUpProduct
                {
                    Id = id,
                    Product = product,
                    Quantity = 1,
                    TotalPrice = product!.Cost
                };
                SummUpProducts!.Add(summUpProduct);
            }
            else
                SummUpProducts!.First(x => x.Product!.Id == id).Quantity++;
            TotalCost += product!.Cost;
        }

        public void RemoveProductInBasket(Product? product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            var id = product.Id;
            var count = FindQuantityProductsInBasket(id);
            if (count == 1)
            {
                var summUpProduct = new SummUpProduct
                {
                    Id = id,
                    Product = product,
                    Quantity = 1,
                    TotalPrice = product!.Cost
                };
                SummUpProducts!.Remove(summUpProduct);
            }
            else if (count > 1)
                SummUpProducts!.First(x => x.Product!.Id == id).Quantity--;
            TotalCost -= product!.Cost;
        }
        private int FindQuantityProductsInBasket(int id)
        {
            if (SummUpProducts!.Any(x => x.Product!.Id == id))
                return SummUpProducts!.FirstOrDefault(x => x.Product!.Id == id)!.Quantity;
            return 0;
        }
    }
}
