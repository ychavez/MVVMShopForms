using MVVMShopForms.Data;
using MVVMShopForms.Models;
using MVVMShopForms.View;
using MVVMShopForms.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVMShopForms.ViewModels
{
    class ProductsViewModel : BaseViewModel
    {

        private Context _Context;
        public ProductsViewModel()
        {
            _Context = new Context();
            LoadProducts();
            AddCommand = new Command(Add);
        }
        private ObservableCollection<Product> _Products;
        public ObservableCollection<Product> Products
        {
            get => _Products;
            
            set
            {

                SetProperty(ref _Products, value);
            }
        }
        public ICommand AddCommand { get; set; }

        public async void LoadProducts()
        {

            Products = new ObservableCollection<Product>(await _Context.GetProducts());
        }
      
        public  void Add()
        {
            Application.Current.MainPage = new ProductItemView();

        }

    }
}
