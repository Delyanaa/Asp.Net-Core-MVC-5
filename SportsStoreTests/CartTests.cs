using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStoreTests
{
    public class CartTests
    {
        public List<Product> getData()
        {
            var list = new List<Product>() {
                new Product() { ProductID = 1, Name = "P1", Price = 10M },
                new Product() { ProductID = 2, Name = "P2", Price = 15M },
                new Product() { ProductID = 3, Name = "P3", Price = 12M },
                new Product() { ProductID = 4, Name = "P4", Price = 14M },
                new Product() { ProductID = 5, Name = "P5", Price = 15M },
                new Product() { ProductID = 6, Name = "P6", Price = 1M },
                new Product() { ProductID = 7, Name = "P7", Price = 50M }
            };


            return list;
        }

        [Fact]
        public void Can_Add_New_Items()
        {
            //Arrange 
            var cart = new Cart();
            var list = getData(); 

            //Act
            cart.AddItem(list.First(), 2);
            cart.AddItem(list[1], 3);
            cart.AddItem(list[2], 1);

            CartLine[] line = cart.lineCollection.ToArray();

            //Assert
            Assert.Equal(3, line.Length);
            Assert.Equal(list[0], line[0].Product);
            Assert.Equal(list[1], line[1].Product);
        } 
        
        [Fact]
        public void Can_Sum_Total_Price()
        {
            //Arrange 
            var cart = new Cart();
            var line = getData();
            

            //Act
            cart.AddItem(line[0], 2);
            cart.AddItem(line[1], 1);
            cart.AddItem(line[2], 1);

            //Assert
            Assert.Equal(47, cart.ComputeTotalValue());
        }  
        
        [Fact]
        public void Can_Remove_Item()
        {
            //Arrange 
            var cart = new Cart();
            var line = getData();
            

            //Act
            cart.AddItem(line[0], 2);
            cart.AddItem(line[1], 1);
            cart.AddItem(line[2], 1);

            var priceBefore = cart.ComputeTotalValue();
            var cartBefore = cart.lineCollection;

            cart.RemoveLine(new Product() { ProductID = 3, Name = "P3", Price = 12M });

            //Assert
            Assert.Equal(47, priceBefore);
            Assert.Equal(35, cart.ComputeTotalValue());

            Assert.Equal(cart.lineCollection.Count(), cartBefore.Count);
        }

        [Fact]
        public void Can_Remove_All_Items()
        {
            //Arrange 
            var cart = new Cart();
            var line = getData();
            

            //Act
            cart.AddItem(line[0], 2);
            cart.AddItem(line[1], 1);
            cart.AddItem(line[2], 1);
            cart.Clear();

            //Assert
            Assert.True(cart.lineCollection.Count() == 0);
        }


    }
}
