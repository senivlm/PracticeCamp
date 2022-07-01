using System;

namespace Task12
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Weight { get; set; }
        public Product()
        { }
        public Product(in string name, in decimal price, in double weight)
        {
            if (string.IsNullOrEmpty(name) || price <= 0 || weight <= 0)
            {
                throw new ProductException(name, price, weight);
            }
            Name = name;
            Price = price;
            Weight = weight;
        }
        public virtual void ChangePrice(int percentage)
        {
            Price = (decimal)((double)Price * (percentage / 100d));
        }
        public override string ToString()
        {
            return $"Name: {Name} Price: {Price} Weight: {Math.Round(Weight, 3)}";
        }
        public override bool Equals(object obj)
        {
            return obj is Product product &&
                   Name == product.Name &&
                   Price == product.Price &&
                   Weight == product.Weight;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Price, Weight);
        }
        public static bool operator ==(in Product left, in Product right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(in Product left, in Product right)
        {
            return !(left == right);
        }
    }
}
