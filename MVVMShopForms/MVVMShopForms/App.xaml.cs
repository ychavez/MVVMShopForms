using MVVMShopForms.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVMShopForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Plugin.Media.CrossMedia.Current.Initialize();

            MainPage = new NavigationPage(new Login());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
