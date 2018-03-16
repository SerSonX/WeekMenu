using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Xamarin.Forms;
namespace WeekMenu.Droid
{
    [Activity(Label = "WeekMenu", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            createDB_IfDoesntExist();
            LoadApplication(new App());
        }

        void createDB_IfDoesntExist()
        {
            string sqliteFilename = WeekMenu.App.DATABASE_NAME;

            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            //if (File.Exists(path))
            {

                var dbAssetStream = this.Assets.Open(sqliteFilename);

                var dbFileStream = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate);
                var buffer = new byte[1024];

                int b = buffer.Length;
                int length;

                while ((length = dbAssetStream.Read(buffer, 0, b)) > 0)
                {
                    dbFileStream.Write(buffer, 0, length);
                }

                dbFileStream.Flush();
                dbFileStream.Close();
                dbAssetStream.Close();
                //var sw = new StreamWriter(path);

                //using (var sr = new StreamReader(assets.Open(sqliteFilename)))
                //{
                //    sw.Write(sr.ReadToEnd());
                //}
                //sw.Close();
            }
        }
    }
}

