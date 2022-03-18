using Shopbridge_base.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services.Interfaces
{
    public interface IProductService
    {
        public IEnumerable<Product> GetProduct();

        public Product GetProduct(int id);

        public Product UpdateProduct(int id, Product product);

        public Product AddProduct(Product product);

        public Product DeleteProduct(int id);

        public bool ProductExists(int id);
    }
}
