using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WeekMenu
{
	public class MasterPage : ContentPage
	{
        void setButtonSettings(Button b)
        {
            b.TextColor = Color.Black;
            b.BackgroundColor = Color.White;
            b.BorderColor = Color.Black;
            //b.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button));
            b.BorderRadius = 0;
        }
        MasterDetailPage mainPage;
		public MasterPage (MasterDetailPage mdp)
		{
            mainPage = mdp;
            Button about = new Button
            {
                Text = "О нас",
            };
            about.Clicked += About_Clicked;
            setButtonSettings(about);
            Button settings = new Button
            {
                Text = "Настройки",
            };
            setButtonSettings(settings);
            Button menu = new Button
            {
                Text = "Ваше меню",
            };
            setButtonSettings(menu);
            menu.Clicked += Menu_Clicked;
            Button products = new Button
            {
                Text = "Список продуктов",
            };
            setButtonSettings(products);
            Title = "Меню на неделю";
            products.Clicked += Products_Clicked;
            Content = new StackLayout
            {
                //HorizontalOptions = LayoutOptions.FillAndExpand,
                //VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(0, 0, 0, 0),
                Children = {
                    menu,products,settings,about }
            };
		}

        private void Menu_Clicked(object sender, EventArgs e)
        {
            mainPage.Detail = new NavigationPage(new YourMenuPage());
            mainPage.IsPresented = false;
        }

        private void Products_Clicked(object sender, EventArgs e)
        {
            mainPage.Detail = new NavigationPage(new ProductsPage());
            mainPage.IsPresented = false;
        }

        private void About_Clicked(object sender, EventArgs e)
        {
            mainPage.Detail = new NavigationPage(new AboutPage());
            mainPage.IsPresented = false;
        }
    }
}