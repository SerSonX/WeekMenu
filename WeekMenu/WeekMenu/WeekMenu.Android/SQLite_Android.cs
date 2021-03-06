﻿using System;
using WeekMenu.Droid;
using System.IO;
using Xamarin.Forms;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

[assembly: Dependency(typeof(SQLite_Android))]
namespace WeekMenu.Droid
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android() { }
        public string GetDatabasePath(string sqliteFilename)
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            if (!File.Exists(path))
            {
                var dbAssetStream = Forms.Context.Assets.Open(sqliteFilename);
                var dbFileStream = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate);
                var buffer = new byte[1024];
                int b = buffer.Length;
                int length;
                while ((length = dbAssetStream.Read(buffer, 0, b)) > 0)
                    dbFileStream.Write(buffer, 0, length);
                dbFileStream.Flush();
                dbFileStream.Close();
                dbAssetStream.Close();
            }
                return path;
        }
    }
}