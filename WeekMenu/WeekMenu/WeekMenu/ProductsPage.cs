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
                Label expirationDateLabel = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1
                };
                expirationDateLabel.SetBinding(Label.TextProperty, "ExpirationDate");
                expirationDateLabel.SetBinding(Label.BackgroundColorProperty, "Good");
                //BoxView good = new BoxView();
                //good.SetBinding(BoxView.ColorProperty, "Good");

                Grid cellGrid = new Grid
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                cellGrid.Children.Add(nameLabel, 0, 2, 0, 1);
                cellGrid.Children.Add(countAndUnitLabel, 2, 3, 0, 1);
                cellGrid.Children.Add(expirationDateLabel, 3, 5, 0, 1);
                //cellGrid.Children.Add(good, 5, 6, 0, 1);
                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Children = {
                            cellGrid 
                    }
                    }
                };
            });

            productList.ItemTapped += ProductList_ItemTapped;

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
            titleGrid.Children.Add(new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("c3fdff"),
                FontAttributes = FontAttributes.Bold,
                Text = " Срок годости",
                FontSize =
               Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1.1
            }, 3, 5, 0, 1);

            ToolbarItem addItem = new ToolbarItem
            {
                Text = "ДОБАВИТЬ",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            addItem.Clicked += AddItem_Clicked;
            ToolbarItems.Add(addItem);

            Content = new StackLayout
            {
                Padding = new Thickness(1, 2, 1, 1),
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                   //titleGrid,
                    productList
                }
            };


        }

        public void Refresh()
        {
            productList.ItemsSource = null;
            productList.ItemsSource = App.Database.ProductsViewList;
        }

        private async void ProductList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
            var id = (e.Item as ProductView).Id;
            var edPage = new EditOrCreateProductPage(id);
            edPage.Changed += EdPage_Changed;
            await Navigation.PushAsync(edPage);
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            var edPage = new EditOrCreateProductPage(0);
            edPage.Changed += EdPage_Changed;
            await Navigation.PushAsync(edPage);
        }

        private void EdPage_Changed(object sender)
        {
            Refresh();
        }
    }
}