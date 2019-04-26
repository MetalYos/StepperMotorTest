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
    public partial class MovePage : ContentPage
    {
        public MovePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void SaveMoveButton_Clicked(object sender, EventArgs e)
        {
            Move updatedMove = (BindingContext as Move);
            if (App.Database.Contains(updatedMove.RecipeID))
            {
                Recipe recipe = App.Database.GetRecipe(updatedMove.RecipeID);
                foreach (var move in recipe.Moves)
                {
                    if (move.ID == updatedMove.ID)
                    {
                        int index = recipe.Moves.IndexOf(move);
                        recipe.Moves.Insert(index, updatedMove);
                        recipe.Moves.Remove(move);
                        await Navigation.PopAsync();
                    }
                }
                recipe.Moves.Add(updatedMove);
            }
            else
            {
                await DisplayAlert("Error", "Recipe does not exist in the Database! Can't delete it.", "Ok");
            }
        }

        async void DeleteMoveButton_Clicked(object sender, EventArgs e)
        {
            Move currentMove = (BindingContext as Move);
            if (App.Database.Contains(currentMove.RecipeID))
            {
                Recipe recipe = App.Database.GetRecipe(currentMove.RecipeID);
                foreach (var move in recipe.Moves)
                {
                    if (move.ID == currentMove.ID)
                    {
                        recipe.Moves.Remove(move);
                        await Navigation.PopAsync();
                    }
                }
                await DisplayAlert("Error", "Move does not exist in the Recipe! Can't delete it.", "Ok");
            }
            else
            {
                await DisplayAlert("Error", "Recipe does not exist in the Database! Can't delete it.", "Ok");
            }
        }

        async void CancelMoveButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void DistanceStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            distanceLabel.Text = string.Format("{0:F1} mm", e.NewValue);
        }
    }
}