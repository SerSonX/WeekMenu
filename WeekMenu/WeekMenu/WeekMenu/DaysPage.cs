using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WeekMenu
{
	public class DaysPage : ContentPage
	{
        class DayOfWeek
        {
            public string Day { get; set; }

            public DayOfWeek(string day)
            {
                Day = day;
            }
        }
        static DayOfWeek[] daysOfWeek =
            { new DayOfWeek("Понедельник"), new DayOfWeek("Вторник"), new DayOfWeek("Среда"),
            new DayOfWeek("Четверг"), new DayOfWeek("Пятница"), new DayOfWeek("Суббота"),
            new DayOfWeek("Воскресенье") };
        ListView listView = new ListView
        {
            ItemsSource = daysOfWeek
        };

        public DaysPage ()
		{
            BackgroundColor = Color.White;
            listView.ItemTemplate = new DataTemplate(() =>
            {
                Label dayLabel = new Label();
                dayLabel.SetBinding(Label.TextProperty, "Day");
                dayLabel.FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label))*1.2;
                return new ViewCell
                {
                    View = new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Children =
                         {
                            dayLabel
                         }
                    }
                };
            });
            listView.ItemSelected += ListView_ItemSelected;
            Title = "Меню на неделю";
            Content = new StackLayout {
                Children = {
                    listView
                }
			};
		}

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var s = ((sender as ListView).SelectedItem as DayOfWeek).Day;
            int i=0;
            switch (s)
            {
                case "Понедельник":
                    i = 1;
                    break;
                case "Вторник":
                    i = 2;
                    break;
                case "Среда":
                    i = 3;
                    break;
                case "Четверг":
                    i = 4;
                    break;
                case "Пятница":
                    i = 5;
                    break;
                case "Суббота":
                    i = 6;
                    break;
                case "Воскресенье":
                    i = 7;
                    break;
            }
            await Navigation.PushAsync(new DayMenuPage(i));

        }
    }
}