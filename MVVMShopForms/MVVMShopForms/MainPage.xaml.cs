using MVVMShopForms.Models;
using MVVMShopForms.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MVVMShopForms
{
    public partial class MainPage : MasterDetailPage
    {
        public List<MainMenuItem> menuList
        {
            get;
            set;
        }
        public MainPage()
        {
            InitializeComponent();
            menuList = new List<MainMenuItem>();

            menuList.Add(new MainMenuItem()
            {
                Title = "Productos",
                Icon = "logo_aureapng",
                TargetType = typeof(ProductsView)
            });
            menuList.Add(new MainMenuItem()
            {
                Title = "Contact",
                Icon = "logo_aureapng",
                TargetType = typeof(ProductsView)
            });
          
            navigationDrawerList.ItemsSource = menuList;  
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ProductsView)));
        }
      
        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MainMenuItem)e.SelectedItem;
            Type page = item.TargetType;
            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
    }
}

