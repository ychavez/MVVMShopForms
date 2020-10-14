using MVVMShopForms.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMShopForms.Models
{
   public class Login : BaseModel
    {
        private string   _User;
        public string User
        {
            get => _User;
            set => SetProperty(ref _User, value);

        }
        private string _Password;
        public string Password
        {
            get => _Password;
            set => SetProperty(ref _Password, value);

        }
    }
}
