using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Task09
{
    class PriceList
    {
        private readonly Dictionary<string, decimal> prices;
        public PriceList()
        {
            prices = new Dictionary<string, decimal>();
        }
        public PriceList(in Dictionary<string, decimal> prices)
        {
            this.prices = prices;
        }
        public PriceList(in string fileName)
        {
            prices = new Dictionary<string, decimal>();
            string[] priceList = File.ReadAllLines(fileName);
            foreach (var item in priceList)
            {
                string[] price = item.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                prices.Add(price[0].Trim(), decimal.Parse(price[1]));
            }
        }
        public bool TryGetProductPrice(in string title, out decimal price)
        {
            try
            {
                if (!prices.TryGetValue(title, out price))
                {
                    throw new ArgumentException();
                }
                else
                {
                    return true;
                }
            }
            catch (ArgumentException)
            {
                if (!decimal.TryParse(Console.ReadLine(), out decimal productPrice))
                    throw;
                prices.Add(title, productPrice);
                price = productPrice;
                return true;
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, decimal> item in prices)
            {
                sb.Append($"{item.Key} - {item.Value}\n");
            }
            return sb.ToString();
        }
    }
}