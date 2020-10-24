using MVVMShopForms.Data;
using MVVMShopForms.Models;
using MVVMShopForms.View;
using MVVMShopForms.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVMShopForms.ViewModels
{
    class ProductsViewModel : BaseViewModel
    {
        public ICommand AddCommand { get; set; }
        public ICommand Refresh { get; set; }
        private Context _Context;
        public ProductsViewModel()
        {
            _Context = new Context(Globals.ServiceApiKey);
            LoadProducts();
            AddCommand = new Command(Add);
            Refresh = new Command(LoadProducts);
        }
        private ObservableCollection<Product> _Products;
        public ObservableCollection<Product> Products { get => _Products; set { SetProperty(ref _Products, value); } }
        public async void LoadProducts()
        {
            IsBusy = true;
            Products = new ObservableCollection<Product>(await _Context.GetProducts());
            IsBusy = false;
        }
        public void Add() => Navigation.PushAsync(new ProductItemView());
        
    }
}
