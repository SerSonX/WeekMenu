using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WeekMenu
{
	public class DayMenuPage : ContentPage
	{
        int dayOfWeek;

        private ListView dayMenuList;

        private List<DishesInDayView> dishesInDayViewList;
        public List<DishesInDayView> DishesInDayViewList
        {
            get
            {
                if (dishesInDayViewList == null)
                {
                    dishesInDayViewList = App.Database.DaysAndDishesList.
                            Where(d => d.Day == dayOfWeek).
                            Select(d => new DishesInDayView
                            {
                                Id = d.Id,
                                Name = App.Database.DishesList.First(q => q.Id==d.DishId).Name,
                                Type = d.Type
                            }).OrderBy(d => d.Type).ToList();
                }
                return dishesInDayViewList;
            }
        }


        public DayMenuPage (int dayOfWeek)
		{
            BackgroundColor = Color.White;
            this.dayOfWeek = dayOfWeek;
            Title = dayByNumber(dayOfWeek);

            dayMenuList = new ListView()
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            dayMenuList.ItemsSource = DishesInDayViewList;
            dayMenuList.ItemTemplate = new DataTemplate(() =>
            {
                Label nameLabel = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1.2
                };
                nameLabel.SetBinding(Label.TextProperty, "Name");

                Label typeLabel = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1.2
                };
                typeLabel.SetBinding(Label.TextProperty, "TypeName");

                Grid cellGrid = new Grid
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                cellGrid.Children.Add(nameLabel, 0, 2, 0, 1);
                cellGrid.Children.Add(typeLabel, 2, 4, 0, 1);

                return new ViewCell
                {
                    View = cellGrid
                };
            });
            dayMenuList.ItemTapped += DayMenuList_ItemTapped;
         
            ToolbarItem addItem = new ToolbarItem
            {
                Text = "ДОБАВИТЬ",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            addItem.Clicked += AddItem_Clicked;
            ToolbarItems.Add(addItem);

            ToolbarItem listOfProductItem = new ToolbarItem
            {
                Text = "Список недостающих продуктов",
                Order = ToolbarItemOrder.Secondary,
                Priority = 1
            };
            ToolbarItems.Add(listOfProductItem);
            listOfProductItem.Clicked += ListOfProductItem_Clicked;

            var content = new StackLayout
            {
                Padding = new Thickness(1, 2, 1, 1),
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            content.Children.Add(dayMenuList);
            Content = content;
        }

        private async void ListOfProductItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HaventProducts(dayOfWeek));
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            var dPage = new DishesPage(dayOfWeek);
            dPage.Changed += DishPage_Changed;
            await Navigation.PushAsync(dPage);
        }

        private async void DayMenuList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
            var id = (e.Item as DishesInDayView).Id;
            var dishPage = new DishPage(id);
            dishPage.Changed += DishPage_Changed;
            //dishPage.Cooked += DishPage_Cooked;
            await Navigation.PushAsync(dishPage);
        }

        private void DishPage_Cooked(object sender)
        {
        }

        private void DishPage_Changed(object sender)
        {
            Refresh();
        }

        void Refresh()
        {
            dishesInDayViewList = App.Database.DaysAndDishesList.
                            Where(d => d.Day == dayOfWeek).
                            Select(d => new DishesInDayView
                            {
                                Id = d.Id,
                                Name = App.Database.DishesList.First(q => q.Id == d.DishId).Name,
                                Type = d.Type
                            }).OrderBy(d => d.Type).ToList();
            dayMenuList.ItemsSource = null;
            dayMenuList.ItemsSource = DishesInDayViewList;
        }

        string dayByNumber(int number)
        {
            switch (number)
            {
                case 1:
                    return "Понедельник";
                case 2:
                    return "Вторник";
                case 3:
                    return "Среда";
                case 4:
                    return "Четверг";
                case 5:
                    return "Пятница";
                case 6:
                    return "Суббота";
                case 7:
                    return "Воскресенье";
            }
            throw new ArgumentException("Argument must be from 1 to 7");
        }
    }
}