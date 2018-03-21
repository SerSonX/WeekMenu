using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WeekMenu
{
	public partial class App : Application
	{
        public const string DATABASE_NAME = "weekmenu.db";
        static Repository database;
        public static Repository Database
        {
            get
            {
                if (database == null)
                {
                    database = new Repository(DATABASE_NAME);
                }
                return database;
            }
        }
        public App ()
		{
            InitializeComponent();
            MainPage = new WeekMenu.MainPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
