using MVVMShopForms.Models;
using MVVMShopForms.ViewModels;
using Plugin.Media;
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
    public partial class ProductItemView : ContentPage
    {
        private ProductItemViewModel viewModel;
        public ProductItemView(Product product = null)
        {
            InitializeComponent();
            BindingContext = viewModel = new ProductItemViewModel(product) { Navigation = Navigation };
            btnDelete.IsEnabled = (product != null);
        }
       
    }
}