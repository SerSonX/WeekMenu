using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeekMenu.iOS;
using Xamarin.Forms;
using System.IO;

using Foundation;
using UIKit;

[assembly: Dependency(typeof(SQLite_iOS))]
namespace WeekMenu.iOS
{
    public class SQLite_iOS : ISQLite
    {
        public SQLite_iOS() { }
        public string GetDatabasePath(string sqliteFilename)
        {
            // определяем путь к бд
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // папка библиотеки
            var path = Path.Combine(libraryPath, sqliteFilename);

            return path;
        }
    }
}