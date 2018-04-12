using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WeekMenu
{
    public class EditOrCreateDishPage : ContentPage
    {
        public delegate void EventHandler(object sender);
        public event EventHandler Changed;
        int idOfDish;
        Entry nameEnt = new Entry
        {
            FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Entry)) * 1,
        };
        Label nameLab = new Label()
        {
            FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1,
            Text = "Наименование"
        };

        public EditOrCreateDishPage(int idOfDish)
        {
            this.idOfDish = idOfDish;
            if (idOfDish == 0)
                Title = "Добавление";
            else
                Title = "Изменение";
            BackgroundColor = Color.White;
            if (idOfDish != 0)
            {
                var dh = App.Database.DishesList.Find(d => d.Id == idOfDish);
                nameEnt.Text = dh.Name;
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

            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(2, 2, 2, 2),
                Children = {
                    nameLab, nameEnt,
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
            if (!await DisplayAlert("Удаление", "Вы уверены, что хотите удалить блюдо?", "Да", "Нет"))
            {
                return;
            }
            if (idOfDish != 0)
            {
                App.Database.Database.Delete<Dish>(idOfDish);
                App.Database.DishesList.Remove(App.Database.DishesList.Find(d => d.Id == idOfDish));
                foreach (var d in App.Database.DaysAndDishesList.
                    Where(d => d.DishId == idOfDish))
                        App.Database.Database.Delete<DayAndDish>(d.Id);
                App.Database.DaysAndDishesList.
                    RemoveAll(d => d.DishId == idOfDish);
                foreach (var d in App.Database.IngredientsList.
                    Where(d => d.DishId == idOfDish))
                    App.Database.Database.Delete<Ingredient>(d.Id);
                App.Database.IngredientsList.
                    RemoveAll(d => d.DishId == idOfDish);
                Changed(this);
            }
            await DisplayAlert("Удаление", "Блюдо успешно удалено", "Ок");
            await Navigation.PopAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Ok_Clicked(object sender, EventArgs e)
        {
            if (nameEnt.Text == "")
            {
                await DisplayAlert("Ошибка", "Поле должно быть заполнено", "Ок");
                return;
            }

            if (idOfDish == 0)
            {
                Dish dh = new Dish
                {
                    Id = 0,
                    Name = nameEnt.Text,
                };
                App.Database.Database.Insert(dh);
                App.Database.DishesList.Add(dh);
                idOfDish = dh.Id;
            }
            else
            {
                var dh = App.Database.DishesList.First(d => d.Id == idOfDish);
                dh.Name = nameEnt.Text;
                App.Database.Database.Update(dh);
            }
            Navigation.PopAsync();
            Navigation.PushAsync(new AddIngridientsPage(idOfDish));
            Changed(this);
        }
    }
}