using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;

namespace WeekMenu
{
    public class Repository
    {
        public SQLiteConnection Database;
        private Dictionary<int, ProductName> namesOfProudcts;
        public Dictionary<int, ProductName> NamesOfProudcts
        {
            get
            {
                if (namesOfProudcts == null)
                    namesOfProudcts = Database.Table<ProductName>().
                    Select(p => p).ToDictionary(p => p.Id, p => p);
                return namesOfProudcts;
            }
        }

        private List<Product> productsList;
        public List<Product> ProductsList
        {
            get
            {
                if (productsList==null)
                    productsList=Database.Table<Product>().Select(p => p).ToList();
                return productsList;
            }
        }

        private List<ProductView> productsViewList;
        public List<ProductView> ProductsViewList
        {
            get
            {
                if (productsViewList == null)
                    productsViewList = ProductsList.Select(p => new ProductView
                    {
                        Id = p.Id,
                        Name = NamesOfProudcts[p.NameId].Name,
                        CountAndUnit = p.Count.ToString() + " " + NamesOfProudcts[p.NameId].Unit,
                        ExpirationDate = p.ExpirationDate
                    }).ToList();
                return productsViewList;
            }
        }
        public Repository(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            Database = new SQLiteConnection(databasePath);
            Database.CreateTable<Product>();
            Database.CreateTable<ProductName>();
        }
        public IEnumerable<Product> GetItems()
        {
            return (from i in Database.Table<Product>() select i).ToList();

        }
        public Product GetItem(int id)
        {
            return Database.Get<Product>(id);
        }
        public int DeleteItem(int id)
        {
            return Database.Delete<Product>(id);
        }
        public int SaveItem(Product item)
        {
            if (item.Id != 0)
            {
                Database.Update(item);
                return item.Id;
            }
            else
            {
                return Database.Insert(item);
            }
        }
    }
}
