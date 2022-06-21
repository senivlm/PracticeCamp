using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Task09
{
    class Dish : IEnumerable
    {
        public string Title { get; set; }
        private readonly Dictionary<string, decimal> ingredients;
        public IEnumerator GetEnumerator() => ingredients.GetEnumerator();
        public Dish() { }
        public Dish(in string title, in Dictionary<string, decimal> ingredients)
        {
            Title = title;
            this.ingredients = ingredients;
        }
        public decimal this[string key]
        {
            get
            {
                return ingredients[key];
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, decimal> item in ingredients)
            {
                sb.Append($"{item.Key} - {item.Value}\n");
            }
            return $"{Title}\n{sb}";
        }
    }
}