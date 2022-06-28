using System.Collections.Generic;
using System.Text;
using System;

namespace Task09
{
    static class MenuService
    {
        static public bool TryGetMenuPrice(in Menu menu, 
                                           in PriceList priceList, 
                                           in Course course, 
                                           out decimal total)
        {
            total = default;
            foreach (Dish item in menu)
            {
                if (!TryGetDishPrice(item, priceList, course, out decimal dishPrice))
                {
                    return false;
                }
                total += dishPrice;
            }
            total = Math.Round(total, 2);
            return true;
        }
        static public bool TryGetDishPrice(in Dish dish, 
                                           in PriceList priceList, 
                                           in Course course,
                                           out decimal dishPrice)
        {
            dishPrice = default; 
            foreach (KeyValuePair<string, decimal> item in dish)
            {
                if (!priceList.TryGetProductPrice(item.Key, out decimal productPrice))
                {
                    return false;
                }
                dishPrice += ((productPrice * item.Value / course[course.currency]) / 1000m);
                dishPrice = Math.Round(dishPrice, 2);
            }
            return true;
        }
        //Не дуже  добре об'єднувати 2 різні складові в один метод(тобто формування текстової інформації та обчислення суми. Якщо Вам треба тільки одна складова, то отримаєте надлишковість.)
        static public string GetDishes(in Menu menu, 
                                       in PriceList priceList, 
                                       in Course course,
                                       out decimal total)
        {
            StringBuilder sb = new StringBuilder();
            total = default;
            foreach (Dish item in menu)
            {
                if (!TryGetDishPrice(item, priceList, course, out decimal dishPrice))
                {
                    return sb.ToString();
                }
                total += dishPrice;
                sb.Append($"{item.Title} : {course.currency} {Math.Round(dishPrice, 2)}\n");
            }
            return sb.ToString();
        }
        static public Dictionary<string, decimal> GetProductsAmount(in Menu menu)
        {
            Dictionary<string, decimal> amount = new Dictionary<string, decimal>();
            foreach (Dish item in menu)
            {
                foreach (KeyValuePair<string, decimal> data in item)
                {
                    if (amount.ContainsKey(data.Key))
                    {
                        amount[data.Key] += data.Value;
                    }
                    else
                    {
                        amount.Add(data.Key, data.Value);
                    }
                }
            }
            return amount;
        }
        static public Dictionary<string, decimal> GetProductsTotal(in Menu menu, 
                                                                   in PriceList priceList, 
                                                                   in Course course)
        {
            Dictionary<string, decimal> products = GetProductsAmount(menu);
            Dictionary<string, decimal> productsTotal = new Dictionary<string, decimal>();
            foreach (KeyValuePair<string, decimal> item in products)
            {
                if(priceList.TryGetProductPrice(item.Key, out decimal productPrice))
                {
                    productsTotal.Add(item.Key, 
                                      Math.Round(item.Value * productPrice / course[course.currency] / 1000m, 2));
                }
            }
            return productsTotal;
        }
        static public string PrintProductCosts(in Menu menu,
                                               in PriceList priceList, 
                                               in Course course)
        {
            Dictionary<string, decimal> query = GetProductsAmount(menu);
            Dictionary<string, decimal> query1 = GetProductsTotal(menu, priceList, course);
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, decimal> item in query)
            {
                sb.Append($"Restaurant needs {item.Value} " +
                          $"gram of {item.Key} for " +
                          $"{course.currency} {query1[item.Key]}\n");
            }
            return sb.ToString();
        }
    }
}

    
