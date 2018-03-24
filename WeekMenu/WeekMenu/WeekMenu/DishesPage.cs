using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WeekMenu
{
	public class DishesPage : ContentPage
	{
        public delegate void EventHandler(object sender);
        public event EventHandler Changed;

        private ListView listView = new ListView();

        int dayOfWeek;

        public DishesPage (int dayOfWeek)
		{
            this.dayOfWeek = dayOfWeek;
            BackgroundColor = Color.White;
            Title = "Блюда";
            listView.ItemsSource = App.Database.DishesList;
            listView.ItemTemplate = new DataTemplate(() =>
            {
                Label nameLabel = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1
                };
                nameLabel.SetBinding(Label.TextProperty, "Name");

                Grid cellGrid = new Grid
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                cellGrid.Children.Add(nameLabel, 0, 2, 0, 1);

                return new ViewCell
                {
                    View = cellGrid
                };
            });
            listView.ItemTapped += ListView_ItemTapped;

            Button addDish = new Button
            {
                Text = "ДОБАВИТЬ НОВОЕ БЛЮДО",
            };
            addDish.Clicked += AddDish_Clicked;

            Content = new StackLayout
            {
                Padding = new Thickness(1, 2, 1, 1),
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                   addDish,
                   listView
                }
            };
        }

        int mealToInt(string s)
        {
            switch (s)
            {
                case "Завтрак":
                    return 0;
                case "Второй завтрак":
                    return 1;
                case "Обед":
                    return 2;
                case "Полдник":
                    return 3;
                case "Ужин":
                    return 4;
                case "Перекус":
                    return 5;
            }
            return 5;
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
            var id = (e.Item as Dish).Id;
            string s = await DisplayActionSheet("Выберите приём пищи", "Отмена", null,
                "Завтрак", "Второй завтрак", "Обед", "Полдник", "Ужин", "Перекус");
            if (s == "Отмена")
                return;
            var dd = new DayAndDish
            {
                Id = 0,
                Day = dayOfWeek,
                DishId = id,
                Type = mealToInt(s)
            };
            App.Database.Database.Insert(dd);
            App.Database.DaysAndDishesList.Add(dd);
            Changed(this);
        }

        private async void AddDish_Clicked(object sender, EventArgs e)
        {
            var edPage = new EditOrCreateDishPage(0);
            edPage.Changed += EdPage_Changed;
            await Navigation.PushAsync(edPage);
        }

        private void EdPage_Changed(object sender)
        {
            Refresh();
        }

        public void Refresh()
        {
            listView.ItemsSource = null;
            listView.ItemsSource = App.Database.DishesList;
        }
    }
}