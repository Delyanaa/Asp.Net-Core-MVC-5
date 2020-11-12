using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models
{
    public class Order
    {
        [BindNever] // prevents the user from supplying values for these properties in an HTTP request
        public int OrderID { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Please enter the first address line")]
        public string Address_Line1 { get; set; }
        
        public string Address_Line2 { get; set; }
        
        public string Address_Line3 { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }
       
        [Required(ErrorMessage = "Please enter a state name")]
        public string State { get; set; }
        
        public string Zip { get; set; }
        
        [Required(ErrorMessage = "Please enter a country name")]
        public string Country { get; set; }
        
        public bool GiftWrap { get; set; }
    }
}
