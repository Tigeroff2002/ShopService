namespace ShopService.Models
{
    public class Basket
    {
        public virtual User? Client { get; set; }
        public virtual ICollection<SummUpProduct>? SummUpProducts { get; set; }
        public int BasketStatusId { get; set; } = 0;
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
    }
}
