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
        public int Id { get; set;}
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
}
