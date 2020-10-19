using MVVMShopForms.Data;
using MVVMShopForms.Models;
using MVVMShopForms.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
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
            User =  new Login();
            _Context = new Context();
            Login = new Command(login);
        }
        private async void login()
        {
            string Token = await _Context.Login(User);
            if (Token == "")
            {
                await Application.Current.MainPage.DisplayAlert("Datos no validos", "Usuario no valido, intente de nuevo", "OK");
                User = new Login();
                return;
            }

            Application.Current.Properties["token"] = Token;
            Application.Current.MainPage  = new MainPage();
        }
    }
}
