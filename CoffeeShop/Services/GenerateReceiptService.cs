using CoffeeShop.Models.IServices;
using CoffeeShop.Models;
using CoffeeShop.Data;

namespace CoffeeShop.Services
{
    public class GenerateReceiptService: IGenerateReceiptService
    {
        private readonly IOrderService _orderService;
        private readonly AppDbContext _appDbContext;

        public GenerateReceiptService(IOrderService orderService, AppDbContext appDbContext)
        {
            _orderService = orderService;
            _appDbContext = appDbContext;   
        }
        public async Task<string> GenerateOrderReceipt(Order order)
        {
            var orderObject = await _orderService.GetOrderById(order);

            string receipt = await GenerateReceiptFromOrder(orderObject);

            order.OrderReceipt = receipt;

             _appDbContext.Orders.Update(orderObject); 
           await _appDbContext.SaveChangesAsync();        



            return "receipt generated and stored";

        }

        public async Task<string> GenerateReceiptFromOrder(Order order)
        {
            string receipt = $"order id : {order.Id}\n";
            receipt += $"customer name {order.Name}\n";
            foreach (var item in order.OrderItems)
            {
                receipt += $"orderItem product: {item.Product} orderItem quantity: {item.Quantity} orderItem price: {item.Price} total amount: {order.TotalAmount}\n";
            }
            return receipt;

        }
    }

}

