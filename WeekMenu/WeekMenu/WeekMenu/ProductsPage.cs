using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WeekMenu
{
    public class ProductsPage : ContentPage
    {
        public ProductsPage()
        {
            Title = "Продукты";
            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = "Морковь 1.5 кг"
                    },
                    new Label
                    {
                        Text = "Огурец 1кг"
                    },
                    new Label
                    {
                        Text = "Картофель 3кг"
                    },
                    new Label
                    {
                        Text = "Лук 1кг"
                    },
                    new Label
                    {
                        Text = "Федук 73кг"
                    },
                }
            };
        }

    }
}