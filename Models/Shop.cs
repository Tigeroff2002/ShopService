using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Models
{
    public class Shop : IEquatable<Shop>
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? WorkingTime { get; set; }
        public virtual ICollection<Shipping>? Shippings { get; set; }
        public virtual ICollection<SummUpProduct>? ProductQuantities { get; set; }

        public Shop(string name, string address, string workingTime)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            Name = name;

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException(nameof(address));

            Address = address;

            if (string.IsNullOrWhiteSpace(workingTime))
                throw new ArgumentException(nameof(workingTime));

            WorkingTime = workingTime;

            Shippings = new List<Shipping>();
            ProductQuantities = new List<SummUpProduct>();
        }

        public bool CheckExistenseProductOnShop(int _goodId)
        {
            ArgumentNullException.ThrowIfNull(ProductQuantities);

            var quantity = ProductQuantities!.First(x => x.Id == _goodId).Quantity;
            if (quantity > 0)
                return true;
            return false;
        }

        public int GetQuantityOfProduct(int _goodId)
        {
            ArgumentNullException.ThrowIfNull(ProductQuantities);

            if (!ProductQuantities!.Any(x => x.Id == _goodId))
                return 0;

            return ProductQuantities!.First(x => x.Id == _goodId).Quantity;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (obj is Shop shop)
                return Equals(shop);
            else
                return false;
        }
        public bool Equals(Shop? shop)
        {
            if (shop == null)
                return false;

            return Id == shop.Id;
        }

        public override int GetHashCode()
        {
            return (Id).GetHashCode();
        }
    }
}
