using MVVMShopForms.ViewModels;

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