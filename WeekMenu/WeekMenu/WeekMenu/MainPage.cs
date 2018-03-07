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
            Master = new MasterPage();
            Detail = new NavigationPage(new AboutPage());
		}
	}
}