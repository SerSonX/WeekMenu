using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace WeekMenu
{
    [Table("ProductNames")]
    public class ProductName
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
    }
    [Table("Products")]
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int NameId { get; set; }
        public double Count { get; set; }
        public string ExpirationDate { get; set; }
    }

    public class ProductView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountAndUnit { get; set; }
        public string ExpirationDate { get; set; }

    }

    [Table("Dishes")]
    public class Dish
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [Table("Ingredients")]
    public class Ingredient
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int DishId { get; set; }
        public int ProductNameId { get; set; }
        public double Count { get; set; }
    }

    public class IngredientView
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountAndUnit { get; set; }
    }

    public class DishesInDayView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
    }

    [Table("DaysAndDishes")]
    public class DayAndDish
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Day { get; set; }
        public int DishId { get; set; }
        public int Type { get; set; }
    }
}
