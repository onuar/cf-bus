namespace CfBus.Starbucks.Messages
{
    public interface ICashierService
    {
        PaymentDetail Payment(OrderDetail orderDetail);
    }
}