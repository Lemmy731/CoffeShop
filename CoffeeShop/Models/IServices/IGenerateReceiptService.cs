namespace CoffeeShop.Models.IServices
{
    public interface IGenerateReceiptService
    {
        Task<string> GenerateOrderReceipt(Order order);
        Task<string> GenerateReceiptFromOrder(Order order);
    }
}
