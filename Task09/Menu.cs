using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Collections;

namespace Task09
{
    class Menu : IEnumerable
    {
        private readonly List<Dish> menu;
        public Dish this[int index]
        {
            get
            {
                return menu[index];
            }
        }
        public Menu()
        {
            menu = new List<Dish>();
        }
        public Menu(in List<Dish> menu)
        {
            this.menu = menu;
        }
        public IEnumerator GetEnumerator() => menu.GetEnumerator();
        public Menu(in string fileName)
        {
            menu = new List<Dish>();
            string text = File.ReadAllText(fileName);
            string[] dishes = text.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in dishes)
            {
                Dictionary<string, decimal> dict = new Dictionary<string, decimal>();
                string[] dish = item.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in dish.Skip(1))
                {
                    string[] product = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    dict.Add(product[0], decimal.Parse(product[1]));
                }
                menu.Add(new Dish(dish[0], dict));
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Dish item in menu)
            {
                sb.Append(item);
            }
            return sb.ToString();
        }
    }
}