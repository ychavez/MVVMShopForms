using Microsoft.EntityFrameworkCore;
using ProductsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsApi.DataContext
{
    public class ProductsContext : DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options)
            : base(options)
        {
           
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }

    }
}
