using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Data;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;

namespace Shopbridge_base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ILogger<ProductsController> logger;
        public ProductsController(IProductService _productService, ILogger<ProductsController> logger)
        {
            this.productService = _productService;
        }

       
        [HttpGet]   
        public async Task<IActionResult> GetProduct()
        {
            try
            {
                return Ok(productService.GetProduct());
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Exception at Controller ");
                throw;
            }
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                return Ok(productService.GetProduct(id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception at Controller ");
                throw;
            }
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            try
            {
                return Ok(productService.UpdateProduct(id,product));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception at Controller ");
                throw;
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> PostProduct(Product product)
        {
            try
            {
                return Ok(productService.AddProduct( product));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception at Controller ");
                throw;
            }
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                return Ok(productService.DeleteProduct(id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception at Controller ");
                throw;
            }
        }

    }
}
