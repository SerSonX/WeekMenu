using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;

namespace WeekMenu
{
    public class ProductRepository
    {
        SQLiteConnection database;
        public ProductRepository(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Product>();
        }
        public IEnumerable<Product> GetItems()
        {
            return (from i in database.Table<Product>() select i).ToList();

        }
        public Product GetItem(int id)
        {
            return database.Get<Product>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<Product>(id);
        }
        public int SaveItem(Product item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}
