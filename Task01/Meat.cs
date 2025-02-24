﻿using System;

namespace hw_entry
{
    public class Meat : Product
    {
        public Category MeatCategory { get; }
        public Species MeatSpecies { get; }

        public Meat(in Category meatCategory, in Species meatSpecies,
            in string name, in decimal price, in double weight)
                : base(name, price, weight)
        {
            MeatCategory = meatCategory;
            MeatSpecies = meatSpecies;
        }
        public override void ChangePrice(int percentage)
        {
            switch (MeatCategory)
            {
                case Category.HighSort:
                    percentage += 25;
                    break;
                case Category.Sort1:
                    percentage += 10;
                    break;
                case Category.Sort2:
                    percentage += 5;
                    break;
                default:
                    break;
            }
            Price = (decimal)((double)Price * (percentage / 100d));
        }
        public override bool Equals(object obj)
        {
            return obj is Meat meat &&
                   base.Equals(obj) &&
                   Name == meat.Name &&
                   Price == meat.Price &&
                   Weight == meat.Weight &&
                   MeatCategory == meat.MeatCategory &&
                   MeatSpecies == meat.MeatSpecies;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Name, Price, Weight, MeatCategory, MeatSpecies);
        }
        public override string ToString()
        {
            return $"{base.ToString()} Sort: {MeatCategory} Species: {MeatSpecies}";
        }
    }

}
