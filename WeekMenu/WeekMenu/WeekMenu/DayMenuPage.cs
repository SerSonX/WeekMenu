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

        public Label[] typesOfMeal = new Label[]
        {
            new Label{Text = "Завтрак" }, new Label {Text = "Второй завтрак"},
            new Label {Text = "Обед"}, new Label {Text = "Полдник"},
            new Label {Text = "Ужин"}, new Label {Text = "Перекус" }
        };

        private ListView[] dayMenuList = new ListView[6];

        private List<DishesInDayView>[] dishesInDayViewList;
        public List<DishesInDayView>[] DishesInDayViewList
        {
            get
            {
                if (dishesInDayViewList == null)
                {
                    dishesInDayViewList = new List<DishesInDayView>[6];
                    for (int i = 0; i < 6; i++)
                    {
                        dishesInDayViewList[i] = App.Database.DaysAndDishesList.
                            Where(d => d.Day == dayOfWeek && d.Type == i).
                            Select(d => new DishesInDayView
                            {
                                Id = d.DishId,
                                Name = App.Database.DishesList.First(q => q.Id==d.DishId).Name,
                                Type = d.Type
                            }).ToList();
                    }
                }
                return dishesInDayViewList;
            }
        }

        public DayMenuPage (int dayOfWeek)
		{
            BackgroundColor = Color.White;
            this.dayOfWeek = dayOfWeek;
            Title = dayByNumber(dayOfWeek);

            for (int i = 0; i < 6; i++)
            {
                dayMenuList[i] = new ListView();
                dayMenuList[i].ItemsSource = DishesInDayViewList[i];
                dayMenuList[i].ItemTemplate = new DataTemplate(() =>
                {
                    Label nameLabel = new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1.2
                    };
                    nameLabel.SetBinding(Label.TextProperty, "Name");

                    Label timeLabel = new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1.2
                    };
                    timeLabel.SetBinding(Label.TextProperty, "Time");

                    Grid cellGrid = new Grid
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand
                    };
                    cellGrid.Children.Add(nameLabel, 0, 2, 0, 1);
                    cellGrid.Children.Add(timeLabel, 2, 4, 0, 1);

                    return new ViewCell
                    {
                        View = cellGrid
                    };
                });
                dayMenuList[i].ItemTapped += DayMenuList_ItemTapped;
            }
            Grid titleGrid = new Grid()
            {
                BackgroundColor = Color.White,
                ColumnSpacing = 2,
                RowSpacing = 2
            };

            titleGrid.Children.Add(new Label
            {
                BackgroundColor = Color.FromHex("c3fdff"),
                Text = " Блюдо",
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize =
               Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1.2
            }, 0, 2, 0, 1);

            titleGrid.Children.Add(new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("c3fdff"),
                FontAttributes = FontAttributes.Bold,
                Text = " Время",
                FontSize =
               Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1.2
            }, 2, 3, 0, 1);
         
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
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            content.Children.Add(titleGrid);
            for (int i = 0; i < 6; i++)
            {
                content.Children.Add(typesOfMeal[i]);
                content.Children.Add(dayMenuList[i]);
            }
            Content = content;
        }

        private async void ListOfProductItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HaventProducts());
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
            for (int i = 0; i < 6; i++)
            {
                dishesInDayViewList[i] = App.Database.DaysAndDishesList.
                    Where(d => d.Day == dayOfWeek && d.Type == i).
                    Select(d => new DishesInDayView
                    {
                        Id = d.DishId,
                        Name = App.Database.DishesList.First(q => q.Id == d.DishId).Name,
                        Type = d.Type
                    }).ToList();
                dayMenuList[i].ItemsSource = null;
                dayMenuList[i].ItemsSource = DishesInDayViewList[i];
            }
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