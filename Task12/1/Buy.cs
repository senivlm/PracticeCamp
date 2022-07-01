
namespace Task12
{
    public delegate void ExpirationDate(Buy buy);
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
            return obj is Buy buy &&
                   Product.Equals(buy.Product);
        }
        public override int GetHashCode()
        {
            return Product.GetHashCode();
        }
        public static bool operator ==(in Buy left, in Buy right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(in Buy left, in Buy right)
        {
            return !(left == right);
        }
    }
}
