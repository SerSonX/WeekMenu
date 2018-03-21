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

		public DayMenuPage (int dayOfWeek)
		{
            BackgroundColor = Color.White;
            this.dayOfWeek = dayOfWeek;
            Title = dayByNumber(dayOfWeek);
			Content = new StackLayout {
				Children = {
					new Label { Text = "Welcome to Xamarin.Forms!" }
				}
			};
		}
	}
}