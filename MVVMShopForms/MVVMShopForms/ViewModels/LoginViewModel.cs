using MVVMShopForms.Data;
using MVVMShopForms.Models;
using MVVMShopForms.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVMShopForms.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private Context _Context;
        public Login User { get; set; }
        public ICommand Login { get; set; }

        public LoginViewModel()
        {
            User = new Login();
            _Context = new Context();
            Login = new Command(login);
        }
        private async void login()
        {
            IsBusy = true;
            string Token = await _Context.Login(User);
            if (Token == "")
            {
                await Application.Current.MainPage.DisplayAlert("Datos no validos", "Usuario no valido, intente de nuevo", "OK");
                User = new Login();
                IsBusy = false;
                return;
            }
            Application.Current.Properties["token"] = Token; 
            Globals.ServiceApiKey = Token;
            await Application.Current.SavePropertiesAsync();
            Application.Current.MainPage = new MainPage();
            IsBusy = false;
        }
    }
}
