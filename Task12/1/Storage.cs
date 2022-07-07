using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Task12
{
    public class Storage
    {// З проєктуванням біда. Є суттєві порушення принципів Solid і немає абстракції!!!!
        public event ExpirationDate ExpirationDatePassed;

        private readonly List<Buy> products;
        private readonly string fileNameLog;
        private readonly string fileName;
        public Storage()
        {
            products = new List<Buy>();
            ExpirationDatePassed = Remove;
            ExpirationDatePassed += Write;
        }
        public Storage(in List<Buy> products) : this()
        {
            foreach (Buy item in products)
            {
                this.products.Add(item);
            }
        }
        public Storage(in string fileNameLog, in string fileName) : this()
        {
            if (!File.Exists(fileNameLog))
            {
                throw new Exception($"File {fileNameLog} is not found");
            }
            if (!File.Exists(fileName))
            {
                throw new Exception($"File {fileName} is not found");
            }
            try
            {
                this.fileNameLog = fileNameLog;
                this.fileName = fileName;
            }
            catch (IOException exp)
            {
                throw new Exception(exp.Message);
            }
            Parse();
        }
        private void Parse()
        {
            string[] plainText = File.ReadAllLines(fileName);
            if (plainText.Length != 0) {
                foreach (string item in plainText)
                {
                    string[] line = item.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (Enum.IsDefined(typeof(Category), line[0]))
                    {
                        string name = char.ToUpper(line[2][0]) + line[2].Substring(1);
                        products.Add(new Buy(
                            new Meat(
                                (Category)Enum.Parse(typeof(Category), line[0]),
                                (Species)Enum.Parse(typeof(Species),
                                line[1]),
                                name,
                                decimal.Parse(line[3]),
                                double.Parse(line[4])

                            ), int.Parse(line[5])
                            ));
                    }
                    else if (DateTime.TryParse(line[0], out DateTime dt))
                    {
                        string name = char.ToUpper(line[1][0]) + line[1].Substring(1);
                        products.Add(new Buy(
                            new DairyProducts(
                                dt, name, decimal.Parse(line[2]), double.Parse(line[3])
                                ), int.Parse(line[4])
                            ));
                    }
                    else
                    {
                        string name = char.ToUpper(line[0][0]) + line[0].Substring(1);
                        products.Add(new Buy(
                            new Product(
                                name,
                                decimal.Parse(line[1]),
                                double.Parse(line[2])),
                            int.Parse(line[3])
                            ));
                    }
                }
                CheckExpirationDate();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        public string FindProduct(Predicate<Buy> find)
        {
            return string.Join<Buy>('\n',products.FindAll(find).ToArray());
        }
        private void Remove(Buy buy)
        {
            products.Remove(buy);
        }
        private void Write(Buy buy)         
        {
            using StreamWriter writer = new StreamWriter(fileNameLog, true);
            writer.WriteLine($"{DateTime.Now} - removed {buy.Product.Name} " +
                $"with amount of {buy.Amount} {(buy.Amount > 1 ? "items" : "item")} " +
                $"expired {(DateTime.Now.Date - (buy.Product as DairyProducts).ExpirationDate).TotalDays} days ago");
        }
        public void CheckExpirationDate()
        {
            for (int i = 0; i < products.Count(); i++)
            {
                if (products[i].Product is DairyProducts dairyProduct &&
                    DateTime.Now > dairyProduct.ExpirationDate)
                {
                    ExpirationDatePassed?.Invoke(products[i--]);
                }
            }
        }
        public Storage Intersect(in Storage other)
        {
            if(this == null || other == null)
            {
                throw new ArgumentNullException("No data given");
            }
            return new Storage(products.Intersect(other.products).ToList()); ;
        }
        public Storage Union(in Storage other)
        {
            if (this == null && other == null)
            {
                throw new ArgumentNullException("No data given");
            }
            else if (this == null){
                return other;
            }
            else if(other == null)
            {
                return this;
            }
            return new Storage(products.Union(other.products).ToList());
        }
        public Storage Except(in Storage other)
        {
            if (this == null)
            {
                throw new ArgumentNullException("No data given");
            }
            else if (other == null)
            {
                return this;
            }
            return new Storage(products.Except(other.products).ToList());
        }
        public override bool Equals(object obj)
        {
            return obj is Storage storage &&
                   products.Equals(storage.products);
        }
        public override int GetHashCode()
        {
            return products.GetHashCode();
        }
        public static bool operator ==(in Storage left, in Storage right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(in Storage left, in Storage right)
        {
            return !(left == right);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            decimal sum = 0;
            foreach (Buy item in products)
            {
                sb.Append($"\nProduct:\n{item.Product}\nAmount: {item.Amount}\nSum: {item.Sum}");
                sum += item.Sum;
            }
            sb.Append($"\nTotal: {sum}");
            return sb.ToString();
        }
    }
}
