using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SportsStore.Infrastructure;

namespace SportsStore.Models
{

    public class SessionCart : Cart
    {

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services
                .GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            SessionCart cart = session?
                .GetJson<SessionCart>("Cart")
                ?? new SessionCart();

            cart.CurrentSession = session;
            
            return cart;
        }

        [JsonIgnore]
        public ISession CurrentSession { get; set; }

        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            CurrentSession.SetJson("Cart", this);
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            CurrentSession.SetJson("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            CurrentSession.Remove("Cart");
        }
    }
}