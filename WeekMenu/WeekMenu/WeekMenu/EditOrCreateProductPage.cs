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
        Entry nameEnt = new Entry
        {
            FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1,
        };
        Entry unitEnt = new Entry
        {
            FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1,
        };
        Entry countEnt = new Entry
        {
            FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1,
        };
        Entry dateEnt = new Entry
        {
            FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1,
        };
        Label nameLab = new Label()
        {
            FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1,
            Text = "Наименование"
        };
        Label unitLab = new Label()
        {
            FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1,
            Text = "Величина"
        };
        Label countLab = new Label
        {
            FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1,
            Text = "Кол-во"
        };
        Label dateLab = new Label
        {
            FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1,
            Text = "Срок годности"
        };
        public EditOrCreateProductPage (int idOfProduct)
		{
            this.idOfProduct = idOfProduct;
            
            if (idOfProduct != 0)
            {
            var pr = App.Database.ProductsList.Find(p => p.Id == idOfProduct);
                nameEnt.Text = App.Database.NamesOfProudcts[pr.NameId].Name;
                unitEnt.Text = App.Database.NamesOfProudcts[pr.NameId].Unit;
                countEnt.Text = pr.Count.ToString();
                dateEnt.Text = pr.ExpirationDate;
            }
            Button ok = new Button
            {
                Text = "Принять"
            };
            ok.Clicked += Ok_Clicked;

            Button cancel = new Button
            {
                Text = "Отменить"
            };
            cancel.Clicked += Cancel_Clicked;
            Content = new StackLayout {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(2, 2, 2, 2),
                Children = {
					nameLab, nameEnt, countLab,countEnt,unitLab,unitEnt, dateLab,dateEnt,
                    new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.End,
                        Orientation = StackOrientation.Horizontal,
                        Children = {ok,cancel}
                    }
				}
			};
		}

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Ok_Clicked(object sender, EventArgs e)
        {
            if (nameEnt.Text=="" || dateEnt.Text=="" || countEnt.Text=="" || unitEnt.Text=="")
            {
                await DisplayAlert("Ошибка", "Все поля должны быть заполнены", "Ок");
                return;
            }
           
            if (App.Database.NamesOfProudcts.Count(p => p.Value.Name == nameEnt.Text)!=0)
            {
                var tmp = App.Database.NamesOfProudcts.First(p => p.Value.Name == nameEnt.Text);
                if (tmp.Value.Unit!=unitEnt.Text)
                {
                    await DisplayAlert("Предупреждение", "Вы изменили единицу измерения для этого продукта. Если вы продолжите, она изменится для всех таких же продуктов, а так же в Меню", "Вы хотите продолжить?", "Продолжить", "Отменить" );
                }
                if (idOfProduct == 0)
            {

                App.Database.Database.Insert();
            }
                Database.Update(item);
                    return item.Id;
                else
                {
                }
            }
            await Navigation.PopAsync();

        }
    }
}