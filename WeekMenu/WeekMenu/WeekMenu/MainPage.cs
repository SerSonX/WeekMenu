using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WeekMenu
{
	public class MainPage : MasterDetailPage
	{
		public MainPage ()
		{

            Master = new MasterPage(this);
            //Detail = new NavigationPage(new AboutPage());
            Detail = new NavigationPage(new ProductsPage())
            {
                BarBackgroundColor = Color.FromHex("76ff03")
            };

        }
	}
}