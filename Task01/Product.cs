using System;

namespace hw_entry
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
            if (obj != null && obj is Product product)
            {
                return Name == product.Name &&
                       Price == product.Price &&
                       Weight == product.Weight;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
