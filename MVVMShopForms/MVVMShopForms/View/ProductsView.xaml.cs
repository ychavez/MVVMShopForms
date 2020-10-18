using MVVMShopForms.Models;
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
    public partial class ProductsView : ContentPage
    {
        private ProductsViewModel viewModel;
        public ProductsView()
        {
            InitializeComponent();

            BindingContext = viewModel = new ProductsViewModel() { Navigation = Navigation };
        }

        private async void ProductList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            if (!(e.Item is Product item))
                return;

            await Navigation.PushAsync(new ProductItemView(item));

            ProductList.SelectedItem = null;


        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadProducts();
        }


    }
}