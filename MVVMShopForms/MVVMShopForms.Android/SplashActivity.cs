using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMShopForms.Droid
{
    [Activity(Label = "Aurea", Icon = "@drawable/logo_aureaIco", Theme = "@style/SplashTheme", MainLauncher = true, NoHistory =true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }
        protected override void OnResume()
        {
            base.OnResume();

            Task startup = new Task(StartUp);
            startup.Start();
        }
        async void StartUp()
        {
            await Task.Delay(500);
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

    }
}