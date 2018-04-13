using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Xamarin.Forms;

namespace WeekMenu
{
	public class EditOrCreateProductPage : ContentPage
	{
        public delegate void EventHandler(object sender);
        public event EventHandler Changed;
        int idOfProduct;
        Entry nameEnt = new Entry
        {
            FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Entry)) * 1,
        };
        Entry unitEnt = new Entry
        {
            FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Entry)) * 1,
        };
        Entry countEnt = new Entry
        {
            FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Entry)) * 1,
        };
        Entry dateEnt = new Entry
        {
            FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Entry)) * 1,
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
            if (idOfProduct == 0)
                Title = "Добавление";
            else
                Title = "Изменение";
            BackgroundColor = Color.White;
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
                Text = "ПРИНЯТЬ"
            };
            ok.Clicked += Ok_Clicked;

            Button cancel = new Button
            {
                Text = "ОТМЕНИТЬ"
            };
            cancel.Clicked += Cancel_Clicked;

            ToolbarItem deleteItem = new ToolbarItem
            {
                Text = "УДАЛИТЬ",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            deleteItem.Clicked += DeleteItem_Clicked;
            ToolbarItems.Add(deleteItem);

            Content = new StackLayout {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(2, 2, 2, 2),
                Children = {
					nameLab, nameEnt, countLab,countEnt,unitLab,unitEnt, dateLab,dateEnt,
                    new StackLayout
                    {
                        Padding = new Thickness(2, 2, 2, 2),
                        HorizontalOptions = LayoutOptions.End,
                        Orientation = StackOrientation.Horizontal,
                        Children = {ok,cancel}
                    }
				}
			};
		}

        private async void DeleteItem_Clicked(object sender, EventArgs e)
        {
            if (!await DisplayAlert("Удаление", "Вы уверены, что хотите удалить продукт?", "Да", "Нет"))
            {
                return;
            }
            if (idOfProduct != 0)
            {
                App.Database.Database.Delete<Product>(idOfProduct);
                App.Database.ProductsList.RemoveAll(p => p.Id == idOfProduct);
                App.Database.ProductsViewList.RemoveAll(p => p.Id == idOfProduct);
                Changed(this);
            }
            await DisplayAlert("Удаление", "Продукт успешно удалён", "Ок");
            await Navigation.PopAsync();
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

            double count;
            if (!double.TryParse(countEnt.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out count) &&
                !double.TryParse(countEnt.Text, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out count) &&
                !double.TryParse(countEnt.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out count))
            {
                await DisplayAlert("Ошибка", "В поле кол-во должно находиться число", "Ок");
                return;
            }

            DateTime tmpd;
            if (!DateTime.TryParse(dateEnt.Text, out tmpd))
            {
                await DisplayAlert("Ошибка", "В поле срок годности должна находиться дата", "Ок");
                return;
            }

            ProductName nameOfP;

            if (App.Database.NamesOfProudcts.Count(p => p.Value.Name == nameEnt.Text) != 0)
            {
                nameOfP = App.Database.NamesOfProudcts.First(p => p.Value.Name == nameEnt.Text).Value;
                if (nameOfP.Unit != unitEnt.Text)
                {
                    if (!await DisplayAlert("Предупреждение!", "Вы изменили единицу измерения для \"" + nameEnt.Text +
                        "\". Если вы продолжите, она изменится для всех продуктов с таким названием!", "Продолжить", "Отменить"))
                        return;
                    else
                    {
                        App.Database.NamesOfProudcts[nameOfP.Id].Unit= unitEnt.Text;
                        App.Database.Database.Update(App.Database.NamesOfProudcts[nameOfP.Id]);
                    }
                }
            }
            else
            {
                nameOfP = new ProductName { Id = 0, Name = nameEnt.Text, Unit = unitEnt.Text };
                App.Database.Database.Insert(nameOfP);
                App.Database.NamesOfProudcts[nameOfP.Id] = nameOfP;
            }

            if (idOfProduct == 0)
            {
                Product prod = new Product
                {
                    Id = 0,
                    Count = count,
                    NameId = nameOfP.Id,
                    ExpirationDate = dateEnt.Text
                };
                App.Database.Database.Insert(prod);
                App.Database.ProductsList.Add(prod);
                App.Database.ProductsViewList.Add(new ProductView
                {
                    Id = prod.Id,
                    CountAndUnit = prod.Count.ToString() + " " + nameOfP.Unit,
                    Name = nameOfP.Name,
                    ExpirationDate = prod.ExpirationDate,
                    Good = (Convert.ToDateTime(prod.ExpirationDate) >= DateTime.Now.Date ?
                        Color.FromHex("75ff7a") : Color.FromHex("ff6666"))
                });
            }
            else
            {
                var prod = App.Database.ProductsList.First(p => p.Id == idOfProduct);
                prod.NameId = nameOfP.Id;
                prod.Count = count;
                prod.ExpirationDate = dateEnt.Text;
                App.Database.Database.Update(prod);
                var prV = App.Database.ProductsViewList.First(p => p.Id == prod.Id);
                prV.Name = nameOfP.Name;
                prV.ExpirationDate = prod.ExpirationDate;
                prV.CountAndUnit = prod.Count.ToString() + " " + nameOfP.Unit;
                prV.Good = (Convert.ToDateTime(prod.ExpirationDate) >= DateTime.Now.Date ?
                        Color.FromHex("75ff7a") : Color.FromHex("ff6666"));
            }
            Changed(this);
            await Navigation.PopAsync();
        }
    }
}