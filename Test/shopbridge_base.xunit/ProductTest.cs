using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Controllers;
using Shopbridge_base.Data;
using Shopbridge_base.Data.Repository;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services;
using Shopbridge_base.Domain.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace shopbridge_base.xunit
{
    public class ProductTest
    {
        private ServiceCollection services;
        private ProductsController _controller;
        public ProductTest()
        {
            services = new ServiceCollection();
           

            services.AddDbContext<Shopbridge_Context>(options =>
                    options.UseInMemoryDatabase("Shopbridge_Context"));
            services.AddLogging();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRepository, Repository>();
            var serviceProvider = services.BuildServiceProvider();
            var productsservices=serviceProvider.GetRequiredService<IProductService>();
            var productsLoger = serviceProvider.GetRequiredService<ILogger<ProductsController>>();
            _controller = new ProductsController(productsservices, productsLoger);
        }
        
        [Fact]
        public void GetProduct()
        {
            var result = Task.Run(async () => await _controller.GetProduct()).Result;
            //Assert
            Assert.IsType<OkObjectResult>(result);

            var list = (OkObjectResult) result;

            Assert.IsType<List<Product>>(list.Value);



            var listPro = list.Value as List<Product>;

            Assert.Equal(2, listPro.Count);
        }

        [Theory]
        [InlineData(1)]
        public void GetProductById(int validGuid)
        {
           

            //Act
            var okResult = Task.Run(async () => await _controller.GetProduct(validGuid));

            Assert.IsType<OkObjectResult>(okResult.Result);


            //Now we need to check the value of the result for the ok object result.
            var item = okResult.Result as OkObjectResult;

            //We Expect to return a single book
            Assert.IsType<Product>(item.Value);

            //Now, let us check the value itself.
            var bookItem = item.Value as Product;
            Assert.Equal(validGuid, bookItem.Product_Id);
            Assert.Equal("desc1", bookItem.Name);
        }

        [Fact]
        public void PostProduct()
        {

            //Arange
            var product = new Product()
            {
                Name = "name",
                Description = "Description"
            };

            //Act
            var actionResult = Task.Run(async () => await _controller.PostProduct(product));

            //Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);


            //Now we need to check the value of the result for the ok object result.
            var item = actionResult.Result as OkObjectResult;

            //We Expect to return a single book
            Assert.IsType<Product>(item.Value);

            //Now, let us check the value itself.
            var bookItem = item.Value as Product;
            Assert.Equal("name", bookItem.Name);
            Assert.Equal("Description", bookItem.Description);
        }

        [Fact]
        public void PutProduct()
        {

            //Arange
            var product = new Product()
            {
                Price = 34,
                Product_Id=1,
                Name = "Min test",
                Description = "Min test"
            };

            //Act
            var actionResult = Task.Run(async () => await _controller.PutProduct(1,product));

            //Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);


            //Now we need to check the value of the result for the ok object result.
            var item = actionResult.Result as OkObjectResult;

            //We Expect to return a single book
            Assert.IsType<Product>(item.Value);

            //Now, let us check the value itself.
            var bookItem = item.Value as Product;
            Assert.Equal("Min test", bookItem.Name);
            Assert.Equal("Min test", bookItem.Description);
        }


        [Fact]
        public void DeleteProduct()
        {

            //Arange
            //Act
            var actionResult = Task.Run(async () => await _controller.DeleteProduct(2));

            //Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);


            //Now we need to check the value of the result for the ok object result.
            var item = actionResult.Result as OkObjectResult;

            //We Expect to return a single book
            Assert.IsType<Product>(item.Value);

            //Now, let us check the value itself.
            var bookItem = item.Value as Product;
            Assert.Equal("Test 2", bookItem.Name);
            Assert.Equal("desc2", bookItem.Description);
        }
    }
}