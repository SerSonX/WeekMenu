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
            Title = "О нас";
            BackgroundColor = Color.White;
            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = "Версия: 1.0"
                    },
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
                        Text = "Научный руководитель: Анфалов Евгений (Anf)."
                    }

                }
            };

        }
    }
    
}