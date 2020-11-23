using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.ViewModels
{
    public class NavigationMenuListViewModel
    {
        public IEnumerable<string> Categories { get; set; }
        public dynamic SelectedCategory { get; set; }

        public NavigationMenuListViewModel(IEnumerable<string> categories, dynamic selected)
        {
            Categories = categories;
            SelectedCategory = selected;
        }
    }
}
