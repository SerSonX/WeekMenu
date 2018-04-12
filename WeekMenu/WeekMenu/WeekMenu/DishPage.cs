using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WeekMenu
{
    public class DishPage : ContentPage
    {
        public delegate void EventHandler(object sender);
        public event EventHandler Changed;
        public event EventHandler Cooked;

        List<IngredientView> ingridients;
        ListView listView = new ListView();

        private int id;

        private DayAndDish dayAndDish;

        public DishPage(int id)
        {
            this.id = id;
            BackgroundColor = Color.White;

            dayAndDish = App.Database.DaysAndDishesList.Find(d => d.Id == id);

            var dish = App.Database.DishesList.Find(d => d.Id == dayAndDish.DishId);

            Title = dish.Name;

            ingridients = App.Database.IngredientsList.FindAll(i => i.DishId == dish.Id).
                Select(i => new IngredientView { Id = i.Id, CountAndUnit = i.Count.ToString() +
                " " + App.Database.NamesOfProudcts[i.ProductNameId].Unit,
                    Name = App.Database.NamesOfProudcts[i.ProductNameId].Name }).ToList();

            listView.ItemsSource = ingridients;
            listView.IsEnabled = false;
            listView.ItemTemplate = new DataTemplate(() =>
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

            Grid titleGrid = new Grid()
            {
                BackgroundColor = Color.White,
                ColumnSpacing = 2,
                RowSpacing = 2
            };

            titleGrid.Children.Add(new Label
            {
                BackgroundColor = Color.FromHex("c3fdff"),
                Text = " Игридиент",
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

            //ToolbarItem changeItem = new ToolbarItem
            //{
            //    Text = "ИЗМЕНИТЬ",
            //    Order = ToolbarItemOrder.Primary,
            //    Priority = 1
            //};
            //changeItem.Clicked += ChangeItem_Clicked;
            //ToolbarItems.Add(changeItem);

            ToolbarItem deleteItem = new ToolbarItem
            {
                Text = "УДАЛИТЬ",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            deleteItem.Clicked += DeleteItem_Clicked;
            ToolbarItems.Add(deleteItem);

            ToolbarItem isCookedItem = new ToolbarItem
            {
                Text = "ПРИГОТОВЛЕНО",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            isCookedItem.Clicked += IsCookedItem_Clicked;
            ToolbarItems.Add(isCookedItem);

            Content = new StackLayout
            {
                Padding = new Thickness(1, 2, 1, 1),
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                   //titleGrid,
                   listView
                }
            };
        }

        private async void DeleteItem_Clicked(object sender, EventArgs e)
        {
            if (!await DisplayAlert("Удаление", "Вы уверены, что хотите удалить блюдо из меню?", "Да", "Нет"))
            {
                return;
            }
            App.Database.Database.Delete<DayAndDish>(id);
            App.Database.DaysAndDishesList.Remove(dayAndDish);
            await DisplayAlert("Удаление", "Блюдо успешно удалено", "Ок");
            Changed(this);
            await Navigation.PopAsync();
        }

        private void IsCookedItem_Clicked(object sender, EventArgs e)
        {
            foreach (var i in App.Database.IngredientsList.FindAll(i => i.DishId == dayAndDish.DishId))
            {
                var count = i.Count;
                List<Product> deleted = new List<Product>();
                foreach(var j in App.Database.ProductsList.Where(p => p.NameId == i.ProductNameId
                && (Convert.ToDateTime(p.ExpirationDate) >= DateTime.Now.Date)))
                {
                    if (j.Count>count)
                    {
                        j.Count -= count;
                        App.Database.Database.Update(j);
                        App.Database.ProductsViewList.First(p => p.Id == j.Id).CountAndUnit =
                            j.Count.ToString() + " " + App.Database.NamesOfProudcts[j.NameId].Unit;
                        break;
                    }
                    else if (j.Count==count)
                    {
                        App.Database.Database.Delete(j);
                        App.Database.ProductsViewList.RemoveAll(p => p.Id == j.Id);
                        deleted.Add(j);
                        break;
                    }
                    else
                    {
                        count -= j.Count;
                        App.Database.Database.Delete(j);
                        App.Database.ProductsViewList.RemoveAll(p => p.Id == j.Id);
                        deleted.Add(j);
                    }
                    if (deleted.Count>0)
                        App.Database.productsViewList = null;
                }
                foreach (var item in deleted)
                {
                    App.Database.ProductsList.Remove(item);
                }
            }
        }

        private async void ChangeItem_Clicked(object sender, EventArgs e)
        {
            var edPage = new EditOrCreateDishPage(id);
            edPage.Changed += EdPage_Changed;
            await Navigation.PushAsync(edPage);
        }

        private void EdPage_Changed(object sender)
        {
            Refresh();
            Changed(this);
        }

        public void Refresh()
        {
            listView.ItemsSource = null;
            listView.ItemsSource = ingridients;
        }
    }
}