using Microsoft.Extensions.Logging;
using Shopbridge_base.Data;
using Shopbridge_base.Data.Repository;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services
{

    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> logger;
        private readonly IRepository repository;
        private readonly Shopbridge_Context dbcontext;


        public ProductService(ILogger<ProductService> logger, IRepository repository, Shopbridge_Context dbcontext)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
        }

        public Product DeleteProduct(int id)
        {
            try
            {
                var prod = new Product();
                if (ProductExists(id))
                {
                    prod = GetProduct(id);
                    dbcontext.Remove(prod);
                    dbcontext.SaveChanges();
                    
                }
                return prod;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception at Product services");
                throw;
            }
        }

        public IEnumerable<Product> GetProduct()
        {
            try
            {
                return repository.Get<Product>();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception at Product services");
                return null;
            }
        }

        public Product GetProduct(int id)
        {
            try
            {
                return repository.Get<Product>(x => x.Product_Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception at Product services");
                return null;
            }
        }

        public Product AddProduct(Product product)
        {
            try
            {
                dbcontext.Add(product);
                dbcontext.SaveChanges();
                return product;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception at Product services");
                throw;
            }
        }

        public bool ProductExists(int id)
        {
            try
            {
                return repository.Get<Product>(x => x.Product_Id == id).Count() > 0;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception at Product services");
                throw;
            }
        }

        public Product UpdateProduct(int id, Product product)
        {
            try
            {
                if (ProductExists(id))
                {
                    
                    dbcontext.Update(product);
                    dbcontext.SaveChanges();

                }
                return product;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception at Product services");
                throw;
            }
        }
    }
}
