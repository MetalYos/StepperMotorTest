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
            if (App.Database.ContainsRecipe(updatedMove.RecipeID))
            {
                if (!App.Database.UpdateMove(updatedMove))
                    App.Database.AddMoveToRecipe(updatedMove);

                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Recipe does not exist in the Database! Can't delete it.", "Ok");
            }
        }

        async void DeleteMoveButton_Clicked(object sender, EventArgs e)
        {
            Move currentMove = (BindingContext as Move);
            if (App.Database.DeleteMove(currentMove))
                await Navigation.PopAsync();
            else
                await DisplayAlert("Error", "Recipe/Move does not exist in the Database! Can't delete it.", "Ok");
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