using Microsoft.EntityFrameworkCore;
using Shopbridge_base.Domain.Models;

namespace Shopbridge_base.SeedData
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Product_Id = 1,
                    Name="Test 1",
                    Price=200,
                    Description="desc1"
                },
                new Product()
                {
                    Product_Id = 2,
                    Name = "Test 2",
                    Price = 100,
                    Description = "desc2"
                }
            );
        }
    }
}
