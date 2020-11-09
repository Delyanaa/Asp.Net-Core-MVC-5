using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using SportsStore.Components;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStoreTests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            //Assert
            var mock = new Mock<IProductRepository>();

            mock.Setup(p => p.Products).Returns((new Product[] {
                new Product{ProductID = 1,Name = "P1", Category = "Cat"},
                new Product{ProductID = 2,Name = "P2", Category = "Dog"},
                new Product{ProductID = 3,Name = "P3", Category = "Cow"},
                new Product{ProductID = 4,Name = "P4", Category = "Cat"},
                new Product{ProductID = 5,Name = "P5", Category = "Cow"}
            }).AsQueryable<Product>()
            );

            NavigationMenuViewComponent navigationMenuViewComponent = 
                new NavigationMenuViewComponent(mock.Object); 

            //Act
            var result = (
                (IEnumerable<string>) (navigationMenuViewComponent.Invoke()
            as ViewViewComponentResult).ViewData.Model
            ).ToArray();

            //Assert 
            Assert.True(Enumerable.SequenceEqual(result, new string[] {"Cat", "Cow", "Dog"}));
        }
    }
}
