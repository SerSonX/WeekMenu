using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WeekMenu
{
    public class AboutPage : ContentPage
    {
        public AboutPage()
        {
            Button donate = new Button
            {
                Text = "Donate",

            };
            donate.Clicked += Donate_Clicked;
            Title = "О нас";
            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = "Это приложение является проектной работой."
                    },
                    new Label
                    {
                        Text = "Автор: Сергей Листишенков (SerSX)."
                    },
                    new Label
                    {
                        Text = "Научный руководитель: Анфалов Евгений (Anfalov)."
                    },
                    donate

                }
            };

        }

        private async void Donate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContentPage());
        }
    }
    
}