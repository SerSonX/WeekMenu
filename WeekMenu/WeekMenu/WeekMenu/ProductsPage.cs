using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WeekMenu
{
    public class ProductsPage : ContentPage
    {

        private ListView productList = new ListView(); 

        public ProductsPage()
        {
            Title = "Продукты";
            productList.ItemsSource = App.Database.GetItems();
            productList.ItemTemplate = new DataTemplate(() =>
            {
                Label nameLabel = new Label();
                Label countAndUnitLabel = new Label();
                Label expirationDateLabel = new Label();
                nameLabel.SetBinding(Label.TextProperty, "Name");
               // nameLabel.FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1.2;
                countAndUnitLabel.SetBinding(Label.TextProperty, "Count");
                //countAndUnitLabel.FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1.2;
                expirationDateLabel.SetBinding(Label.TextProperty, "ExpirationDate");
               // expirationDateLabel.FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1.2;
                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Children =
                         {
                            nameLabel, countAndUnitLabel, expirationDateLabel
                         }
                    }
                };
            });
            Content = new StackLayout
            {
                Children =
                {
                    productList
                }
            };
        }

    }
}