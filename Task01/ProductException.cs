using System;

namespace hw_entry
{
    public class ProductException : Exception
    {
        public ProductException(in string name, in decimal price, in double weight)
            : base($"Some data: {name} {price} {weight} are incorrect. Beware!")
        { }
        public ProductException(in Product product, in int amount)
            : base($"{product.Name} has amount of {amount}")
        { }
        public ProductException(in string name)
            : base($"No expiration date given for {name}")
        { }
    }
}
