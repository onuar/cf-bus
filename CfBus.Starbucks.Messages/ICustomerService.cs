namespace CfBus.Starbucks.Messages
{
    public interface ICustomerService
    {
        void Order(string customerName, OrderDetail orderDetail);
    }
}