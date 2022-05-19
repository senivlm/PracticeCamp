using System;

namespace hw_entry
{
    public class DairyProducts : Product
    {
        public DateTime ExpirationDate { get; private set; }

        public DairyProducts(in DateTime expirationDate, in string name, in decimal price, in double weight)
            : base(name, price, weight)
        {
            if (expirationDate == null)
                throw new ProductException(name);
            ExpirationDate = expirationDate;
        }
        public override void ChangePrice(int percentage)
        {
            if (DateTime.Now > ExpirationDate)
            {
                percentage /= 2;
            }
            Price = (decimal)((double)Price * (percentage / 100d));
        }
        public override string ToString()
        {
            return $"{base.ToString()} Expiration date: {Math.Round((ExpirationDate - DateTime.Now).TotalDays)} days are left";
        }
        public override bool Equals(object obj)
        {
            return obj is DairyProducts products &&
                   base.Equals(obj) &&
                   Name == products.Name &&
                   Price == products.Price &&
                   Weight == products.Weight &&
                   ExpirationDate == products.ExpirationDate;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Name, Price, Weight, ExpirationDate);
        }
    }
}
