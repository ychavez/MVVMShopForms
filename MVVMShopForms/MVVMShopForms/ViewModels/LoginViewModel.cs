using MVVMShopForms.Data;
using MVVMShopForms.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MVVMShopForms.ViewModels
{
   public class LoginViewModel : BaseViewModel
    {
        private Context _Context;
        public ICommand Login { get; set; }

        public LoginViewModel()
        {
            _Context = new Context();
        }
    }
}
