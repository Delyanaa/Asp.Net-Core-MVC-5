using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System.Linq;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int ItemsPerPage = 4;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int productPage = 1) =>
            View(new ProductsListViewModel
            {
                Products = repository.Products
                        .OrderBy(p => p.ProductID)
                        .Where(p => category == null || p.Category == category)
                        .Skip((productPage - 1) * ItemsPerPage)
                        .Take(ItemsPerPage),
                PagingInfo = new PagingInfo
                {
                    TotalItems = (category == null ?
                                    repository.Products.Count() :
                                    repository.Products.Where(e =>
                                    e.Category == category).Count()
                    ),
                    ItemsPerPage = ItemsPerPage,
                    CurrentPage = productPage
                },
                CurrentCategory = category
            }
            );
    }
}