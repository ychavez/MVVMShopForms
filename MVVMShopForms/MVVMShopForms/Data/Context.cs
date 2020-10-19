using MVVMShopForms.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MVVMShopForms.Data
{
    public class Context
    {
        private RestService _RestService;
        public Context()
        {
            _RestService = new RestService(Constants.ServiceUrlBase);
        }

        public async Task<string> Login(Login user)
        {
          var Token =  await _RestService.PostDataAsync<Login>(user, "Account/Login");
            return Token;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _RestService.GetDataAsync<Product>("products");
        }

        public async Task AddProduct(Product product)
        {
            await _RestService.PostDataAsync<Product>(product, "products");
        }

        public async Task UpdateProduct(Product product)
        {
            if (product.Id != 0)
                await _RestService.PutDataAsync<Product>(product, "products", product.Id);
        }
        public async Task DeteProduct(Product product)
        {
            if (product.Id != 0)
                await _RestService.DeleteDataAsync("products", product.Id);
        }
        public bool CheckToken(string token) => _RestService.CheckToken(token);
        

    }
}
