using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStoreTests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
             new Product {ProductID = 1, Name = "P1"},
             new Product {ProductID = 2, Name = "P2"},
             new Product {ProductID = 3, Name = "P3"},
             new Product {ProductID = 4, Name = "P4"},
             new Product {ProductID = 5, Name = "P5"}
             }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object);
            controller.ItemsPerPage = 3;
            // Act
            var result =
            controller.List(null, 2).ViewData.Model as ProductsListViewModel;
            // Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);

        }

        [Fact]
        public void Can_Send_Correct_Pagging_Info_To_View()
        {
            //Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
             new Product {ProductID = 1, Name = "P1"},
             new Product {ProductID = 2, Name = "P2"},
             new Product {ProductID = 3, Name = "P3"},
             new Product {ProductID = 4, Name = "P4"},
             new Product {ProductID = 5, Name = "P5"}
             }).AsQueryable<Product>());

            //Arrange v1
            var controller = new ProductController(mock.Object) { ItemsPerPage = 3};

            //Act
            ProductsListViewModel result =
                controller.List(null, 2).ViewData.Model as ProductsListViewModel as ProductsListViewModel;

            //Assert 
            PagingInfo pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void Can_Filter_Products() {
            //Arange 
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products)
                .Returns(
                    new Product[] {
                        new Product{Name = "P1", Category="cat1"},
                        new Product{Name = "P2", Category="cat2"},
                        new Product{Name = "P3", Category="cat2"},
                        new Product{Name = "P4", Category="cat1"},
                        new Product{Name = "P5", Category="cat1"},
                        new Product{Name = "P6", Category="cat1"}
                    }.AsQueryable<Product>()
                );
            var controller = new ProductController(mock.Object);
            controller.ItemsPerPage = 2;

            //Act
            var viewModel = controller.List("cat1", 1).ViewData.Model as ProductsListViewModel;
            var products = viewModel.Products.ToArray();

            //Assert
            Assert.Equal(2, products.Length);
            Assert.True(products[0].Name == "P1" && products[0].Category == "cat1");
            Assert.True(products[1].Name == "P4" && products[0].Category == "cat1");
        }

        [Fact]
        public void Generate_Category_Specific_Product_Count()
        {
            //Arrange 
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns((new Product[] {
                    new Product{ProductID = 1, Name = "P1", Category = "cat"},
                    new Product{ProductID = 2, Name = "P2", Category = "dog"},
                    new Product{ProductID = 3, Name = "P3", Category = "dog"},
                    new Product{ProductID = 4, Name = "P4", Category = "cat"},
                    new Product{ProductID = 5, Name = "P5", Category = "rabbit"},
                    new Product{ProductID = 6, Name = "P6", Category = "cat"},
                    new Product{ProductID = 7, Name = "P7", Category = "cat"},
                    new Product{ProductID = 8, Name = "P8", Category = "cat"},
                    new Product{ProductID = 9, Name = "P9", Category = "cat"}

                }).AsQueryable<Product>()
            );

            var controller = new ProductController(mock.Object);

            //Act
            Func<ViewResult, ProductsListViewModel> GetModel = result =>
                                                    result?.ViewData?.Model as ProductsListViewModel;
            // Action
            int? res1 = GetModel(controller.List("cat"))?.PagingInfo.TotalItems;
            int? res2 = GetModel(controller.List("dog"))?.PagingInfo.TotalItems;
            int? res3 = GetModel(controller.List("rabbit"))?.PagingInfo.TotalItems;
            int? resAll = GetModel(controller.List(null))?.PagingInfo.TotalItems;

            // Assert
            Assert.Equal(6, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(9, resAll);
        }
    }
}
