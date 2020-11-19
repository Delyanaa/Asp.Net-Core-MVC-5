using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SportsStoreTests
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            //Arrange
            var mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            var order = new Order();

            var orderController = new OrderController(mock.Object, cart);

            //Act 
            ViewResult result = orderController.Checkout(order) as ViewResult;

            //Assert
            //check that the order hasn't been stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            //check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));

            //check that I am passing an invalid model to the view
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            var mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            cart.AddItem(new Product(), 1);

            var orderController = new OrderController(mock.Object, cart);
            //add an error to the model
            orderController.ModelState.AddModelError("error", "error");

            // Act - try to checkout
            ViewResult result = orderController.Checkout(new Order()) as ViewResult;


            // Assert
            // check that the order hasn't been passed 
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            // check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result.ViewName)); 

            // check that I am passing an invalid model to the view
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            var mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            cart.AddItem(new Product(), 1);

            var orderController = new OrderController(mock.Object, cart);

            // Act - try to checkout
            RedirectToActionResult result =
            orderController.Checkout(new Order()) as RedirectToActionResult;


            // Assert 
            // check that the order has been stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);

            // check that the method is redirecting to the Completed action
            Assert.Equal("Completed", result.ActionName);
        }
    }
}
