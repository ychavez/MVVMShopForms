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
    public class ProductItemViewModel : BaseViewModel
    {
        private Context _Context;
        public Product Product { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ProductItemViewModel(Product product = null)
        {
            Product = product ?? new Product();
            SaveCommand = new Command(Save);
            DeleteCommand = new Command(Delete);
            _Context = new Context();
        }

        private async void Save()
        {
            if (Product.Id == 0)
                await _Context.AddProduct(Product);
            else
                await _Context.UpdateProduct(Product);

            await Navigation.PopAsync();

        }

        private async void Delete()
        {

            bool answer = await Application.Current.MainPage.DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");

            await _Context.DeteProduct(Product);

            await Navigation.PopAsync();
        }

    }
}
