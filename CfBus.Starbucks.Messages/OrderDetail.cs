using System;

namespace CfBus.Starbucks.Messages
{
    [Serializable]
    public class OrderDetail
    {
        public string Coffee { get; set; }
        public int Size { get; set; }
    }
}
