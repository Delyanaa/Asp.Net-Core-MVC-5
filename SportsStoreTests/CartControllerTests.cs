using Microsoft.Extensions.Configuration;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStoreTests
{
    public class CartControllerTests
    {
        [Fact]
        public void Adding_Item_To_Cart_Count()
        {
            //Arrange
            var mock = new Mock<IProductRepository>();

            mock.Setup(p => p.Products).Returns((new Product[] {
                new Product{ProductID = 1,Name = "P1", Category = "Cat"},
                new Product{ProductID = 2,Name = "P2", Category = "Dog"},
                new Product{ProductID = 3,Name = "P3", Category = "Cow"},
                new Product{ProductID = 4,Name = "P4", Category = "Cat"},
                new Product{ProductID = 5,Name = "P5", Category = "Cow"}
            }).AsQueryable<Product>()
           );

            var controller = new CartController(mock.Object, new Cart());

            //Act
            var result = controller.AddToCart(1, "/");

            //Assert
            mock.VerifyGet(m => m.Products, Times.Once());
        }
    }
}
