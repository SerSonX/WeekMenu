using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WeekMenu
{
	public class HaventProducts : ContentPage
	{
		public HaventProducts ()
		{
            BackgroundColor = Color.White;
            Title = "Недостающие продукты";
            ListView productList = new ListView();
            productList.ItemsSource = App.Database.ProductsViewList;
            productList.ItemTemplate = new DataTemplate(() =>
            {
                Label nameLabel = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1
                };
                nameLabel.SetBinding(Label.TextProperty, "Name");

                Label countAndUnitLabel = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1
                };
                countAndUnitLabel.SetBinding(Label.TextProperty, "CountAndUnit");

                Grid cellGrid = new Grid
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                cellGrid.Children.Add(nameLabel, 0, 2, 0, 1);
                cellGrid.Children.Add(countAndUnitLabel, 2, 3, 0, 1);

                return new ViewCell
                {
                    View = cellGrid
                };
            });
            productList.IsEnabled = false;
            Grid titleGrid = new Grid()
            {
                BackgroundColor = Color.White,
                ColumnSpacing = 2,
                RowSpacing = 2
            };

            titleGrid.Children.Add(new Label
            {
                BackgroundColor = Color.FromHex("c3fdff"),
                Text = " Продукты",
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize =
               Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1.1
            }, 0, 2, 0, 1);

            titleGrid.Children.Add(new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("c3fdff"),
                FontAttributes = FontAttributes.Bold,
                Text = " Кол-во",
                FontSize =
               Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1.1
            }, 2, 3, 0, 1);

            Content = new StackLayout
            {
                Padding = new Thickness(1, 2, 1, 1),
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                   titleGrid,
                    productList
                }
            };
        }
	}
}