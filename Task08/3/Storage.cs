using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Task08
{
    public class Storage
    {
        public List<Buy> Products;

        readonly Log log;
        
        public Storage(string file)
        {
            Products = new List<Buy>();
            log = new Log(file);
        }
        public Storage(in List<Buy> products)
        {
            Products = new List<Buy>();
            foreach (Buy item in products)
            {
                Products.Add(item);
            }
        }
        private string FindFile()
        {
            string file;
            do
            {
                Console.WriteLine("Input file name: ");
                file = Console.ReadLine();
            }
            while (!File.Exists($"..\\..\\..\\{file}"));
            return $"..\\..\\..\\{file}";
        }
        private void Parse()
        {
            string[] plainText = File.ReadAllLines(FindFile());
            foreach (string item in plainText)
            {
                string[] line = item.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    if (Enum.IsDefined(typeof(Category), line[0]))
                    {
                        string name = char.ToUpper(line[2][0]) + line[2].Substring(1);
                        Products.Add(new Buy(
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
                        Products.Add(new Buy(
                            new DairyProducts(
                                dt, name, decimal.Parse(line[2]), double.Parse(line[3])
                                ), int.Parse(line[4])
                            ));
                    }
                    else
                    {
                        string name = char.ToUpper(line[0][0]) + line[0].Substring(1);
                        Products.Add(new Buy(
                            new Product(
                                name, 
                                decimal.Parse(line[1]), 
                                double.Parse(line[2])), 
                            int.Parse(line[3])
                            ));
                    }
                }
                catch(Exception e)
                {
                    log.Add(e.Message);
                }
            }
        }
        public List<Buy> ShowInfo()
        {
            Parse();
            return Products;
        }
        public string ShowLogs()
        {
            return log.ToString();
        }
        public void CorrectLine(string dt)
        {
            log.CorrectLine(dt);
        }
        public Storage Intersect(in Storage other)
        {
            if(this == null || other == null)
            {
                throw new ArgumentNullException("No data given");
            }
            return new Storage(Products.Intersect(other.Products).ToList()); ;
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
            return new Storage(Products.Union(other.Products).ToList());
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
            return new Storage(Products.Except(other.Products).ToList());
        }
        public override bool Equals(object obj)
        {
            return obj is Storage storage &&
                   Products.Equals(storage.Products);
        }
        public override int GetHashCode()
        {
            return Products.GetHashCode();
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
