using System;
using System.Collections.Generic;
using System.Text;

namespace hw_entry
{
    public class Storage
    {
        public List<Buy> Products { get; set; } = new List<Buy>();
        public Storage(in List<Buy> products)
        {
            Products = products;
        }
        public Buy this[int i]
        {
            get
            {
                return Products[i];
            }
            set
            {
                Products[i] = value;
            }
        }
        public void CreateDialogue()
        {
            while (true)
            {
                Console.WriteLine("Input name of new item:");
                string name = Console.ReadLine().Trim();
                Console.WriteLine("Input price of new item:");
                decimal.TryParse(Console.ReadLine().Trim(), out decimal price);
                Console.WriteLine("Input weight of new item:");
                double.TryParse(Console.ReadLine().Trim(), out double weight);
                Console.WriteLine("Input amount of item:");
                int.TryParse(Console.ReadLine().Trim(), out int amount);
                Console.WriteLine("Anything else? Yes/No");
                Products.Add(new Buy(new Product(name, price, weight), amount));
                if (Console.ReadLine().Trim().ToLower() == "no")
                {
                    break;
                }
            }
        }
        public void InitializeData(in string name, in decimal price, in double weight, in int amount)
        {
            Buy buy = new Buy
            {
                Product =
                {
                    Name = name, Price = price, Weight = weight },
                Amount = amount
            };
            Products.Add(buy);
        }
        public List<Meat> FindMeat()
        {
            List<Meat> meat = new List<Meat>();
            foreach (Buy item in Products)
            {
                if (item.Product is Meat)
                    meat.Add(item.Product as Meat);
            }
            return meat;
        }
        public void ChangePrice(in int percentage)
        {
            foreach (Buy item in Products)
            {
                item.Product.ChangePrice(percentage);
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            decimal sum = 0;
            foreach (Buy item in Products)
            {
                sb.Append($"\nProduct:\n{item.Product}\nAmount: {item.Amount}\nSum: {item.Sum}");
                sum += item.Sum;
            }
            sb.Append($"\nTotal: {sum}");
            return sb.ToString();
        }
    }
}
