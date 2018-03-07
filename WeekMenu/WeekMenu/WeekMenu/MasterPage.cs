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
            b.FontSize = b.FontSize;
            b.BorderRadius = 0;
        }
		public MasterPage ()
		{
            Button about = new Button
            {
                Text = "О нас",
            };
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
            Button products = new Button
            {
                Text = "Список продуктов",
            };
            setButtonSettings(products);
            Title = "Меню на неделю";
            Content = new StackLayout
            {
                //HorizontalOptions = LayoutOptions.FillAndExpand,
                //VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(0, 0, 0, 0),
                Children = {
                    menu,products,settings,about }
            };
		}
	}
}