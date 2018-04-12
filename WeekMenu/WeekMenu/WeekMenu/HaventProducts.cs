using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WeekMenu
{
	public class HaventProducts : ContentPage
	{
        int dayOfWeek;
		public HaventProducts (int dayOfWeek)
		{
            this.dayOfWeek = dayOfWeek;
            BackgroundColor = Color.White;
            Title = "Недостающие продукты";
            //Dictionary<ProductNameId, Count>
            Dictionary<int, double> neededIngredients = new Dictionary<int, double>();

            foreach (var dishId in App.Database.DaysAndDishesList.
                                   Where(d => d.Day == dayOfWeek).
                                   Select(d => d.DishId))
            {
                foreach (var ing in App.Database.IngredientsList.FindAll(i => i.DishId == dishId))
                {
                    if (neededIngredients.ContainsKey(ing.ProductNameId))
                        neededIngredients[ing.ProductNameId] += ing.Count;
                    else
                        neededIngredients[ing.ProductNameId] = ing.Count;
                }
            }
            foreach (var p in App.Database.ProductsList)
            {
                if (Convert.ToDateTime(p.ExpirationDate) >= DateTime.Now.Date
                    && neededIngredients.ContainsKey(p.NameId))
                    neededIngredients[p.NameId] -= p.Count;
            }

            List<IngredientView> haventIngridients = neededIngredients.Where(i=> i.Value > 0).
                Select(i => new IngredientView
                {
                    Name = App.Database.NamesOfProudcts[i.Key].Name,
                    CountAndUnit = i.Value.ToString() + " " +
                    App.Database.NamesOfProudcts[i.Key].Unit
                }).ToList();

            ListView haventProductsView = new ListView();
            haventProductsView.ItemsSource = haventIngridients;
            haventProductsView.ItemTemplate = new DataTemplate(() =>
            {
                Label nameLabel = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1
                };
                nameLabel.SetBinding(Label.TextProperty, "Name");

                Label countAndUnitLabel = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)) * 1
                };
                countAndUnitLabel.SetBinding(Label.TextProperty, "CountAndUnit");

                Grid cellGrid = new Grid
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                cellGrid.Children.Add(nameLabel, 0, 2, 0, 1);
                cellGrid.Children.Add(countAndUnitLabel, 2, 3, 0, 1);

                return new ViewCell
                {
                    View = cellGrid
                };
            });
            haventProductsView.IsEnabled = false;
            
            Content = new StackLayout
            {
                Padding = new Thickness(1, 2, 1, 1),
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    haventProductsView
                }
            };
        }
	}
}