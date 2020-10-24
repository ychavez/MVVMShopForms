using MVVMShopForms.Data;
using MVVMShopForms.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace MVVMShopForms
{
    public partial class App : Xamarin.Forms.Application
    {
            Context _Context;
        public App()
        {
         
            InitializeComponent();
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
          //  Plugin.Media.CrossMedia.Current.Initialize();
            if (Xamarin.Forms.Application.Current.Properties.ContainsKey("token"))
            {
                _Context = new Context();
                if (_Context.CheckToken(Xamarin.Forms.Application.Current.Properties["token"].ToString()))
                { 
                    MainPage = new  MainPage();
                    return;
                }
            }
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
