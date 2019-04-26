using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using StepMotorTest.Data;

namespace StepMotorTest
{
    public partial class App : Application
    {
        static RecipesDatabase database = null;

        public static RecipesDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new RecipesDatabase(Path.Combine(Environment
                        .GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Recipes.xml"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
