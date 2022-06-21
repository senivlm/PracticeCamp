using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;

namespace Task09
{
    public enum Currency
     {
         UAH,
         USD,
         EUR
     }
     class Course
     {
         public Currency currency;
         private readonly Dictionary<Currency, decimal> courses;
         public Course()
         {
            courses = new Dictionary<Currency, decimal>
            {
                [Currency.UAH] = 1.0m
            };
         }
         public Course(in string fileName, in Currency currency) : this()
         {
            this.currency = currency;
            string[] coursesFile = File.ReadAllLines(fileName);
            foreach (var item in coursesFile)
             {
                 string[] course = item.Split(new string[] { " = " }, StringSplitOptions.RemoveEmptyEntries);
                 courses.Add((Currency)Enum.Parse(typeof(Currency), course[0]), 
                             decimal.Parse(course[1],
                                           NumberStyles.Any,
                                           CultureInfo.InvariantCulture));
             }
        }
        public decimal this[Currency key]
        {
            get
            {
                return courses[key];
            }
        }
        public override string ToString()
         {
             StringBuilder sb = new StringBuilder();
             foreach (KeyValuePair<Currency, decimal> item in courses)
             {
                 sb.Append($"{item.Key} = {item.Value}\n");
             }
             return sb.ToString();
         }
     }
}
