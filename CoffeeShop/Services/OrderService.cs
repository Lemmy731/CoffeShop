using CoffeeShop.Data;
using CoffeeShop.Models;
using CoffeeShop.Models.IServices;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Services
{
    public class OrderService: IOrderService
    {
        private readonly AppDbContext _dbContext;

        public OrderService(AppDbContext dbContext)
        {
                _dbContext = dbContext;     
        }

        public async Task<Order> GetOrderById(Order order)
        {
           var orderItem = await _dbContext.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == order.Id);
            return orderItem;
        }
        public async Task<string> SaveOrderTotalAmount(Order order)
        {
            var orderObject = await GetOrderById(order);
            double totalAmount = await GetOrderTotalAmount(orderObject);
            order.TotalAmount = totalAmount;        
            _dbContext.Orders.Update(orderObject);
            var save = await _dbContext.SaveChangesAsync();    
            if(save > 0)
            {
                return "total amount updated successfully";
            }
            return "unsuccessful to update amount";
        }
        public async Task<double> GetOrderTotalAmount(Order order)
        {
            double totalAmount = 0;
            foreach(var item in order.OrderItems)
            {
                totalAmount += item.Quantity * item.Price;
            }
            return totalAmount; 
        }
    }
}
