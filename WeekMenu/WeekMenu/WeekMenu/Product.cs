using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace WeekMenu
{
    [Table("Products")]
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Count { get; set; }
        public string Unit { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
