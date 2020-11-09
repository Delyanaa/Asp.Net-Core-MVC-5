using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public IProductRepository repository;

        public NavigationMenuViewComponent(IProductRepository productRepository)
        {
            repository = productRepository;
        }

        public IViewComponentResult Invoke()  //(InvokeAsync) this is the name of the method by convention 
        {

            var navigationMenuListViewModel = new NavigationMenuListViewModel(
                (repository.Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(x => x)), 
                RouteData?.Values["category"]
                );

            return View(navigationMenuListViewModel);
        }
    }
}
