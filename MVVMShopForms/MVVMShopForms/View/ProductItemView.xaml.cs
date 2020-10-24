using MVVMShopForms.Models;
using MVVMShopForms.ViewModels;

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