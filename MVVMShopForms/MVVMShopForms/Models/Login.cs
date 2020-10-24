﻿using MVVMShopForms.Models.Base;

namespace MVVMShopForms.Models
{
    public class Login : BaseModel
    {
        private string _Email;
        public string Email { get => _Email; set => SetProperty(ref _Email, value); }
        private string _Password;
        public string Password { get => _Password; set => SetProperty(ref _Password, value); }
    }
}
