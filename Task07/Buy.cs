
namespace Task07
{
    public class Buy
    {
        public Product Product { get; set; }
        public int Amount { get; set; }
        public decimal Sum { get; }
        public double Weight { get; }
        public Buy()
        {
            Product = new Product();
        }
        public Buy(in Product product, in int amount = 1)
        {
            if (amount <= 0)
            {
                throw new ProductException(product, amount);
            }
            Product = product;
            Amount = amount;
            Sum = product.Price * Amount;
            Weight = product.Weight * Amount;
        }
        public override string ToString()
        {
            return $"Product: {Product} Amount: {Amount} Sum: {Sum}";
        }
        public override bool Equals(object obj)
        {
            if (obj != null && obj is Buy buy)
            {
                return Product == buy.Product &&
                       Amount == buy.Amount &&
                       Sum == buy.Sum;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
