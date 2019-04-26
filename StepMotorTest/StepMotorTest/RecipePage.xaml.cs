using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using StepMotorTest.Models;
using StepMotorTest.Data;

namespace StepMotorTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipePage : ContentPage
    {
        public RecipePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void MovesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new MovePage
                {
                    BindingContext = (e.SelectedItem as Move)
                });
            }
        }

        async void AddMoveButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MovePage
            {
                BindingContext = new Move((BindingContext as Recipe).ID)
            });
        }

        async void SaveRecipeButton_Clicked(object sender, EventArgs e)
        {
            Recipe recipe = new Recipe();
            recipe.Name = entryRecipeName.Text;
            recipe.Moves = (movesListView.ItemsSource as List<Move>);

            if (App.Database.Contains(recipe.ID))
                App.Database.UpdateRecipe(recipe);
            else
                App.Database.SaveRecipe(recipe);

            await Navigation.PopAsync();
        }

        async void DeleteRecipeButton_Clicked(object sender, EventArgs e)
        {
            Recipe recipe = (BindingContext as Recipe);
            if (App.Database.Contains(recipe.ID))
            {
                App.Database.DeleteRecipe(recipe.ID);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Recipe does not exist in the Database! Can't delete it.", "Ok");
            }
        }

        async void CancelRecipeButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void GoRecipeButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}