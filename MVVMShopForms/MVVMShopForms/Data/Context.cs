using MVVMShopForms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVVMShopForms.Data
{
    public class Context
    {
        private RestService _RestService;
        public Context() => _RestService = new RestService(Globals.ServiceUrlBase);
        public Context(string token) => _RestService = new RestService(Globals.ServiceUrlBase, token);
        public async Task<string> Login(Login user) => await _RestService.PostDataAsync<Login>(user, "Account/Login");
        public async Task<List<Product>> GetProducts() => await _RestService.GetDataAsync<Product>("products");
        public async Task AddProduct(Product product) => await _RestService.PostDataAsync<Product>(product, "products");
        public async Task UpdateProduct(Product product)
        {
            if (product.Id != 0)
                await _RestService.PutDataAsync<Product>(product, "products", product.Id);
        }
        public async Task DeteProduct(Product product)
        {
            if (product.Id != 0) { await _RestService.DeleteDataAsync("products", product.Id); }
        }
        public bool CheckToken(string token) => _RestService.CheckToken(token);


    }
}
