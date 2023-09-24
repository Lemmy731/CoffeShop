namespace CoffeeShop.Models.IServices
{
    public interface IOrderService
    {
       Task<Order> GetOrderById(Order order);
       Task<string> SaveOrderTotalAmount(Order order);
       Task<double> GetOrderTotalAmount(Order order);
    }
}
