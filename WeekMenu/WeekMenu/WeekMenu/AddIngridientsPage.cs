using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WeekMenu
{
	public class AddIngridientsPage : ContentPage
	{

        List<IngredientView> ingridients = new List<IngredientView>();

        Entry newName;
        Entry count;
        Entry unit;

        ListView ingListView = new ListView();

        int idOfDish;

		public AddIngridientsPage (int idOfDish)
		{
            Title = "Ингридиенты";
            BackgroundColor = Color.White;
            this.idOfDish = idOfDish;
            ingridients = App.Database.IngredientsList.FindAll(i => i.DishId == idOfDish).
            Select(i => new IngredientView
            {
                Id = i.Id,
                CountAndUnit = i.Count.ToString() +
            " " + App.Database.NamesOfProudcts[i.ProductNameId].Unit,
                Name = App.Database.NamesOfProudcts[i.ProductNameId].Name
            }).ToList();

            ingListView.ItemsSource = ingridients;
            ingListView.ItemTemplate = new DataTemplate(() =>
            {
                Label nameLabel = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1
                };
                nameLabel.SetBinding(Label.TextProperty, "Name");

                Label countAndUnit = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1
                };
                countAndUnit.SetBinding(Label.TextProperty, "CountAndUnit");

                Grid cellGrid = new Grid
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                cellGrid.Children.Add(nameLabel, 0, 2, 0, 1);
                cellGrid.Children.Add(countAndUnit, 2, 3, 0, 1);

                return new ViewCell
                {
                    View = cellGrid
                };
            });
            ingListView.ItemTapped += IngListView_ItemTapped;
            Label newNameLab = new Label
            {
                Text = "Наименование",
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1,
            };
            Label newIngLab = new Label
            {
                Text = "Новый игридиент",
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1.1,
            };
            newName = new Entry
            {
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Entry)) * 1,
            };
            Label countLab = new Label
            {
                Text = "Кол-во",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1
            };
            count = new Entry
            {
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Entry)) * 1,
            };
            Label unitLab = new Label
            {
                Text = "Ед. измерения",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1
            };
            unit = new Entry
            {
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Entry)) * 1,
            };

            Button ok = new Button
            {
                Text = "ОК"
            };
            ok.Clicked += Ok_Clicked;
            Button newIng = new Button
            {
                Text = "ДОБАВИТЬ НОВЫЙ ИНГРЕДИЕНТ"
            };
            newIng.Clicked += NewIng_Clicked;

            Grid titleGrid = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
                ColumnSpacing = 2,
                RowSpacing = 2
            };

            titleGrid.Children.Add(new Label
            {
                BackgroundColor = Color.FromHex("c3fdff"),
                Text = " Продукт",
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
            Content = new StackLayout
            {
                Padding = new Thickness(2, 2, 2, 2),
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    //titleGrid
                    ingListView,
                    newNameLab,
                    newName,
                    countLab,
                    count,
                    unitLab,
                    unit,
                    newIng,
                    ok
                }
            };
        }

        private async void Ok_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void IngListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
            if (!await DisplayAlert("Удаление", "Вы уверены, что хотите удалить этот ингредиент?",
                "Да", "Нет"))
            {
                return;
            }
            var id = (e.Item as IngredientView).Id;
            App.Database.Database.Delete<Ingredient>(id);
            App.Database.IngredientsList.RemoveAll(i => i.Id == id);
            ingridients.RemoveAll(i => i.Id == id);
            Refresh();
            await DisplayAlert("Удаление", "Ингредиент успешно удалён", "Ок");
        }

        public void Refresh()
        {
            ingListView.ItemsSource = null;
            ingListView.ItemsSource = ingridients;
        }

        private async void NewIng_Clicked(object sender, EventArgs e)
        {
            if (newName.Text == "" || count.Text == "" || unit.Text == "")
            {
                await DisplayAlert("Ошибка", "Все поля должны быть заполнены", "Ок");
                return;
            }

            if (!double.TryParse(count.Text, out var tmp))
            {
                await DisplayAlert("Ошибка", "В поле кол-во должно находиться число", "Ок");
                return;
            }

            ProductName nameOfP;

            if (App.Database.NamesOfProudcts.Count(p => p.Value.Name == newName.Text) != 0)
            {
                nameOfP = App.Database.NamesOfProudcts.
                    First(p => p.Value.Name == newName.Text).Value;
                if (nameOfP.Unit != unit.Text)
                {
                    if (!await DisplayAlert("Предупреждение!", "Вы изменили единицу измерения для \""
                        + newName.Text + "\". Если вы продолжите, она изменится" +
                        " для всех продуктов с таким названием!", "Продолжить", "Отменить"))
                        return;
                    else
                    {
                        App.Database.NamesOfProudcts[nameOfP.Id].Unit = unit.Text;
                        App.Database.Database.Update(App.Database.NamesOfProudcts[nameOfP.Id]);
                    }
                }
            }
            else
            {
                nameOfP = new ProductName { Id = 0, Name = newName.Text, Unit = unit.Text };
                App.Database.Database.Insert(nameOfP);
                App.Database.NamesOfProudcts[nameOfP.Id] = nameOfP;
            }

            var i = new Ingredient
            {
                Id = 0,
                Count = double.Parse(count.Text),
                DishId = idOfDish,
                ProductNameId = nameOfP.Id
            };
            App.Database.Database.Insert(i);
            App.Database.IngredientsList.Add(i);
            ingridients.Add(new IngredientView
            {
                Id = idOfDish,
                Name = nameOfP.Name,
                CountAndUnit = i.Count.ToString() + " " + nameOfP.Unit
            });
            Refresh();
        }
    }
}