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
            BackgroundColor = Color.White;
            Title = "Продукты";
            
            productList.ItemsSource = App.Database.ProductsViewList;
            productList.ItemTemplate = new DataTemplate(() =>
            {
                Label nameLabel = new Label();
                Label countAndUnitLabel = new Label();
                Label expirationDateLabel = new Label();
                nameLabel.HorizontalTextAlignment = TextAlignment.Center;
                nameLabel.VerticalTextAlignment = TextAlignment.Center;
                nameLabel.SetBinding(Label.TextProperty, "Name");
                nameLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) * 1;
                countAndUnitLabel.HorizontalTextAlignment = TextAlignment.Center;
                countAndUnitLabel.VerticalTextAlignment = TextAlignment.Center;
                countAndUnitLabel.SetBinding(Label.TextProperty, "CountAndUnit");
                countAndUnitLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) * 1;
                expirationDateLabel.HorizontalTextAlignment = TextAlignment.Center;
                expirationDateLabel.VerticalTextAlignment = TextAlignment.Center;
                expirationDateLabel.SetBinding(Label.TextProperty, "ExpirationDate");
                expirationDateLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) * 1;
                Grid cellGrid = new Grid();
                cellGrid.Children.Add(nameLabel, 0, 2, 0, 1);
                cellGrid.Children.Add(countAndUnitLabel, 2, 3, 0, 1);
                cellGrid.Children.Add(expirationDateLabel, 3, 5, 0, 1);
                cellGrid.HorizontalOptions = LayoutOptions.FillAndExpand;
                return new ViewCell
                {
                    View = cellGrid
                };
            });
            productList.ItemSelected += ProductList_ItemSelected;

            Grid titleGrid = new Grid();
            titleGrid.BackgroundColor = Color.White;
            titleGrid.ColumnSpacing = 2;
            titleGrid.RowSpacing = 2;
            titleGrid.Children.Add(new Label
            {
                BackgroundColor = Color.FromHex("b0ff57"),
                Text = " Продукты",
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment= TextAlignment.Center,
                FontSize =
               Device.GetNamedSize(NamedSize.Medium, typeof(Label)) * 1.1
            }, 0, 2, 0, 1);
            titleGrid.Children.Add(new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("b0ff57"),
                FontAttributes = FontAttributes.Bold,
                Text = " Кол-во",
                FontSize =
               Device.GetNamedSize(NamedSize.Medium, typeof(Label)) * 1.1
            }, 2, 3, 0, 1);
            titleGrid.Children.Add(new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("b0ff57"),
                FontAttributes = FontAttributes.Bold,
                Text = " Срок годости",
                FontSize =
               Device.GetNamedSize(NamedSize.Medium, typeof(Label)) * 1.1
            }, 3, 5, 0, 1);

            ToolbarItem changeItem = new ToolbarItem
            {
                Text = "Изменить",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            changeItem.Clicked += ChangeItem_Clicked;

            Content = new StackLayout
            {
                Padding = new Thickness(1,2,1,1),
            BackgroundColor = Color.White,
            HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                   titleGrid,
                    productList
                }
            };


        }

        private async void ChangeItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditOrCreateProductPage(0));
        }

        private async void ProductList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var id = ((sender as ListView).SelectedItem as ProductView).Id;
            await Navigation.PushAsync(new EditOrCreateProductPage(id));
        }
    }
}