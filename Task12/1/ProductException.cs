using System;

namespace Task12
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
        public ProductException(in Category category)
            :base($"the sort of meat {category} is undefined")
        { }
        public ProductException(in Species species)
            : base($"The kind of meat {species} is undefined")
        { }
    }
}
