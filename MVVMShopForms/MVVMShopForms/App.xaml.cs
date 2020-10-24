using MVVMShopForms.Data;
using MVVMShopForms.View;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace MVVMShopForms
{
    public partial class App : Xamarin.Forms.Application
    {
        Context _Context;
        public App()
        {
            InitializeComponent();
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            if (Xamarin.Forms.Application.Current.Properties.ContainsKey("token"))
            {
                string Token = Xamarin.Forms.Application.Current.Properties["token"].ToString();
                _Context = new Context();
                if (_Context.CheckToken(Token))
                {
                    Globals.ServiceApiKey = Xamarin.Forms.Application.Current.Properties["token"].ToString();
                    MainPage = new MainPage();
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
