using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using StepMotorTest.Interfaces;
using StepMotorTest.Models;

namespace StepMotorTest
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Display paired BlueTooth devices
            btDevicesListView.ItemsSource = null;
            try
            {
                btDevicesListView.ItemsSource = DependencyService.Get<IBluetoothHelper>().GetPairedDevices();
            }
            catch (Exception e)
            {
                DisplayAlert(e.Source, e.Message, "Ok");
                btDevicesListView.ItemsSource = new List<MyBluetoothDevice>();
            }

        }

        async void OnRecipesButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecipesPage());
        }

        async void BtDevicesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Connect to the selected device
            if (e.SelectedItem != null)
            {
                try
                {
                    string name = e.SelectedItem as string;
                    if (await DependencyService.Get<IBluetoothHelper>().Connect(name))
                        await DisplayAlert("Connection", "Connection to " + name + " was established.", "Ok");
                    else
                        await DisplayAlert("Connection", "Could not connect to " + name + ".", "Ok");
                }
                catch (Exception exp)
                {
                    await DisplayAlert(exp.Source, exp.Message, "Ok");
                }
            }
        }
    }
}
