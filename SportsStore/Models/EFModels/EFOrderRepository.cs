using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private AppDbContext context;

        public EFOrderRepository(AppDbContext dbContext)
        {
            context = dbContext;
        }

        // Specify that when an Order object is read from the
        // database, the collection associated with the Lines property 
        // should also be loaded along with each Product object associated with each collection object.
        public IQueryable<Order> Orders => context.Orders.Include(o => o.Lines).ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            // Notify EF that the objects exist and shouldn’t be stored in the database unless they are modified.
            // This ensures that Entity Framework Core won’t try to write the deserialized Product objects that are
            // associated with the Order object
            context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
                context.Orders.Add(order);
            
            context.SaveChanges();
        }
    }
}
