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
            Title = "О программе";
            BackgroundColor = Color.White;
            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = "Версия: 0.9"
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
                        Text = "E-mail: SerSX@icloud.com"
                    },
                    new Label
                    {
                        Text = "Научный руководитель: Анфалов Евгений (Anf)."
                    },
                    new Label
                    {
                        Text = "E-mail: anfalovd10@yandex.ru"
                    }

                }
            };

        }
    }
    
}