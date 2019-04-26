using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using StepMotorTest.Models;

namespace StepMotorTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipesPage : ContentPage
    {
        public RecipesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            recipesListView.ItemsSource = App.Database.GetRecipes();
        }

        async void RecipesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new RecipePage
                {
                    BindingContext = e.SelectedItem as Recipe
                });
            }
        }

        async void AddRecipeButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecipePage
            {
                BindingContext = new Recipe()
            });
        }
    }
}