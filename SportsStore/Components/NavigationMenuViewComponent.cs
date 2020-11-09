using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public string Invoke()  //(InvokeAsync) this is the name of the method by convention 
        {
            return "This will be the category list";
        }
    }
}
