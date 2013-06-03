using System;

namespace CfBus.Starbucks.Messages
{
    [Serializable]
    public class PaymentDetail
    {
        public int Cost { get; set; }
    }
}
