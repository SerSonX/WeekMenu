using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WeekMenu
{
	public class EditOrCreateProductPage : ContentPage
	{
        int idOfProduct;
		public EditOrCreateProductPage (int idOfProduct)
		{
            this.idOfProduct = idOfProduct;
            Entry nameEnt, unitEnt, countEnt, dateEnt;
            Label nameLab = new Label()
            {
                Text = "Наименование"
            };
            Label unitLab = new Label()
            {
                Text = "Величина"
            };
            Label countLab = new Label
            {
                Text = "Кол-во"
            };
            Label dateLab = new Label
            {
                Text = "Срок годности"
            };
            if (idOfProduct == 0)
            {
                nameEnt = new Entry
                {
                    Placeholder = "Пожалуйста, введите наименование продукта"
                };
            }
            var a = App.Database.ProductsList.Find(p => p.Id == idOfProduct);
            Content = new StackLayout {
				Children = {
					new Label { Text = "Welcome to Xamarin Forms!" }
				}
			};
		}
	}
}