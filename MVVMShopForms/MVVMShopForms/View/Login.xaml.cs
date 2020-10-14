using MVVMShopForms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVMShopForms.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private LoginViewModel viewmodel;
        public Login()
        {
            InitializeComponent();
            BindingContext = viewmodel = new LoginViewModel() { Navigation = Navigation };

        }
    }
}